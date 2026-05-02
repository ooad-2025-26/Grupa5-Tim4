namespace SmartClinic.Models
{
    using System.ComponentModel.DataAnnotations;

  
        public class Korisnik
        {
            [Key]
            public int Id { get; set; }

            [Required]
            public string Ime { get; set; }

            [Required]
            public string Prezime { get; set; }

            [Required]
            [EmailAddress]
            public string Email { get; set; }

            [Required]
            public string Lozinka { get; set; }

            public ICollection<Termin>? Termini { get; set; }
        }
}
