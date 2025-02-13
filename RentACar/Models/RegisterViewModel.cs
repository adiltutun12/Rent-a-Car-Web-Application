using System.ComponentModel.DataAnnotations;

namespace RentACar.Models
{
    public class RegisterViewModel
    {
        [Required]
        [EmailAddress]
        public string Email { get; set; }

        [Required]
        [StringLength(100, ErrorMessage = "The {0} must be at least {2} and at max {1} characters long.", MinimumLength = 6)]
        [DataType(DataType.Password)]
        public string Password { get; set; }

        [DataType(DataType.Password)]
        [Display(Name = "Confirm password")]
        [Compare("Password", ErrorMessage = "The password and confirmation password do not match.")]
        public string ConfirmPassword { get; set; }
        [Required]
        [Display(Name = "Ime")]
        public string Ime { get; set; }
        [Required]
        [Display(Name = "Prezime")]
        public string Prezime { get; set; }
        [Required]
        [Phone]
        [Display(Name = "Broj Telefona")]
        public string BrojTelefona { get; set; }
    }

}
