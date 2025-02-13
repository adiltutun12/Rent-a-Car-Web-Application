using System.ComponentModel.DataAnnotations;

namespace RentACar.Models
{
    public class Poslovnica
    {
        [Key]
        public int Id { get; set; }
        public String Adresa { get; set; }
        public String Kontakt { get; set; }
        public String RadnoVrijeme { get; set; }

        public Poslovnica() { }
    }
}
