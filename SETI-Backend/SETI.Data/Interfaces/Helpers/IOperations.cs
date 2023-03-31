using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SETI.Data.Interfaces.Helpers
{
    public interface IOperations
    {
        (int, decimal) GetPaybackByProjectId(int projectId, decimal investmentAmount);
    }
}
