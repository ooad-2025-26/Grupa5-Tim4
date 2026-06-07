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

        public string? Specijalizacija { get; set; }

        public ICollection<Termin>? PacijentTermini { get; set; }

        public ICollection<Termin>? DoktorTermini { get; set; }

        public string PunoIme()
        {
            return $"{Ime} {Prezime}";
        }
    }
}
