using SETI.Data.Class;

namespace SETI.Data.Interfaces.Services
{
    public interface IInvestmentProjectService
    {
        List<InitialProject> GetCurrentValidProjects();
    }
}
