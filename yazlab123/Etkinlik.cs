using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace yazlab123
{
    public class Etkinlik
    {
        public int EtkinlikID { get; set; }
        public string EtkinlikAdi { get; set; }
        public string EtkinlikAciklamasi { get; set; }
        public DateTime EtkinlikTarihi { get; set; }
        public TimeSpan EtkinlikSaati { get; set; }
        public int EtkinlikSuresi { get; set; } // Dakika cinsinden
        public string EtkinlikKonumu { get; set; }
        public string EtkinlikKategorisi { get; set; }
    }
}