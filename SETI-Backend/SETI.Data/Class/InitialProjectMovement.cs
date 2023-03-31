using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SETI.Data.Class
{
    public class InitialProjectMovement
    {
        public int MovementId { get; set; }
        public decimal MovementAmount { get; set; }
        public int PeriodId { get; set; }
        public int ProjectId { get; set; }
        public decimal DiscountRatePercentage { get; set; }
    }
}
