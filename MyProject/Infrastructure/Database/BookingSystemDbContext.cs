using Microsoft.EntityFrameworkCore;
using MyProject.Domain.Aircrafts;
using MyProject.Domain.Airports;
using MyProject.Domain.Bookings;
using MyProject.Domain.Flights;
using MyProject.Domain.Passengers;
using MyProject.Domain.Seats;
using MyProject.Domain.Users;

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


        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.Entity<Aircraft>(aircraft =>
            {
                aircraft.HasKey(a => a.Id);
                aircraft.Property(a => a.Model).IsRequired();
                aircraft.Property(a => a.TotalSeats).IsRequired();
            });

            modelBuilder.Entity<Airport>(airport =>
            {
                airport.HasKey(a => a.Id);
                airport.Property(a => a.Name).IsRequired();
                airport.Property(a => a.City).IsRequired();
                airport.Property(a => a.Country).IsRequired();
                airport.Property(a => a.Code).IsRequired();
            });

            modelBuilder.Entity<Booking>(booking =>
            {
                booking.HasKey(b => b.Id);
                booking.Property(b => b.UserId).IsRequired();
                booking.Property(b => b.FlightId).IsRequired();
                booking.Property(b => b.BookingDate).IsRequired();
                booking.Property(b => b.TotalPrice).IsRequired();
                booking.Property(b => b.Status).IsRequired().HasConversion<string>(); ;
                booking.HasOne(b => b.User)
                        .WithMany(u => u.Bookings)
                        .HasForeignKey(b => b.UserId)
                        .OnDelete(DeleteBehavior.Cascade);
                booking.HasOne(b => b.Flight)
                        .WithMany(f => f.Bookings)
                        .HasForeignKey(b => b.FlightId)
                        .OnDelete(DeleteBehavior.Restrict);
            });

            modelBuilder.Entity<Flight>(flight =>
            {
                flight.HasKey(f => f.Id);
                flight.Property(f => f.FlightNumber).IsRequired().HasMaxLength(10); ;
                flight.Property(f => f.DepartureAirportId).IsRequired();
                flight.Property(f => f.ArrivalAirportId).IsRequired();
                flight.Property(f => f.DepartureTime).IsRequired();
                flight.Property(f => f.ArrivalTime).IsRequired();
                flight.Property(f => f.AircraftId).IsRequired();
                flight.Property(f => f.Price).IsRequired();
                flight.HasOne(f => f.Aircraft)
                        .WithMany(a => a.Flights)
                        .HasForeignKey(f => f.AircraftId)
                        .OnDelete(DeleteBehavior.Restrict);
                flight.HasOne(f => f.DepartureAirport)
                        .WithMany(a => a.DepartureFlights)
                        .HasForeignKey(f => f.DepartureAirportId)
                        .OnDelete(DeleteBehavior.Restrict);
                flight.HasOne(f => f.ArrivalAirport)
                        .WithMany(a => a.ArrivalFlights)
                        .HasForeignKey(f => f.ArrivalAirportId)
                        .OnDelete(DeleteBehavior.Restrict);
            });

            modelBuilder.Entity<Passenger>(passenger =>
            {
                passenger.HasKey(p => p.Id);
                passenger.Property(p => p.BookingId).IsRequired();
                passenger.Property(p => p.FirstName).IsRequired();
                passenger.Property(p => p.LastName).IsRequired();
                passenger.Property(p => p.DateOfBirth).IsRequired();
                passenger.Property(p => p.PassportNumber).IsRequired().HasMaxLength(10); ;
                passenger.Property(p => p.SeatId).IsRequired();
                passenger.HasOne(p => p.Booking)
                            .WithMany(b => b.Passengers)
                            .HasForeignKey(p => p.BookingId)
                            .OnDelete(DeleteBehavior.Cascade);
                passenger.HasOne(p => p.Seat)
                            .WithOne(s => s.Passenger)
                            .HasForeignKey<Passenger>(p => p.SeatId)
                            .OnDelete(DeleteBehavior.Restrict);
            });

            modelBuilder.Entity<Seat>(seat =>
            {
                seat.HasKey(s => s.Id);
                seat.Property(s => s.SeatNumber).IsRequired();
                seat.Property(s => s.SeatClass).IsRequired().HasConversion<string>();
                seat.HasOne(s => s.Flight)
                    .WithMany(f => f.Seats)
                    .HasForeignKey(s => s.FlightId)
                    .OnDelete(DeleteBehavior.Cascade);
                seat.HasIndex(s => new { s.FlightId, s.SeatNumber }).IsUnique();
            });

            modelBuilder.Entity<User>(user =>
            {
                user.HasKey(u => u.Id);
                user.Property(u => u.Name).IsRequired();
                user.Property(u => u.Email).IsRequired();
                user.HasIndex(u => u.Email).IsUnique();
                user.Property(u => u.PasswordHash).IsRequired();
                user.Property(u => u.UserRole).IsRequired().HasConversion<string>(); ;
            });
        }

    }
}
