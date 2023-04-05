using SETI.Data.Class;
using SETI.Data.DTO;

namespace SETI.Data.Interfaces.Strategies.Operation
{
    public interface IOperationResult
    {
        public OperationDto ExecuteOperation(List<InitialProjectMovement> movements, decimal investmentAmount);
    }
}
