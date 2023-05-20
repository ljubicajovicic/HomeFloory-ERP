using System;
using System.Collections.Generic;
using System.ComponentModel;

namespace HomeFloory.Models;

public partial class Korpa
{
    public decimal IdKorpa { get; set; }

    public decimal? CenaDostave { get; set; }

    [DefaultValue(0)]
    public decimal? UkupnaCena { get; set; }

    public decimal IdPlacanje { get; set; }

    public decimal IdDostava { get; set; }

    public virtual ICollection<DodatiProizvodi> DodatiProizvodi { get; } = new List<DodatiProizvodi>();

    public virtual Dostava IdDostavaNavigation { get; set; } = null!;

    public virtual Placanje IdPlacanjeNavigation { get; set; } = null!;
}
