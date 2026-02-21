using Microsoft.AspNetCore.Mvc;
using MyProject.Models;
using System.Diagnostics;

namespace TuwaiqProjects.Controllers
{
    public class HomeController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }

    }
}
