using Microsoft.AspNetCore.Mvc;
using MyProject.Domain.Airports;

namespace MyProject.Controllers.Airports
{
    public class AirportController : Controller
    {
        // DI
        private readonly IAirportRepository _repository;
        public AirportController(IAirportRepository repository)
        {
            _repository = repository;
        }

        // First page
        public async Task<IActionResult> Index()
        {
            var airports = await _repository.GetAllAsync();

            return View(airports);
        }

        // Create
        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Create(Airport airport)
        {
            await _repository.AddAsync(airport);
            return RedirectToAction("Index");
        }

        // Edit
        public async Task<IActionResult> Edit(int id)
        {
            var airport = await _repository.GetByIdAsync(id);
            return View(airport);
        }
        [HttpPost]
        public async Task<IActionResult> Edit(Airport airport)
        {
            await _repository.UpdateAsync(airport);
            return RedirectToAction("Index");
        }

        // Delete
        public async Task<IActionResult> Delete(int id)
        {
            var airport = await _repository.GetByIdAsync(id);
            return View(airport);
        }

        [HttpPost]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            await _repository.DeleteAsync(id);
            return RedirectToAction("Index");
        }
    }
}
