using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
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

        //[Authorize]
        [Route("api/[controller]/[action]")]
        [HttpGet]
        public async Task<IActionResult> TiempoRecuperacionInversion()
        {
            var reportResponse = await Task.Run(() =>
                _reportService.TiempoRecuperacionInversion()
            );

            return Ok(reportResponse);
        }

        //[Authorize]
        /*[Route("api/[controller]/[action]")]
        [HttpGet]
        public async Task<IActionResult> BeneficioGeneradoInversion()
        {
            //return BadRequest("Se ha producido un error inesperado, vuelve a intentarlo.");

            return Ok(true);
        }*/
    }
}
