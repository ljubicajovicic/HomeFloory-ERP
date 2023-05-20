using System;
using System.Collections.Generic;

namespace HomeFloory.Models;

public partial class Placanje
{
    public decimal IdPlacanje { get; set; }

    public string? Status { get; set; }

    public DateTime? Datum { get; set; }

    public decimal IdKorisnik { get; set; }

    public virtual Korisnik IdKorisnikNavigation { get; set; } = null!;

    public virtual ICollection<Korpa> Korpe { get; } = new List<Korpa>();
}
