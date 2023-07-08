using System.ComponentModel;

namespace HomeFloory.Models.KorpaDto
{
    public class KorpaDto
    {
        public decimal IdKorpa { get; set; }

        public decimal? CenaDostave { get; set; }

        [DefaultValue(0)]
        public decimal UkupnaCena { get; set; }


        public string? PaymentIntent { get; set; }

        public string? ClientSecret { get; set; }

        //[DefaultValue(1)]
        public decimal IdKorisnik { get; set; }
        //[DefaultValue(1)]
        public decimal IdDostava { get; set; }

        public List<DodatiProizvodi> DodatiProizvodi { get; set; }

    }
}
