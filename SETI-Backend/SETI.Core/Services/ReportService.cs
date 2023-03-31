using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Caching.Memory;
using SETI.Data.Class;
using SETI.Data.DTO;
using SETI.Data.Interfaces.Helpers;
using SETI.Data.Interfaces.Services;
using SETI.WebApi;
using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SETI.Core.Services
{
    public class ReportService : IReportService
    {
        private readonly IMemoryCache _cache;
        private readonly IOperations _operations;

        public ReportService(
            IMemoryCache cache,
            IOperations operations)
        {
            _cache = cache;
            _operations = operations;
        }

        public List<PaybackResultDto> TiempoRecuperacionInversion()
        {
            var currentValidProjects = (List<InitialProject>)_cache.Get("CurrentValidProjects");

            var brokersId = currentValidProjects
                .Select(x => x.BrokerId).DistinctBy(x => x).ToList();

            ConcurrentBag<PaybackResultDto> BrokersDetailConcurrent = new();

            //foreach (var brokerId in brokersId)
            Parallel.ForEach(brokersId, new ParallelOptions() { MaxDegreeOfParallelism = 10 }, brokerId =>
            {
                var brokerProjects = currentValidProjects
                    .Where(x => x.BrokerId == brokerId).ToList();

                var paybackResult = new PaybackResultDto()
                {
                    BrokerId = brokerId,
                    ProjectsCount = brokerProjects.Count
                };

                ConcurrentBag<PaybackDetail> ProjectsDetailConcurrent = new();

                //foreach (var project in brokerProjects)
                Parallel.ForEach(brokerProjects, new ParallelOptions() { MaxDegreeOfParallelism = 10 }, project =>
                {
                    var payback = _operations.GetPaybackByProjectId(project.ProjectId, project.InvestmentAmount);

                    ProjectsDetailConcurrent.Add(new PaybackDetail()
                    {
                        ProjectId = project.ProjectId,
                        InvestmentAmount = project.InvestmentAmount,
                        Periods = payback.Item1,
                        Payback = payback.Item2,
                        PaybackPeriodsRelation = (payback.Item2 / payback.Item1)
                    });
                });

                paybackResult.ProjectsDetail = ProjectsDetailConcurrent.ToList();

                paybackResult.PaybackAverage =
                    paybackResult.ProjectsDetail.Select(x => x.Payback).Average();

                paybackResult.PaybackPeriodsRelationAverage =
                    paybackResult.ProjectsDetail.Select(x => x.PaybackPeriodsRelation).Average();

                BrokersDetailConcurrent.Add(paybackResult);
            });

            return BrokersDetailConcurrent.OrderBy(x => x.PaybackPeriodsRelationAverage).ToList();
        }
    }
}
