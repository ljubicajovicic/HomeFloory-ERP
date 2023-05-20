using System.ComponentModel;

namespace HomeFloory.Models.KorpaDto
{
    public class KorpaDto
    {
        public decimal IdKorpa { get; set; }

        public decimal? CenaDostave { get; set; }

        [DefaultValue(0)]
        public decimal? UkupnaCena { get; set; }

        public decimal IdPlacanje { get; set; }

        public decimal IdDostava { get; set; }
    }
}
