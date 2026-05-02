namespace SmartClinic.Models
{
    using System.ComponentModel.DataAnnotations;

        public class Raspored
        {
            [Key]
            public int RasporedId { get; set; }

            [Required]
            public DateTime Datum { get; set; }

            [Required]
            public string PocetakSmjene { get; set; }

            [Required]
            public string KrajSmjene { get; set; }

            [Required]
            public int DoktorId { get; set; }

        public bool JeUnutarSmjene(TimeSpan vrijeme)
        {
            return vrijeme >= TimeSpan.Parse(PocetakSmjene) &&
                   vrijeme <= TimeSpan.Parse(KrajSmjene);
        }
    }
  
}
