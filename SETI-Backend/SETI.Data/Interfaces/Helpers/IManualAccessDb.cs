using System.Data;

namespace SETI.Data.Interfaces.Helpers
{
    public interface IManualAccessDb
    {
        DataSet GetQueryResult(string consulta);
    }
}
