using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace RentACar.Models
{
    public class Rezervacija
    {
        [Key]
        public int Id { get; set; }
        public DateTime DatumRezervacije { get; set; }
        public DateTime DatumPreuzimanja { get; set; }
        public DateTime DatumPovratka { get; set; }
        public double Iznos { get; set; }

        [ForeignKey("Vozilo")]
        public int VoziloId { get; set; }
        public Account Narucilac {  get; set; }
        public VrstaPlacanja VrstaPlacanja { get; set; }
        

        public Rezervacija() { }
    }
}
