using APBD_tutorial12.DB;
using APBD_tutorial12.DTOs;
using APBD_tutorial12.Models;
using Microsoft.EntityFrameworkCore;

namespace APBD_tutorial12.Services
{
    public class TripService : ITripService
    {
        private readonly TripsDbContext _context;

        public TripService(TripsDbContext context)
        {
            _context = context;
        }

        public GetTripsResponseDto GetTrips(int page, int pageSize)
        {
            var query = _context.Trips
                .Include(t => t.CountryTrips).ThenInclude(ct => ct.Country)
                .Include(t => t.ClientTrips).ThenInclude(ct => ct.Client)
                .OrderByDescending(t => t.DateFrom);

            int totalCount = query.Count();
            int allPages = (int)Math.Ceiling(totalCount / (double)pageSize);

            var trips = query
                .Skip((page - 1) * pageSize)
                .Take(pageSize)
                .Select(t => new TripDto
                {
                    Name = t.Name,
                    Description = t.Description,
                    DateFrom = t.DateFrom,
                    DateTo = t.DateTo,
                    MaxPeople = t.MaxPeople,
                    Countries = t.CountryTrips.Select(ct => new CountryDto { Name = ct.Country.Name }).ToList(),
                    Clients = t.ClientTrips.Select(ct => new ClientDto { FirstName = ct.Client.FirstName, LastName = ct.Client.LastName }).ToList()
                })
                .ToList();

            return new GetTripsResponseDto
            {
                PageNum = page,
                PageSize = pageSize,
                AllPages = allPages,
                Trips = trips
            };
        }

        public Trip GetTripById(int id)
        {
            return _context.Trips.Find(id);
        }
    }
}