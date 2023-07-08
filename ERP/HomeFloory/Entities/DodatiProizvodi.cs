using System;
using System.Collections.Generic;

namespace HomeFloory.Models;

public partial class DodatiProizvodi
{
    public decimal IdDodatiProizvodi { get; set; }
    public decimal IdProizvod { get; set; }

    public decimal IdKorpa { get; set; }

    public decimal? Cena { get; set; }

    public decimal? Kolicina { get; set; }


    public virtual Korpa? IdKorpaNavigation { get; set; } = null!;

    public virtual Proizvod? IdProizvodNavigation { get; set; } = null!;
}
