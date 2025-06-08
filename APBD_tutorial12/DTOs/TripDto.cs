namespace APBD_tutorial12.DTOs
{
    public class TripDto
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public DateTime DateFrom { get; set; }
        public DateTime DateTo { get; set; }
        public int MaxPeople { get; set; }
        public List<CountryDto> Countries { get; set; }
        public List<ClientDto> Clients { get; set; }
    }
}