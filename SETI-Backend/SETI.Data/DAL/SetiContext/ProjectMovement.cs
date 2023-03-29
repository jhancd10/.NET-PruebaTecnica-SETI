using System;
using System.Collections.Generic;

namespace SETI.WebApi
{
    public partial class ProjectMovement
    {
        public int MovementId { get; set; }
        public decimal MovementAmount { get; set; }
        public int? PeriodId { get; set; }
        public int? ProjectId { get; set; }

        public virtual Period Period { get; set; }
        public virtual InvestmentProject Project { get; set; }
    }
}
