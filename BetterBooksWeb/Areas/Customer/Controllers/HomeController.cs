using System.Diagnostics;
using System.Net.Http.Headers;
using BetterBooks.DataAccess.Repository.IRepository;
using BetterBooks.Models;
using BetterBooks.Models.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore.Metadata;

namespace BetterBooksWeb.Areas.Customer.Controllers
{
    [Area("Customer")] // explicitly specifying the controller route
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly IUnitOfWork _unitOfWork;
        public HomeController(ILogger<HomeController> logger, IUnitOfWork unitOfWork)
        {
            _logger = logger;
            _unitOfWork = unitOfWork; 
        }

        public IActionResult Index()
        {
            //retrive all of the Products
            IEnumerable<Product> productList= _unitOfWork.Product.GetAll(includeProperties: "Category,CoverType");



            return View(productList);
        }

        public IActionResult Details(int id)
        {
            //retrive one of the Products using ShoppingCart View model
            ShoppingCart cartObj = new() {
                Count = 2,
                Product = _unitOfWork.Product.GetFirstOrDefault(u => u.Id == id, includeProperties: "Category,CoverType")
            };


            return View(cartObj);
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
