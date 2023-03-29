using System;
using System.Collections.Generic;

namespace SETI.WebApi
{
    public partial class InvestmentSubsector
    {
        public InvestmentSubsector()
        {
            InvestmentProject = new HashSet<InvestmentProject>();
        }

        public int SubsectorId { get; set; }
        public string SubsectorName { get; set; }
        public int? SectorId { get; set; }

        public virtual InvestmentSector Sector { get; set; }
        public virtual ICollection<InvestmentProject> InvestmentProject { get; set; }
    }
}
