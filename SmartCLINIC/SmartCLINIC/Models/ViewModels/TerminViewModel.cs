using Microsoft.AspNetCore.Mvc.Rendering;
using System.ComponentModel.DataAnnotations;

namespace SmartClinic.Models.ViewModels
{
    public class TerminViewModel
    {

        [Display(Name = "Medicinska usluga")]
        [Required(ErrorMessage = "Odaberite uslugu.")]
        public int UslugaId { get; set; }

        [Display(Name = "Doktor")]
        [Required(ErrorMessage = "Odaberite doktora.")]
        public string DoktorId { get; set; } = "";

        [Display(Name = "Datum termina")]
        [Required(ErrorMessage = "Odaberite datum.")]
        public DateTime Datum { get; set; }

        [Display(Name = "Vrijeme termina")]
        [Required(ErrorMessage = "Odaberite vrijeme.")]
        public string Vrijeme { get; set; } = "";

        [Display(Name = "Status termina")]
        [EnumDataType(typeof(StatusTermina))]
        public StatusTermina Status { get; set; }


        public List<SelectListItem> Doktori { get; set; } = new();
        public List<SelectListItem> Usluge { get; set; } = new();
        public List<SelectListItem> VrijemeOpcije { get; set; } = new();
    }
}