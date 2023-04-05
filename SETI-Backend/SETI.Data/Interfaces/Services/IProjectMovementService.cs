using SETI.Data.Class;

namespace SETI.Data.Interfaces.Services
{
    public interface IProjectMovementService
    {
        List<InitialProjectMovement> GetMovementsByProjectId(int projectId);
    }
}
