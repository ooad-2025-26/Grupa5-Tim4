namespace SmartClinic.Models
{
    using System.ComponentModel.DataAnnotations;

        public class SistemZaSkeniranjeQRKoda
        {
            [Key]
            public int UredjajId { get; set; }

            [Required]
            public StatusUredjaja StatusUredjaja { get; set; }

            [Required]
            public string LokacijaUredjaja { get; set; }

        public bool JeAktivan()
        {
            return StatusUredjaja == StatusUredjaja.Aktivan;
        }
    }
}
