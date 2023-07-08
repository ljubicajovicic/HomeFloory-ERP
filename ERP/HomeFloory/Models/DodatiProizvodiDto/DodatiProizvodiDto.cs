namespace HomeFloory.Models.DodatiProizvodiDto
{
    public class DodatiProizvodiDto
    {

        public decimal IdDodatiProizvodi { get; set; }

        public decimal IdProizvod { get; set; }

        public decimal IdKorpa { get; set; }

        public decimal? Cena { get; set; }

        public decimal? Kolicina { get; set; }


        public virtual Korpa? IdKorpaNavigation { get; set; } = null!;
        public virtual Proizvod IdProizvodNavigation { get; set; } = null!;
    }
}
