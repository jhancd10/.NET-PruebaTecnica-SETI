using System;
using System.Collections.Generic;

namespace SETI.WebApi
{
    public partial class InvestmentProject
    {
        public InvestmentProject()
        {
            ProjectMovement = new HashSet<ProjectMovement>();
        }

        public int ProjectId { get; set; }
        public string ProjectCode { get; set; }
        public string ProjectDescription { get; set; }
        public int? SubsectorId { get; set; }
        public int? BrokerId { get; set; }
        public int? InvestmentRegionId { get; set; }
        public decimal InvestmentAmount { get; set; }

        public virtual Broker Broker { get; set; }
        public virtual Region InvestmentRegion { get; set; }
        public virtual InvestmentSubsector Subsector { get; set; }
        public virtual ICollection<ProjectMovement> ProjectMovement { get; set; }
    }
}
