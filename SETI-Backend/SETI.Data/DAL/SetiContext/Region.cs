using System;
using System.Collections.Generic;

namespace SETI.WebApi
{
    public partial class Region
    {
        public Region()
        {
            Broker = new HashSet<Broker>();
            InvestmentProject = new HashSet<InvestmentProject>();
        }

        public int RegionId { get; set; }
        public string RegionCode { get; set; }
        public string RegionName { get; set; }

        public virtual ICollection<Broker> Broker { get; set; }
        public virtual ICollection<InvestmentProject> InvestmentProject { get; set; }
    }
}
