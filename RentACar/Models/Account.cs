using Microsoft.AspNetCore.Identity;

namespace RentACar.Models
{
    public class Account: IdentityUser
    {
        public String Ime { get; set; }
        public string Prezime { get; set; }
        public string BrojTelefona { get; set; }
        public Account() { }
    }
}
