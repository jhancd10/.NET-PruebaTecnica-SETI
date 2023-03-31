using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SETI.Data.DTO
{
    public class PaybackResultDto
    {
        public int BrokerId { get; set; }
        public int ProjectsCount { get; set; }
        public List<PaybackDetail> ProjectsDetail { get; set; }
        public decimal PaybackAverage { get; set; }
        public decimal PaybackPeriodsRelationAverage { get; set; }
    }

    public class PaybackDetail
    {
        public int ProjectId { get; set; }
        public decimal InvestmentAmount { get; set; }
        public int Periods { get; set; }
        public decimal Payback { get; set; }
        public decimal PaybackPeriodsRelation { get; set; }
    }
}
