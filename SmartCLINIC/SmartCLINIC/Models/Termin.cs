namespace SmartClinic.Models
{
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;


        public class Termin
        {
            [Key]
            public int TerminId { get; set; }

            [Required]
            public DateTime Datum { get; set; }

            [Required]
            public string Vrijeme { get; set; }

            [Required]
            public StatusTermina Status { get; set; }

            public int PacijentId { get; set; }

            [ForeignKey("PacijentId")]
            public Korisnik? Pacijent { get; set; }

            public int DoktorId { get; set; }

            public int UslugaId { get; set; }

            [ForeignKey("UslugaId")]
            public UslugaKlinike? UslugaKlinike { get; set; }

            public QRKod? QRKod { get; set; }

        public bool JeZakazan()
        {
            return Status == StatusTermina.Zakazan;
        }

        public bool JeOtkazan()
        {
            return Status == StatusTermina.Otkazan;
        }

        public void PromijeniStatus(StatusTermina noviStatus)
        {
            Status = noviStatus;
        }
    }
}
