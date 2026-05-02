namespace SmartClinic.Models
{
    using System.ComponentModel.DataAnnotations;


    using System.ComponentModel.DataAnnotations;

    
        public class UslugaKlinike
        {
            [Key]
            public int UslugaId { get; set; }

            [Required]
            public string Naziv { get; set; }

            public string? Opis { get; set; }

            [Required]
            public int TrajanjeUsluge { get; set; }

            [Required]
            public double Cijena { get; set; }

            public ICollection<Termin>? Termini { get; set; }

        public string PrikaziOsnovneInformacije()
        {
            return $"{Naziv} - {TrajanjeUsluge} min - {Cijena} KM";
        }
    }
   

}
