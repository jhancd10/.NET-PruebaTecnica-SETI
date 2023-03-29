using System;
using System.Collections.Generic;

namespace SETI.WebApi
{
    public partial class DiscountRate
    {
        public int DiscountRateId { get; set; }
        public int DiscountRatePercentage { get; set; }
        public int? PeriodId { get; set; }

        public virtual Period Period { get; set; }
    }
}
