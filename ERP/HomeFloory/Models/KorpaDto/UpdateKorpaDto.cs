using System.ComponentModel;

namespace HomeFloory.Models.KorpaDto
{
    public class UpdateKorpaDto
    {
        public decimal? CenaDostave { get; set; }

        [DefaultValue(0)]
        public decimal? UkupnaCena { get; set; }

        public decimal IdPlacanje { get; set; }

        public decimal IdDostava { get; set; }
    }
}
