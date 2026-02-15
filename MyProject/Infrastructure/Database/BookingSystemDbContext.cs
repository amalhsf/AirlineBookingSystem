using Microsoft.EntityFrameworkCore;
using MyProject.Domain.Aircraft;
using MyProject.Domain.Airport;
using MyProject.Domain.Booking;
using MyProject.Domain.Flight;
using MyProject.Domain.Passenger;
using MyProject.Domain.Seat;
using MyProject.Domain.User;

namespace MyProject.Infrastructure.Database
{
    public class BookingSystemDbContext : DbContext
    {
        public BookingSystemDbContext(DbContextOptions<BookingSystemDbContext> options) : base(options)
        {
        }

        public DbSet<Aircraft> Aircrafts { get; set; }
        public DbSet<Airport> Airports { get; set; }
        public DbSet<Booking> Bookings { get; set; }
        public DbSet<Flight> Flights { get; set; }
        public DbSet<Passenger> Passengers { get; set; }
        public DbSet<Seat> Seats { get; set; }
        public DbSet<User> Users { get; set; }

        // how to implement relationship + enum?
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.Entity<Aircraft>(aircraft =>
            {
                aircraft.HasKey(a => a.Id);
                aircraft.Property(a => a.Model).IsRequired();
                aircraft.Property(a => a.TotalSeats).IsRequired();
            })
            modelBuilder.Entity<Airport>(airport =>
            {
                airport.HasKey(a => a.Id);
                airport.Property(a => a.Name).IsRequired();
                airport.Property(a => a.City).IsRequired();
                airport.Property(a => a.Country).IsRequired();
            })
            modelBuilder.Entity<Booking>(booking =>
            {
                booking.HasKey(b => b.Id);
                booking.Property(b => b.UserId).IsRequired();
                booking.Property(b => b.FlightId).IsRequired();
                booking.Property(b => b.BookingDate).IsRequired();
                booking.Property(b => b.TotalPrice).IsRequired();
                // Status enum
            })
            modelBuilder.Entity<Flight>(flight =>
            {
                flight.HasKey(f => f.Id);
                flight.Property(f => f.FlightNumber).IsRequired();
                flight.Property(f => f.DepartureAirportId).IsRequired();
                flight.Property(f => f.ArrivalAirportId).IsRequired();
                flight.Property(f => f.DepartureTime).IsRequired();
                flight.Property(f => f.ArrivalTime).IsRequired();
                flight.Property(f => f.AircraftId).IsRequired();
                flight.Property(f => f.Price).IsRequired();
            })
            modelBuilder.Entity<Passenger>(passenger =>
            {
                passenger.HasKey(p => p.Id);
                passenger.Property(p => p.BookingId).IsRequired();
                passenger.Property(p => p.Name).IsRequired();
                passenger.Property(p => p.Age).IsRequired();
                passenger.Property(p => p.NationalId).IsRequired();
            })
            modelBuilder.Entity<Seat>(seat =>
            {
                seat.HasKey(s => s.Id);
                seat.Property(s => s.SeatNumber).IsRequired();
                seat.Property(s => s.IsBooked).IsRequired();
            })
            modelBuilder.Entity<User>(user =>
            {
                user.HasKey(u => u.Id);
                user.Property(u => u.Name).IsRequired();
                user.Property(u => u.Email).IsRequired();
                user.Property(u => u.Password).IsRequired();
            })
        }

    }
}
