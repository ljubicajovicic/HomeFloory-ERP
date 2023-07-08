using System.ComponentModel;

namespace HomeFloory.Models.KorisnikDto
{
    public class UpdateKorisnikDto
    {
        public string? Ime { get; set; }

        public string? Prezime { get; set; }

        public DateTime? DatumRodjenja { get; set; }

        public string? Kontakt { get; set; }

        public string? Email { get; set; }

        public string? Lozinka { get; set; }

        [DefaultValue(1)]
        public decimal? IdAdresaIsporuke { get; set; }

        [DefaultValue(1)]
        public decimal IdUloga { get; set; }
    }
}
