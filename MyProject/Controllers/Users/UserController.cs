using Microsoft.AspNetCore.Mvc;
using MyProject.Domain.Users;

namespace MyProject.Controllers.Users
{
    public class UserController : Controller
    {
        // DI
        private readonly IUserRepository _repository;
        public UserController(IUserRepository repository)
        {
            _repository = repository;
        }

        // First page
        public async Task<IActionResult> Index()
        {
            var users = await _repository.GetAllAsync();
            return View(users);
        }

        // Create
        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Create(User user)
        {
            await _repository.AddAsync(user);
            return RedirectToAction("Index");
        }

        //Edit
        public async Task<IActionResult> Edit(int id)
        {
            var user = await _repository.GetByIdAsync(id);
            return View(user);
        }
        [HttpPost]
        public async Task<IActionResult> Edit(User user)
        {
            await _repository.UpdateAsync(user);
            return RedirectToAction("Index");
        }

        // Delete
        public async Task<IActionResult> Delete(int id)
        {
            var user = await _repository.GetByIdAsync(id);
            return View(user);
        }

        [HttpPost]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            await _repository.DeleteAsync(id);
            return RedirectToAction("Index");
        }
    }
}
