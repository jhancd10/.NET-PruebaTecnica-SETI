using SETI.Data.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SETI.Data.Interfaces.Services
{
    public interface IReportService
    {
        List<PaybackResultDto> TiempoRecuperacionInversion();
        List<VanResultDto> BeneficioGeneradoInversion();
    }
}
