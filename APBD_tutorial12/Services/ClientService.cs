using APBD_tutorial12.DB;
using APBD_tutorial12.DTOs;
using APBD_tutorial12.Models;

namespace APBD_tutorial12.Services
{
    public class ClientService : IClientService
    {
        private readonly TripsDbContext _context;

        public ClientService(TripsDbContext context)
        {
            _context = context;
        }

        public bool HasTrips(int clientId)
        {
            return _context.ClientTrips.Any(ct => ct.IdClient == clientId);
        }

        public bool DeleteClient(int id)
        {
            var client = _context.Clients.Find(id);
            if (client == null) return false;
            if (HasTrips(id)) throw new InvalidOperationException("Client has assigned trips");
            _context.Clients.Remove(client);
            _context.SaveChanges();
            return true;
        }

        public void RegisterClientToTrip(int tripId, NewClientDto dto)
        {
            var trip = _context.Trips.Find(tripId);
            if (trip == null) throw new KeyNotFoundException("Trip not found");
            if (trip.DateFrom <= DateTime.Now) throw new InvalidOperationException("Trip has already started");
            if (_context.Clients.Any(c => c.Pesel == dto.Pesel))
                throw new InvalidOperationException("Client with given PESEL already exists");

            var client = new Client
            {
                FirstName = dto.FirstName,
                LastName = dto.LastName,
                Email = dto.Email,
                Telephone = dto.Telephone,
                Pesel = dto.Pesel
            };
            _context.Clients.Add(client);
            _context.SaveChanges();

            if (_context.ClientTrips.Any(ct => ct.IdTrip == tripId && ct.IdClient == client.IdClient))
                throw new InvalidOperationException("Client is already registered for this trip");

            var assignment = new ClientTrip
            {
                IdClient = client.IdClient,
                IdTrip = tripId,
                RegisteredAt = DateTime.Now,
                PaymentDate = dto.PaymentDate
            };
            _context.ClientTrips.Add(assignment);
            _context.SaveChanges();
        }
    }
}