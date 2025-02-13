using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace RentACar.Models
{
    public class Vozilo
    {
        [Key]
        public int Id { get; set; }
        [Required]

        public String Proizvodjac { get; set; }
        [Required]

        public String Model { get; set; }
        [Required]

        public double Cijena { get; set; }
        [Required]

        public String Slika { get; set; }
        [Required]

        public String Opis { get; set; }
        [Required]

        public TipVozila Tip { get; set; }
        [Required]

        public String RegistarskeTablice { get; set; }

        public bool Navigacija { get; set; }
        [Required]

        public Transmisija Transmisija { get; set; }
        [Required]


        public VrstaGoriva Gorivo { get; set; }
        


        [ForeignKey("Poslovnica")]
        public Poslovnica? MaticnaPoslovnicaId { get; set; }
        public bool Dostupno { get; set; }
        public PutnickiTip? PutnickoVozilo_Tip { get; set; }
        public int? BrojSjedista { get; set; }
        public bool? Tempomat { get; set; }
        public TransportniTip? TransportnoVozilo_Tip { get; set; }
        public double? Nosivost { get; set; }
        public double? Duzina { get; set; }
        public bool? Prikolica { get; set; }

        public Vozilo() { }
    }
}
