using Microsoft.AspNetCore.Mvc;
using SETI.Data.Enumerables;
using SETI.Data.Interfaces.Services;

namespace SETI.WebApi.Controllers
{
    [ApiController]
    public class ReportesController : ControllerBase
    {
        private IReportService _reportService;

        public ReportesController(IReportService reportService)
        {
            _reportService = reportService;
        }

        [Route("api/[controller]/[action]")]
        [HttpGet]
        public async Task<IActionResult> TiempoRecuperacionInversion()
        {
            var reportResponse = await Task.Run(() =>
                _reportService.ReportGenerator(OperationType.Payback)
            );

            /* El endpoint responde un listado ordenado por el Promedio de Payback
               de cada Broker */
            return Ok(reportResponse.OrderBy(x => x.OperationPeriodsRelationAverage).ToList());
        }
         
        [Route("api/[controller]/[action]")]
        [HttpGet]
        public async Task<IActionResult> BeneficioGeneradoInversion()
        {
            var reportResponse = await Task.Run(() =>
                _reportService.ReportGenerator(OperationType.Van)
            );

            /* El endpoint responde un listado descendente ordenado por el Promedio de VAN
               de cada Broker */
            return Ok(reportResponse.OrderByDescending(x => x.Average).ToList());
        }
    }
}
