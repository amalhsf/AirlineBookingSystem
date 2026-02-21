using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using MyProject.Domain.Bookings;
using MyProject.Domain.Flights;
using MyProject.Infrastructure.Repositories;

namespace MyProject.Web.Controllers
{
    public class BookingController : Controller
    {
        private readonly IBookingRepository _bookingRepository;
        private readonly IFlightRepository _flightRepository;

        public BookingController(
            IBookingRepository bookingRepository,
            IFlightRepository flightRepository)
        {
            _bookingRepository = bookingRepository;
            _flightRepository = flightRepository;
        }

        // First page
        public async Task<IActionResult> Index()
        {
            var bookings = await _bookingRepository.GetAllAsync();
            return View(bookings);
        }

        // Create
        public async Task<IActionResult> Create()
        {
            await LoadFlights();

            var booking = new Booking
            {
                BookingDate = DateTime.Now,
                Status = BookingStatus.Pending,
                UserId = 1
            };

            return View(booking);
        }


        [HttpPost]
        public async Task<IActionResult> Create(Booking booking)
        {
            var flight = await _flightRepository.GetByIdAsync(booking.FlightId);

            booking.UserId = 1; // Temporarily
            booking.BookingDate = DateTime.Now;
            booking.TotalPrice = flight.Price;
            booking.Status = BookingStatus.Pending;

            await _bookingRepository.AddAsync(booking);

            return RedirectToAction(nameof(Index));
        }


        // Confirm
        [HttpPost]
        public async Task<IActionResult> Confirm(int id)
        {
            var booking = await _bookingRepository.GetByIdAsync(id);

            booking.Status = BookingStatus.Confirmed;

            await _bookingRepository.UpdateAsync(booking);

            return RedirectToAction(nameof(Index));
        }

        // Cancel
        [HttpPost]
        public async Task<IActionResult> Cancel(int id)
        {
            var booking = await _bookingRepository.GetByIdAsync(id);

            booking.Status = BookingStatus.Cancelled;

            await _bookingRepository.UpdateAsync(booking);

            return RedirectToAction(nameof(Index));
        }


        // ================= HELPER =================
        private async Task LoadFlights()
        {
            var flights = await _flightRepository.GetAllAsync();

            ViewBag.FlightList = new SelectList(
                flights,
                "Id",
                "FlightNumber");
        }
    }
}
