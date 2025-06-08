using APBD_tutorial12.DTOs;

namespace APBD_tutorial12.Services
{
    public interface IClientService
    {
        bool DeleteClient(int id);
        bool HasTrips(int clientId);
        void RegisterClientToTrip(int tripId, NewClientDto dto);
    }
}