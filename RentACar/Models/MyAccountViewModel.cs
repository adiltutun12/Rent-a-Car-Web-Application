using System.ComponentModel.DataAnnotations;

namespace RentACar.Models
{
    public class MyAccountViewModel
    {
        [Display(Name = "Ime")]
        public string Ime { get; set; }

        [Display(Name = "Prezime")]
        public string Prezime { get; set; }

        [Display(Name = "Datum rođenja")]
        [DataType(DataType.Date)]
        public DateTime DatumRodjenja { get; set; }

        [Display(Name = "Broj telefona")]
        public string BrojTelefona { get; set; }

        [Display(Name = "Država")]
        public string Drzava { get; set; }

        [Display(Name = "Adresa")]
        public string Adresa { get; set; }

        [Display(Name = "Email")]
        public string Email { get; set; }
    }
}
