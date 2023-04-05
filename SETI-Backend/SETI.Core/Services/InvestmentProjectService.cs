using SETI.Data.Class;
using SETI.Data.Interfaces.Helpers;
using SETI.Data.Interfaces.Services;
using System.Data;

namespace SETI.Core.Services
{
    public class InvestmentProjectService : IInvestmentProjectService
    {
        private readonly IManualAccessDb _manualAccessDb;

        public InvestmentProjectService(
            IManualAccessDb manualAccessDb) 
        {
            _manualAccessDb = manualAccessDb;
        }

        public List<InitialProject> GetCurrentValidProjects()
        {
            var initialProjects = new List<InitialProject>();
            
            var periodYear = DateTime.Now.Year;
            var periodMonth = DateTime.Now.Month;

            // Query para obtener la informacion de los proyectos validos
            var currentProjectsQuery = "select IP.BrokerId, IP.ProjectId, IP.InvestmentAmount " +
                                       "from InvestmentProject IP " +
                                       "where IP.ProjectId not in ( " +
                                           "select distinct PM.ProjectId  " +
                                           "from ProjectMovement PM " +
                                           "where PM.PeriodId IN ( " +
                                               "select P.PeriodId  " +
                                               "from Period P " +
                                               "where P.PeriodYear < " + periodYear + " or  " +
                                               "(P.PeriodYear = " + periodYear + " and P.PeriodMonth <= " + 
                                               periodMonth + "))) " +
                                       "intersect " +
                                       "select distinct IP2.BrokerId, PM1.ProjectId, IP2.InvestmentAmount " +
                                       "from ProjectMovement PM1 inner join " +
                                       "InvestmentProject IP2 on  " +
                                       "PM1.ProjectId = IP2.ProjectId " +
                                       "where PeriodId IN ( " +
                                           "select PeriodId " +
                                           "from Period " +
                                           "where PeriodYear = " + periodYear + " and PeriodMonth = " + 
                                           (periodMonth + 1) + ")";

            var dataSetResponse = _manualAccessDb.GetQueryResult(currentProjectsQuery);

            for (int i = 0; i < dataSetResponse.Tables[0].Rows.Count; i++)
            {
                initialProjects.Add(new InitialProject()
                {
                    BrokerId = Convert.ToInt32(dataSetResponse.Tables[0].Rows[i][0]),
                    ProjectId = Convert.ToInt32(dataSetResponse.Tables[0].Rows[i][1]),
                    InvestmentAmount = Convert.ToDecimal(dataSetResponse.Tables[0].Rows[i][2])
                });
            }
            return initialProjects.OrderBy(x => x.BrokerId).ToList();
        }
    }
}
