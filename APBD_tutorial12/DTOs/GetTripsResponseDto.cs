namespace APBD_tutorial12.DTOs
{
    public class GetTripsResponseDto
    {
        public int PageNum { get; set; }
        public int PageSize { get; set; }
        public int AllPages { get; set; }
        public List<TripDto> Trips { get; set; }
    }
}