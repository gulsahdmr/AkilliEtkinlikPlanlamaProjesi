using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace yazlab123
{
    public class Kullanicilar
    {

        public int KullaniciID { get; set; }
        public string KullaniciAdi { get; set; }
        public string Sifre { get; set; }
        public string Eposta { get; set; }
        public string ilgiAlanlari { get; set; }
        public string Ad{ get; set; } // Dakika cinsinden
        public string Konum{ get; set; }
        public string Soyad { get; set; }
        public DateTime DogumTarihi { get; set; }

        public string Cinsiyet {  get; set; }
        public string  TelefonNo { get; set; }
        public String ProfilFoto { get; set; }

    }
}