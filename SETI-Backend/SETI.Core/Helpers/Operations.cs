using SETI.Core.Services;
using SETI.Data.Interfaces.Helpers;
using SETI.Data.Interfaces.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SETI.Core.Helpers
{
    public class Operations : IOperations
    {
        private readonly IProjectMovementService _projectMovementService;

        public Operations(
            IProjectMovementService projectMovementService)
        {
            _projectMovementService = projectMovementService;
        }

        public (int, decimal) GetPaybackByProjectId(int projectId, decimal investmentAmount)
        {
            var movements = _projectMovementService.GetMovementsByProjectId(projectId);

            int periodo = 0;
            decimal diff = investmentAmount * (-1);
            decimal benefit = 0;
            decimal ft = 0;

            foreach (var movement in movements)
            {
                periodo++;

                if ((diff + movement.MovementAmount) < 0)
                {
                    diff += movement.MovementAmount;
                    benefit += movement.MovementAmount;
                }
                else
                {
                    ft = benefit + movement.MovementAmount;
                    break;
                }
            }

            periodo = (periodo == movements.Count) ? periodo + 1 : periodo;
            ft = (ft != 0) ? ft : benefit;

            return (movements.Count, (periodo - 1) + ((investmentAmount - benefit) / ft));
        }
    }
}
