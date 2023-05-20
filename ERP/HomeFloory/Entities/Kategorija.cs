using System;
using System.Collections.Generic;

namespace HomeFloory.Models;

public partial class Kategorija
{
    public decimal IdKategorija { get; set; }

    public string? NazivKategorije { get; set; }

    public virtual ICollection<Proizvod> Proizvodi { get; } = new List<Proizvod>();
}
