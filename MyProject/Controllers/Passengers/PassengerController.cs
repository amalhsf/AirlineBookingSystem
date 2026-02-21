using Microsoft.AspNetCore.Mvc;
using MyProject.Domain.Passengers;

namespace MyProject.Controllers.Passengers
{
    public class PassengerController : Controller
    {
        // DI
        private readonly IPassengerRepository _repository;
        public PassengerController(IPassengerRepository repository)
        {
            _repository = repository;
        }

        // First page
        public async Task<IActionResult> Index()
        {
            var passengers = await _repository.GetAllAsync();
            return View(passengers);
        }

        // Create
        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Create(Passenger passenger)
        {
            await _repository.AddAsync(passenger);
            return RedirectToAction("Index");
        }

        //Edit
        public async Task<IActionResult> Edit(int id)
        {
            var passenger = await _repository.GetByIdAsync(id);
            return View(passenger);
        }
        [HttpPost]
        public async Task<IActionResult> Edit(Passenger passenger)
        {
            await _repository.UpdateAsync(passenger);
            return RedirectToAction("Index");
        }

        // Delete
        public async Task<IActionResult> Delete(int id)
        {
            var passenger = await _repository.GetByIdAsync(id);
            return View(passenger);
        }

        [HttpPost]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            await _repository.DeleteAsync(id);
            return RedirectToAction("Index");
        }
    }
}
