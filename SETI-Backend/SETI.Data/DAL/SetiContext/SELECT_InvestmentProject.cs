using System;
using System.Collections.Generic;

namespace SETI.WebApi
{
    public partial class SELECT_InvestmentProject
    {
        public int ProjectId { get; set; }
        public string ProjectCode { get; set; }
        public string ProjectDescription { get; set; }
        public int? SubsectorId { get; set; }
        public string SubsectorName { get; set; }
        public string SectorName { get; set; }
        public int? BrokerId { get; set; }
        public string BrokerName { get; set; }
        public string BrokerCode { get; set; }
        public int? InvestmentRegionId { get; set; }
        public string RegionName { get; set; }
        public decimal InvestmentAmount { get; set; }
    }
}
