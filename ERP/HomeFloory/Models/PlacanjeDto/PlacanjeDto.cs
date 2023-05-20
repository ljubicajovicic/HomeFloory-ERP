namespace HomeFloory.Models.PlacanjeDto
{
    public class PlacanjeDto
    {
        public decimal IdPlacanje { get; set; }

        public string? Status { get; set; }

        public DateTime? Datum { get; set; }

        public decimal IdKorisnik { get; set; }
    }
}
