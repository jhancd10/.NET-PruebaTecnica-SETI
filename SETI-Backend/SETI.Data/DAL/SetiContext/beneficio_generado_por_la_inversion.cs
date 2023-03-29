using System;
using System.Collections.Generic;

namespace SETI.WebApi
{
    public partial class beneficio_generado_por_la_inversion
    {
        public int BrokerId { get; set; }
        public string BrokerName { get; set; }
        public int ProjectId { get; set; }
        public decimal InversionInicial { get; set; }
        public decimal? VAN { get; set; }
    }
}
