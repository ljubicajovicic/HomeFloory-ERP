using System;
using System.Collections.Generic;

namespace HomeFloory.Models;

public partial class Uloga
{
    public decimal IdUloga { get; set; }

    public string? Uloga1 { get; set; }

    public virtual ICollection<Korisnik> Korisnici { get; } = new List<Korisnik>();
}
