using System.ComponentModel;

namespace HomeFloory.Models.KorpaDto
{
    public class UpdateKorpaDto
    {
        public decimal? CenaDostave { get; set; }

        [DefaultValue(0)]
        public decimal UkupnaCena { get; set; }

        public decimal IdKorisnik { get; set; }

        public decimal IdDostava { get; set; }


        public DateTime? Datum { get; set; }

        public string? Status { get; set; }


        /*public virtual ICollection<DodatiProizvodi>? DodatiProizvodi
        { get; set; } = new List<DodatiProizvodi>();*/
    }
}
