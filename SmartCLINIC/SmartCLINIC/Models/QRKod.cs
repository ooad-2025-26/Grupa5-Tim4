namespace SmartClinic.Models
{
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;

        public class QRKod
        {
            [Key]
            public int QRKodId { get; set; }

            [Required]
            public string VrijednostKoda { get; set; }

            [Required]
            public DateTime DatumGenerisanja { get; set; }

            public int TerminId { get; set; }

            [ForeignKey("TerminId")]
            public Termin? Termin { get; set; }

        public bool JeValidan()
        {
            return !string.IsNullOrWhiteSpace(VrijednostKoda);
        }
    }
    
}
