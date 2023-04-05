using SETI.Data.DTO;
using SETI.Data.Enumerables;

namespace SETI.Data.Interfaces.Services
{
    public interface IReportService
    {
        List<BrokerReportDto> ReportGenerator(OperationType operationType);
    }
}
