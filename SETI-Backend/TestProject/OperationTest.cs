using SETI.Core.Strategies.Operation;
using SETI.Data.Class;
using SETI.Data.Enumerables;

namespace TestProject
{
    [TestClass]
    public class OperationTest
    {
        // Inversion Inicial del Proyecto de Prueba
        private readonly decimal _investmentAmount = 5000;

        // Listado de Movimientos del Proyecto de Prueba
        private readonly List<InitialProjectMovement> _movements = new List<InitialProjectMovement>(
            new InitialProjectMovement[]
            {
                    new InitialProjectMovement()
                    {
                        MovementId = 1,
                        PeriodId = 1,
                        ProjectId = 1,
                        MovementAmount = 1200,
                        DiscountRatePercentage = 0.12m
                    },
                    new InitialProjectMovement()
                    {
                        MovementId = 2,
                        PeriodId = 2,
                        ProjectId = 1,
                        MovementAmount = 1900,
                        DiscountRatePercentage = 0.12m
                    },
                    new InitialProjectMovement()
                    {
                        MovementId = 3,
                        PeriodId = 3,
                        ProjectId = 1,
                        MovementAmount = 2600,
                        DiscountRatePercentage = 0.12m
                    },
                    new InitialProjectMovement()
                    {
                        MovementId = 4,
                        PeriodId = 4,
                        ProjectId = 1,
                        MovementAmount = 3700,
                        DiscountRatePercentage = 0.12m
                    }
            }
        );

        [TestMethod]
        public void TestPaybackOperation()
        {
            /* Prueba de la formula: Payback */

            var operationController =
                        new OperationContextController(1, _investmentAmount, OperationType.Payback);

            // Resultado de la operacion 'Payback' usando el proyecto de prueba
            var operationResult = operationController.Get(_movements);

            // Al debuggear se verifica que el resultado es '2,73' --> Prueba Exitosa.
        }

        [TestMethod]
        public void TestVanOperation()
        {
            /* Prueba de la formula: Van */

            var operationController =
                        new OperationContextController(1, _investmentAmount, OperationType.Van);

            // Resultado de la operacion 'VAN' usando el proyecto de prueba
            var operationResult = operationController.Get(_movements);

            // Al debuggear se verifica que el resultado es '1788,14' --> Prueba Exitosa.
        }
    }
}