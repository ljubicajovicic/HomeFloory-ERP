namespace HomeFloory.Models.DodatiProizvodiDto
{
    public class AddDodatiProizvodiDto
    {
        public decimal IdProizvod { get; set; }

        public decimal IdKorpa { get; set; }
        public decimal? Cena { get; set; }

        public decimal? Kolicina { get; set; }

        public decimal? KolicinaPoM2 { get; set; }

    }
}
