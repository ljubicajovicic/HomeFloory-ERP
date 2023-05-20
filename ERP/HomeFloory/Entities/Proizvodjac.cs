using System;
using System.Collections.Generic;

namespace HomeFloory.Models;

public partial class Proizvodjac
{
    public decimal IdProizvodjac { get; set; }

    public string? NazivProizvodjaca { get; set; }

    public virtual ICollection<Proizvod> Proizvodi { get; } = new List<Proizvod>();
}
