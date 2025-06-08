using APBD_tutorial12.DTOs;
using APBD_tutorial12.Models;

namespace APBD_tutorial12.Services
{
    public interface ITripService
    {
        GetTripsResponseDto GetTrips(int page, int pageSize);
        Trip GetTripById(int id);
    }
}