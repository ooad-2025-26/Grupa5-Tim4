using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations;

namespace SmartClinic.Models
{
    public class Korisnik : IdentityUser
    {
        [Required]
        public string Ime { get; set; }

        [Required]
        public string Prezime { get; set; }

        public string? Uloga { get; set; }

        public ICollection<Termin>? Termini { get; set; }

        public string PunoIme()
        {
            return $"{Ime} {Prezime}";
        }
    }
}
