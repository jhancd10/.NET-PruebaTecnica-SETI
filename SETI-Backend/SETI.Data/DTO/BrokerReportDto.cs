namespace SETI.Data.DTO
{
    public class BrokerReportDto
    {
        public int BrokerId { get; set; }
        public int ProjectsCount { get; set; }
        public List<ProjectDetail> ProjectsDetail { get; set; }
        public decimal Average { get; set; }
        public decimal OperationPeriodsRelationAverage { get; set; }
    }

    public class ProjectDetail
    {
        public int ProjectId { get; set; }
        public decimal InvestmentAmount { get; set; }
        public int Periods { get; set; }
        public decimal Result { get; set; }
        public decimal ResultPeriodsRelation { get; set; }
    }
}
