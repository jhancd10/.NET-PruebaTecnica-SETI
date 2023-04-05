using SETI.Data.Class;
using SETI.Data.DTO;
using SETI.Data.Enumerables;
using SETI.Data.Interfaces.Strategies.Operation;

namespace SETI.Core.Strategies.Operation
{
    public class OperationContextController
    {
        private int _projectId;
        private decimal _investmentAmount;
        private OperationType _operationType;

        private IOperationResult _operationStrategy;
        
        public OperationContextController(
            int projectId,
            decimal investmentAmount,
            OperationType operationType)
        {
            _projectId = projectId;
            _investmentAmount = investmentAmount;
            _operationType = operationType;

            _operationStrategy = null!;
        }

        public ProjectDetail Get(List<InitialProjectMovement> movements)
        {
            // Tipo de Operacion: Payback - VAN
            switch (_operationType)
            {
                case OperationType.Payback:
                    
                    _operationStrategy = new PaybackStrategy();
                    
                    var payback = _operationStrategy.ExecuteOperation(movements, _investmentAmount);

                    // Resultado de la Operacion Payback
                    return new ProjectDetail()
                    {
                        ProjectId = _projectId,
                        InvestmentAmount = _investmentAmount,
                        Periods = payback.Periods,
                        Result = payback.Result,
                        ResultPeriodsRelation = (payback.Result / payback.Periods)
                    };

                case OperationType.Van:

                    _operationStrategy = new VanStrategy();

                    var van = _operationStrategy.ExecuteOperation(movements, _investmentAmount);

                    // Resultado de la Operacion VAN
                    return new ProjectDetail()
                    {
                        ProjectId = _projectId,
                        InvestmentAmount = _investmentAmount,
                        Periods = van.Periods,
                        Result = van.Result
                    };

                default: return null!;
            }
        }
    }
}
