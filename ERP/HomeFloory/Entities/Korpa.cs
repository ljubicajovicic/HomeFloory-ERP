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

    public DateTime? Datum { get; set; }

    public string? Status { get; set; }

    public string? PaymentIntent { get; set; }

    public string? ClientSecret { get; set; }

    [DefaultValue(1)]
    public decimal IdPlacanje { get; set; }

    public decimal IdDostava { get; set; }

    public virtual ICollection<DodatiProizvodi> DodatiProizvodi { get; set; } = new List<DodatiProizvodi>();

    public virtual Dostava? IdDostavaNavigation { get; set; } = null!;

    public virtual Placanje? IdPlacanjeNavigation { get; set; } = null!;
}
