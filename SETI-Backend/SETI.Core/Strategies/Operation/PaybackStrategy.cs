using SETI.Data.Class;
using SETI.Data.DTO;
using SETI.Data.Interfaces.Strategies.Operation;

namespace SETI.Core.Strategies.Operation
{
    public class PaybackStrategy : IOperationResult
    {
        public OperationDto ExecuteOperation(List<InitialProjectMovement> movements, decimal investmentAmount)
        {
            /*
             * Dada la Formula:
             * Payback = ( a + ( (Io - b) / Ft) )
             */

            int periodo = 0; // a 
            decimal diff = investmentAmount * (-1);
            decimal benefit = 0; // b
            decimal ft = 0; // Ft

            // Calculo en cada iteracion los datos para la formula
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
                    ft = movement.MovementAmount;
                    break;
                }
            }

            periodo = (periodo == movements.Count) ? periodo + 1 : periodo;
            ft = (ft != 0) ? ft : benefit;

            // Resultado de la operacion
            return new OperationDto()
            {
                Periods = movements.Count,
                Result = (periodo - 1) + ((investmentAmount - benefit) / ft)
            };
        }
    }
}
