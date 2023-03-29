using System;
using System.Collections.Generic;

namespace SETI.WebApi
{
    public partial class Period
    {
        public Period()
        {
            DiscountRate = new HashSet<DiscountRate>();
            ProjectMovement = new HashSet<ProjectMovement>();
        }

        public int PeriodId { get; set; }
        public int PeriodYear { get; set; }
        public int PeriodMonth { get; set; }

        public virtual ICollection<DiscountRate> DiscountRate { get; set; }
        public virtual ICollection<ProjectMovement> ProjectMovement { get; set; }
    }
}
