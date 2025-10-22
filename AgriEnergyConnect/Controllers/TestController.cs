using Microsoft.AspNetCore.Mvc;

namespace AgriEnergyConnect.Controllers
{
    public class TestController : Controller
    {
        [HttpGet]
        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(string name, string email, string message)
        {
            // This line proves the POST was received
            ViewBag.Result = $"POST received! Name: {name}, Email: {email}, Message: {message}";
            return View();
        }
    }
}
