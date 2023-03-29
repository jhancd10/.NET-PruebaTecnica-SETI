using System;
using System.Collections.Generic;

namespace SETI.WebApi
{
    public partial class tiempo_recuperaion_Inversion
    {
        public int BrokerId { get; set; }
        public string BrokerName { get; set; }
        public int ProjectId { get; set; }
        public decimal InversionInicial { get; set; }
        public decimal? SumaFlujos { get; set; }
        public int? CantidadPeridos { get; set; }
        public decimal? FlujoCajaAñosAntesRecuperar { get; set; }
        public decimal? payback { get; set; }
    }
}
