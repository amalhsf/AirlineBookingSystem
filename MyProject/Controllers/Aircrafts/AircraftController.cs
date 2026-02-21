using Microsoft.AspNetCore.Mvc;
using MyProject.Domain.Aircrafts;

namespace MyProject.Controllers.Aircrafts
{
    public class AircraftController : Controller
    {
        // DI
        private readonly IAircraftRepository _repository;
        public AircraftController(IAircraftRepository repository)
        {
            _repository = repository;
        }

        // First page
        public async Task<IActionResult>Index()
        {
            var aircrafts = await _repository.GetAllAsync();

            return View(aircrafts);
        }

        // Create
        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Create(Aircraft aircraft) 
        { 
            await _repository.AddAsync(aircraft);
            return RedirectToAction("Index");
        }

        //Edit
        public async Task<IActionResult> Edit(int id)
        {
            var aircraft = await _repository.GetByIdAsync(id);
            return View(aircraft);
        }
        [HttpPost]
        public async Task<IActionResult>Edit (Aircraft aircraft)
        {
            await _repository.UpdateAsync(aircraft);
            return RedirectToAction("Index");
        }

        // Delete
        public async Task<IActionResult> Delete(int id)
        {
            var aircraft = await _repository.GetByIdAsync(id);
            return View(aircraft);
        }

        [HttpPost]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            await _repository.DeleteAsync(id);
            return RedirectToAction("Index");
        }

    }
}
