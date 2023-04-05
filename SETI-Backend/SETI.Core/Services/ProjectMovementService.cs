using Microsoft.Extensions.Caching.Memory;
using SETI.Data.Class;
using SETI.Data.Interfaces.Services;

namespace SETI.Core.Services
{
    public class ProjectMovementService : IProjectMovementService
    {
        private readonly IMemoryCache _cache;

        public ProjectMovementService(
            IMemoryCache cache)
        {
            _cache = cache;
        }

        public List<InitialProjectMovement> GetMovementsByProjectId(int projectId)
        {
            var projectMovements = (List<InitialProjectMovement>) _cache.Get("ProjectMovements");

            // Query para obtener los movimientos de un proyecto por identificador
            return projectMovements
                .Where(x =>  x.ProjectId == projectId)
                .OrderBy(x => x.MovementId).ToList();
        }
    }
}
