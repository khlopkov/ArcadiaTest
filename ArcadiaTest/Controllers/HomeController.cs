using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using ArcadiaTest.Models;

namespace ArcadiaTest.Controllers
{
    public class HomeController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel {RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier});
        }

        public ViewResult ErrorNotFound()
        {
            Response.StatusCode = 404;
            return View("Error");
        }
    }
}