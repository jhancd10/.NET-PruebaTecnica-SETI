using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Caching.Memory;
using SETI.Data.Class;
using SETI.Data.Interfaces.Services;
using SETI.WebApi;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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

            return projectMovements
                .Where(x =>  x.ProjectId == projectId)
                .OrderBy(x => x.MovementId).ToList();
        }
    }
}
