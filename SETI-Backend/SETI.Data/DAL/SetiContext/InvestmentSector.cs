using System;
using System.Collections.Generic;

namespace SETI.WebApi
{
    public partial class InvestmentSector
    {
        public InvestmentSector()
        {
            InvestmentSubsector = new HashSet<InvestmentSubsector>();
        }

        public int SectorId { get; set; }
        public string SectorName { get; set; }

        public virtual ICollection<InvestmentSubsector> InvestmentSubsector { get; set; }
    }
}
