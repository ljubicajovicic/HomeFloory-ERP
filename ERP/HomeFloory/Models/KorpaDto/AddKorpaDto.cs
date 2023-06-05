using System.ComponentModel;

namespace HomeFloory.Models.KorpaDto
{
    public class AddKorpaDto
    {
        public decimal? CenaDostave { get; set; }

        [DefaultValue(0)]
        public decimal UkupnaCena { get; set; }

        public List<DodatiProizvodi> DodatiProizvodi { get; set; }

        [DefaultValue(1)]
        public decimal? IdPlacanje { get; set; }
        [DefaultValue(1)]
        public decimal IdDostava { get; set; }
    }
}
