using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SETI.Data.Interfaces.Helpers
{
    public interface IManualAccessDb
    {
        DataSet GetQueryResult(string consulta);
    }
}
