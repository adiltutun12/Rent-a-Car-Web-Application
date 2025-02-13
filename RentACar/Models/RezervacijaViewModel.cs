namespace RentACar.Models
{
    public class RezervacijaViewModel
    {
        public DateTime DatumPreuzimanja { get; set; }
        public DateTime DatumPovratka { get; set; }
        public double Iznos { get; set; }
        public int VoziloId { get; set; }
        public string VrstaPlacanja { get; set; }
        public DostavaViewModel? Dostava { get; set; }
    }
}
