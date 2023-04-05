using SETI.Data.Class;
using SETI.Data.DTO;
using SETI.Data.Interfaces.Strategies.Operation;

namespace SETI.Core.Strategies.Operation
{
    public class VanStrategy : IOperationResult
    {
        public OperationDto ExecuteOperation(List<InitialProjectMovement> movements, decimal investmentAmount)
        {
            /*
             * Dada la Formula:
             * VAN = -Io + [ SUMMATORY t = 1 .. n | ( Ft / ( (1 + k) ^ t ) ) ]
             */

            decimal initialInvestmentAmount = investmentAmount * (-1); // -Io

            int periodo = 0; // t
            decimal result = initialInvestmentAmount; // VAN

            // Calculo en cada iteracion los datos para la formula
            foreach (var movement in movements)
            {
                periodo++;

                var divider = Convert.ToDouble(1 + movement.DiscountRatePercentage); // (1 + k)
                var powResult = Convert.ToDecimal(Math.Pow(divider, periodo)); // (1 + k) ^ t
                var partialOperation = movement.MovementAmount / powResult; // Ft / ( (1 + k) ^ t )

                result += partialOperation; // Resultado parcial de la SUMMATORY por cada t
            }

            // Resultado de la operacion
            return new OperationDto()
            {
                Periods = movements.Count,
                Result = result
            };
        }
    }
}
