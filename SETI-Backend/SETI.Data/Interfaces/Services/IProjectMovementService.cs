using SETI.Data.Class;
using SETI.WebApi;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SETI.Data.Interfaces.Services
{
    public interface IProjectMovementService
    {
        List<InitialProjectMovement> GetMovementsByProjectId(int projectId);
    }
}
