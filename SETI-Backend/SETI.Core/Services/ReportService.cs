using Microsoft.Extensions.Caching.Memory;
using SETI.Core.Strategies.Operation;
using SETI.Data.Class;
using SETI.Data.DTO;
using SETI.Data.Enumerables;
using SETI.Data.Interfaces.Services;
using System.Collections.Concurrent;

namespace SETI.Core.Services
{
    public class ReportService : IReportService
    {
        private readonly IMemoryCache _cache;
        private readonly IProjectMovementService _projectMovementService;

        public ReportService(
            IMemoryCache cache,
            IProjectMovementService projectMovementService)
        {
            _cache = cache;
            _projectMovementService = projectMovementService;
        }

        public List<BrokerReportDto> ReportGenerator(OperationType operationType)
        {
            // Obtengo de la cache la data de los Proyectos Validos por Broker
            var currentValidProjects = (List<InitialProject>)_cache.Get("CurrentValidProjects");

            // Listado de identificadores de Broker
            var brokersId = currentValidProjects
                .Select(x => x.BrokerId).DistinctBy(x => x).ToList();

            // Listado Final
            ConcurrentBag<BrokerReportDto> BrokersDetailConcurrent = new();

            // Uso de programacion Concurrente por Brokers
            Parallel.ForEach(brokersId, new ParallelOptions() { MaxDegreeOfParallelism = 10 }, brokerId =>
            {
                // Obtengo los proyectos del broker
                var brokerProjects = currentValidProjects
                    .Where(x => x.BrokerId == brokerId).ToList();

                // Objeto para representar la informacion por Broker
                var brokerResult = new BrokerReportDto()
                {
                    BrokerId = brokerId,
                    ProjectsCount = brokerProjects.Count
                };

                // Listado con la informacion de cada proyecto del Broker 
                ConcurrentBag<ProjectDetail> ProjectsDetailConcurrent = new();

                // Uso de programacion Concurrente por Proyectos de cada Broker
                Parallel.ForEach(brokerProjects, new ParallelOptions() { MaxDegreeOfParallelism = 10 }, project =>
                {
                    // Obtengo los movimientos de cada proyecto por identificador de proyecto
                    var movements = _projectMovementService.GetMovementsByProjectId(project.ProjectId);

                    /* Instancio la clase que controla las estrategias de Operaciones:
                       Recibe la Data del Proyecto y el tipo de operacion */
                    var operationController = 
                        new OperationContextController(project.ProjectId, project.InvestmentAmount, operationType);

                    // Obtengo el resultado de la operacion
                    var operationResult = operationController.Get(movements);

                    // Agrego el resultado del proyecto al listado detalle por Broker
                    ProjectsDetailConcurrent.Add(operationResult);
                });

                // Al finalizar las operaciones en todos los proyectos del Broker agrego el detalle
                brokerResult.ProjectsDetail = ProjectsDetailConcurrent.ToList();

                // Calculo el promedio de la operacion de todos los proyectos
                brokerResult.Average =
                    brokerResult.ProjectsDetail.Select(x => x.Result).Average();

                // Calculo el resultado de Operacion/Periodo para interpretar en el resultado
                brokerResult.OperationPeriodsRelationAverage =
                    brokerResult.ProjectsDetail.Select(x => x.ResultPeriodsRelation).Average();

                // Agrego el resultado del Broker al Listado Final
                BrokersDetailConcurrent.Add(brokerResult);
            });

            // Retorno el Listado Final con la informacion detallada por Broker
            return BrokersDetailConcurrent.ToList();
        }
    }
}
