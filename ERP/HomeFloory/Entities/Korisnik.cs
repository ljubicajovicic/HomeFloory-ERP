using System;
using System.Collections.Generic;
using System.ComponentModel;

namespace HomeFloory.Models;

public partial class Korisnik
{
    public decimal IdKorisnik { get; set; }

    public string? Ime { get; set; }

    public string? Prezime { get; set; }

    public DateTime? DatumRodjenja { get; set; }

    public string? Kontakt { get; set; }

    public string? Email { get; set; }

    public string? Lozinka { get; set; }

    public decimal? IdAdresaIsporuke { get; set; }

    [DefaultValue(1)]
    public decimal IdUloga { get; set; }

    public virtual AdresaIsporuke? IdAdresaIsporukeNavigation { get; set; }

    public virtual Uloga IdUlogaNavigation { get; set; } = null!;

    public virtual ICollection<Placanje> Placanja { get; } = new List<Placanje>();
}
