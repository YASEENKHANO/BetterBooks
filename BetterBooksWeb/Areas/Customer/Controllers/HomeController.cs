using System.Diagnostics;
using System.Net.Http.Headers;
using BetterBooks.Models;
using Microsoft.AspNetCore.Mvc;

namespace BetterBooksWeb.Areas.Customer.Controllers
{
    [Area("Customer")] // explicitly specifying the controller route
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;

        public HomeController(ILogger<HomeController> logger)
        {
            _logger = logger;
        }

        public IActionResult Index()
        {
            return View();
        }



        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
