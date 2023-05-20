using System.ComponentModel.DataAnnotations;

namespace HomeFloory.Models.KorisnikDto
{
    public class LoginDto
    {
        [Required]
        [DataType(DataType.EmailAddress)]
        public string? Email { get; set; }
        [Required]
        [DataType(DataType.Password)]
        public string? Lozinka { get; set; }
    }
}
