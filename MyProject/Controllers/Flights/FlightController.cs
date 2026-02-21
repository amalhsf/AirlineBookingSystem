using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using MyProject.Domain.Aircrafts;
using MyProject.Domain.Airports;
using MyProject.Domain.Flights;
using MyProject.Infrastructure.Repositories;

namespace MyProject.Web.Controllers
{
    public class FlightController : Controller
    {
        private readonly IFlightRepository _flightRepository;
        private readonly IAircraftRepository _aircraftRepository;
        private readonly IAirportRepository _airportRepository;

        public FlightController(
            IFlightRepository flightRepository,
            IAircraftRepository aircraftRepository,
            IAirportRepository airportRepository)
        {
            _flightRepository = flightRepository;
            _aircraftRepository = aircraftRepository;
            _airportRepository = airportRepository;
        }

        // First page 
        public async Task<IActionResult> Index()
        {
            var flights = await _flightRepository.GetAllAsync();
            return View(flights);
        }

        // Create
        public async Task<IActionResult> Create()
        {
            await LoadDropdowns();
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Create(Flight flight)
        {
            await _flightRepository.AddAsync(flight);
            return RedirectToAction(nameof(Index));
        }

        // Edit
        public async Task<IActionResult> Edit(int id)
        {
            var flight = await _flightRepository.GetByIdAsync(id);
            await LoadDropdowns();
            return View(flight);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(Flight flight)
        {
            await _flightRepository.UpdateAsync(flight);
            return RedirectToAction(nameof(Index));
        }

        // Delete
        public async Task<IActionResult> Delete(int id)
        {
            var flight = await _flightRepository.GetByIdAsync(id);
            return View(flight);
        }

        [HttpPost]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            await _flightRepository.DeleteAsync(id);
            return RedirectToAction(nameof(Index));
        }

        // HELPER 
        private async Task LoadDropdowns()
        {
            var aircrafts = await _aircraftRepository.GetAllAsync();
            var airports = await _airportRepository.GetAllAsync();

            ViewBag.AircraftList = new SelectList(aircrafts, "Id", "Model");
            ViewBag.AirportList = new SelectList(airports, "Id", "Name");
        }
    }
}
