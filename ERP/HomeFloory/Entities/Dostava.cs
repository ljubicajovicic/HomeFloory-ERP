using System;
using System.Collections.Generic;

namespace HomeFloory.Models;

public partial class Dostava
{
    public decimal IdDostava { get; set; }

    public string? TipDostave { get; set; }

    public string? NazivSluzbe { get; set; }

    public decimal? CenaUsluge { get; set; }

    public string? RokDostave { get; set; }

    public virtual ICollection<Korpa> Korpe { get; } = new List<Korpa>();
}
