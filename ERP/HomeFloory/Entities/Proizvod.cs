using System;
using System.Collections.Generic;

namespace HomeFloory.Models;

public partial class Proizvod
{
    public decimal IdProizvod { get; set; }

    public string? Naziv { get; set; }

    public string? Opis { get; set; }

    public decimal? KolicinaNaStanju { get; set; }

    public decimal? CenaPoM2 { get; set; }

    public decimal? PaketPoM2 { get; set; }

    public string? Dimenzija { get; set; }

    public string? Nijansa { get; set; }

    public string? UrlSlike { get; set; }

    public decimal IdKategorija { get; set; }

    public decimal? IdProizvodjac { get; set; }

    public virtual ICollection<DodatiProizvodi> DodatiProizvodi { get; } = new List<DodatiProizvodi>();

    public virtual Kategorija IdKategorijaNavigation { get; set; } = null!;

    public virtual Proizvodjac? IdProizvodjacNavigation { get; set; }
}
