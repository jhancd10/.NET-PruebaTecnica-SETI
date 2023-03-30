using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Caching.Memory;
using SETI.Data.Class;
using SETI.Data.DTO;
using SETI.Data.Interfaces.Helpers;
using SETI.Data.Interfaces.Services;
using SETI.WebApi;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SETI.Core.Services
{
    public class ReportService : IReportService
    {
        private readonly IMemoryCache _cache;
        private readonly IProjectMovementService _projectMovementService;
        private readonly IOperations _operations;

        public ReportService(
            IMemoryCache cache,
            IProjectMovementService projectMovementService,
            IOperations operations)
        {
            _cache = cache;
            _projectMovementService = projectMovementService;
            _operations = operations;
        }

        public async Task<List<PaybackResultDto>> TiempoRecuperacionInversion()
        {
            List<PaybackResultDto> BrokersDetailResponse = new();

            var currentValidProjects = (List<InitialProject>) _cache.Get("CurrentValidProjects");

            PaybackResultDto paybackResult = null!;

            int brokerTmp = 0;
            int projectxBroker = 1;

            foreach (var project in currentValidProjects) 
            { 
                if (brokerTmp != project.BrokerId)
                {
                    if (brokerTmp != 0)
                    {
                        paybackResult.ProjectsCount = projectxBroker;

                        paybackResult.PaybackAverage = 
                            paybackResult.ProjectsDetail.Select(x => x.Payback).Average();
                        
                        BrokersDetailResponse.Add(paybackResult);
                    }

                    paybackResult = new PaybackResultDto()
                    {
                        BrokerId = project.BrokerId,
                        ProjectsCount = projectxBroker,
                        ProjectsDetail = new List<PaybackDetail>()
                    };

                    brokerTmp = project.BrokerId;
                    projectxBroker = 1;
                }

                else projectxBroker++;

                var payback = await _operations.GetPaybackByProjectId(project.ProjectId, project.InvestmentAmount);

                paybackResult.ProjectsDetail.Add(new PaybackDetail()
                {
                    ProjectId = project.ProjectId,
                    InvestmentAmount = project.InvestmentAmount,
                    Periods = payback.Item1,
                    Payback = payback.Item2
                });
            }
            return BrokersDetailResponse.OrderBy(x => x.PaybackAverage).ToList();
        }
    }
}
