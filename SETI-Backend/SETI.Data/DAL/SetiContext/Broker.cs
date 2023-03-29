using System;
using System.Collections.Generic;

namespace SETI.WebApi
{
    public partial class Broker
    {
        public Broker()
        {
            InvestmentProject = new HashSet<InvestmentProject>();
        }

        public int BrokerId { get; set; }
        public string BrokerCode { get; set; }
        public string BrokerName { get; set; }
        public int? LocationRegionId { get; set; }

        public virtual Region LocationRegion { get; set; }
        public virtual ICollection<InvestmentProject> InvestmentProject { get; set; }
    }
}
