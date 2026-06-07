using System.ComponentModel.DataAnnotations;

namespace SmartClinic.Models
{
   
        public enum StatusUredjaja
        {
        [Display(Name = "Aktivan")]
        Aktivan,

        [Display(Name = "Neaktivan")]
        Neaktivan,
        [Display(Name = "Kvar")]
        Kvar
        }
  
}
