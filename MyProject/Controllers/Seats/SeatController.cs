using Microsoft.AspNetCore.Mvc;
using MyProject.Domain.Seats;

namespace MyProject.Controllers.Seats
{
    public class SeatController : Controller
    {
        // DI
        private readonly ISeatRepository _repository;
        public SeatController(ISeatRepository repository)
        {
            _repository = repository;
        }

        // First page
        public async Task<IActionResult> Index()
        {
            var seats = await _repository.GetAllAsync();
            return View(seats);
        }

        // Create
        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Create(Seat seat)
        {
            await _repository.AddAsync(seat);
            return RedirectToAction("Index");
        }

        //Edit
        public async Task<IActionResult> Edit(int id)
        {
            var seat = await _repository.GetByIdAsync(id);
            return View(seat);
        }
        [HttpPost]
        public async Task<IActionResult> Edit(Seat seat)
        {
            await _repository.UpdateAsync(seat);
            return RedirectToAction("Index");
        }

        // Delete
        public async Task<IActionResult> Delete(int id)
        {
            var seat = await _repository.GetByIdAsync(id);
            return View(seat);
        }

        [HttpPost]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            await _repository.DeleteAsync(id);
            return RedirectToAction("Index");
        }
    }
}
