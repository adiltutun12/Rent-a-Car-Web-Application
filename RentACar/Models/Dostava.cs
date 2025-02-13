using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace RentACar.Models
{
    public class Dostava
    {
        [Key]
        public int Id { get; set; }
        [ForeignKey("Rezervacija")]
        public int NarudzbaId { get; set; }
        public Rezervacija Narudzba { get; set; }
        public Account? Dostavljac { get; set; }
        public String Adresa { get; set; }
        public bool Prihvacena { get; set; }

        public Dostava() { }

        public void PrihvatiDostavu()
        {
            Prihvacena = true;
        }

        public void OdbijDostavu()
        {
            Prihvacena = false;
        }
    }
}
