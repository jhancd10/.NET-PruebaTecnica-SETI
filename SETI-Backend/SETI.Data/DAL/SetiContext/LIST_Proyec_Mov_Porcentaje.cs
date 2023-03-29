using System;
using System.Collections.Generic;

namespace SETI.WebApi
{
    public partial class LIST_Proyec_Mov_Porcentaje
    {
        public int? ProjectId { get; set; }
        public decimal SumMovimiento { get; set; }
        public int PeriodYear { get; set; }
        public int PeriodMonth { get; set; }
        public decimal? DiscountRatePercentage { get; set; }
        public long? Periodo { get; set; }
    }
}
