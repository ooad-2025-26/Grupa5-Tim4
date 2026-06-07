
using System.ComponentModel.DataAnnotations;
namespace SmartClinic.Models
{
    public enum StatusTermina
    {
        [Display(Name = "Zakazan")]
        Zakazan,

        [Display(Name = "Otkazan")]
        Otkazan,

        [Display(Name = "Realizovan")]
        Realizovan,

        [Display(Name = "Pacijent prisutan")]
        PacijentPrisutan
    }
}