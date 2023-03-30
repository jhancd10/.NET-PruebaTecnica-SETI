using Microsoft.EntityFrameworkCore;
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
        private readonly SetiDbContext _setiDbContext;

        public ProjectMovementService(SetiDbContext setiDbContext)
        {
            _setiDbContext = setiDbContext;
        }

        public async Task<List<ProjectMovement>> GetMovementsByProjectId(int projectId) 
            => await _setiDbContext.ProjectMovement
                    .Where(x => x.ProjectId == projectId)
                    .OrderBy(x => x.MovementId).ToListAsync();
    }
}
