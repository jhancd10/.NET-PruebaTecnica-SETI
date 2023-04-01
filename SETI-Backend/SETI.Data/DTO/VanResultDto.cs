using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SETI.Data.DTO
{
    public class VanResultDto
    {
        public int BrokerId { get; set; }
        public int ProjectsCount { get; set; }
        public List<VanDetail> ProjectsDetail { get; set; }
        public decimal VanAverage { get; set; }
    }

    public class VanDetail
    {
        public int ProjectId { get; set; }
        public decimal InvestmentAmount { get; set; }
        public int Periods { get; set; }
        public decimal Van { get; set; }
    }
}
