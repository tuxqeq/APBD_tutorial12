namespace APBD_tutorial12.Models
{
    public class Trip
    {
        public int IdTrip { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public DateTime DateFrom { get; set; }
        public DateTime DateTo { get; set; }
        public int MaxPeople { get; set; }

        public ICollection<ClientTrip> ClientTrips { get; set; } = new List<ClientTrip>();
        public ICollection<CountryTrip> CountryTrips { get; set; } = new List<CountryTrip>();
    }
}