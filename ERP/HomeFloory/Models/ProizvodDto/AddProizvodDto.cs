namespace HomeFloory.Models.ProizvodDto
{
    public class AddProizvodDto
    {

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
    }
}
