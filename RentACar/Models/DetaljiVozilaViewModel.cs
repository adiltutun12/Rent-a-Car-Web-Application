namespace RentACar.Models
{
    public class DetaljiVozilaViewModel
    {
        public Vozilo Vozilo { get; set; }
        public Poslovnica Poslovnica { get; set; }
        public List<Rezervacija> Rezervacije { get; set; }

    }
}
