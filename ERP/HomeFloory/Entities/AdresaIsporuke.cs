using System;
using System.Collections.Generic;

namespace HomeFloory.Models;

public partial class AdresaIsporuke
{
    public decimal IdAdresaIsporuke { get; set; }

    public string? Grad { get; set; }

    public string? Drzava { get; set; }

    public string? Ulica { get; set; }

    public string? PostanskiBroj { get; set; }

    public virtual ICollection<Korisnik> Korisnici { get; } = new List<Korisnik>();

}
