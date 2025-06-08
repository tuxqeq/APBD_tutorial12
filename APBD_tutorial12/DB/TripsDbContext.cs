using APBD_tutorial12.Models;
using Microsoft.EntityFrameworkCore;

namespace APBD_tutorial12.DB
{
    public class TripsDbContext : DbContext
    {
        public TripsDbContext(DbContextOptions<TripsDbContext> options) : base(options) { }

        protected TripsDbContext()
        {
        }

        public DbSet<Trip> Trips { get; set; }
        public DbSet<Client> Clients { get; set; }
        public DbSet<Country> Countries { get; set; }
        public DbSet<ClientTrip> ClientTrips { get; set; }
        public DbSet<CountryTrip> CountryTrips { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<ClientTrip>()
                .HasKey(ct => new { ct.IdClient, ct.IdTrip });

            modelBuilder.Entity<CountryTrip>()
                .HasKey(ct => new { ct.IdCountry, ct.IdTrip });

            modelBuilder.Entity<ClientTrip>()
                .HasOne(ct => ct.Client)
                .WithMany(c => c.ClientTrips)
                .HasForeignKey(ct => ct.IdClient);

            modelBuilder.Entity<ClientTrip>()
                .HasOne(ct => ct.Trip)
                .WithMany(t => t.ClientTrips)
                .HasForeignKey(ct => ct.IdTrip);

            modelBuilder.Entity<CountryTrip>()
                .HasOne(ct => ct.Country)
                .WithMany(c => c.CountryTrips)
                .HasForeignKey(ct => ct.IdCountry);

            modelBuilder.Entity<CountryTrip>()
                .HasOne(ct => ct.Trip)
                .WithMany(t => t.CountryTrips)
                .HasForeignKey(ct => ct.IdTrip);
        }
    }
}