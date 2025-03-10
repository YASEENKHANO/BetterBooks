using System.Diagnostics;
using System.Net.Http.Headers;
using System.Security.Claims;
using BetterBooks.DataAccess.Repository.IRepository;
using BetterBooks.Models;
using BetterBooks.Models.ViewModels;
using BetterBooks.Utitlity;
using Microsoft.AspNetCore.Authorization;
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

        public IActionResult Details(int productId)
        {
            //retrive one of the Products using ShoppingCart View model
            ShoppingCart cartObj = new() {
                Count = 1,
                ProductId = productId,
                Product = _unitOfWork.Product.GetFirstOrDefault(u => u.Id == productId, includeProperties: "Category,CoverType")
            };


            return View(cartObj);
        }



        [HttpPost]
        [AutoValidateAntiforgeryToken]
        [Authorize]

        public IActionResult Details(ShoppingCart shoppingCart)
        {
            var claimsIdentity = (ClaimsIdentity)User.Identity;

            var claim = claimsIdentity.FindFirst(ClaimTypes.NameIdentifier);

            shoppingCart.ApplicationUserId = claim.Value;

            //trying to append the previous value in the shoping cart based on new count
            ShoppingCart cartFromDb = _unitOfWork.ShoppingCart.GetFirstOrDefault(u => u.ApplicationUserId == claim.Value && u.ProductId==shoppingCart.ProductId);

            if (cartFromDb == null) { 
            
                _unitOfWork.ShoppingCart.Add(shoppingCart);

                _unitOfWork.Save();

                //here we are storing the count of the shopping cart in the session
                //for every set session we will have a get session
                HttpContext.Session.SetInt32(SD.SessionCart, _unitOfWork.ShoppingCart.GetAll(u => u.ApplicationUserId == shoppingCart.ApplicationUserId).ToList().Count);
                //we will show the count of the shopping cart in the navbar in the _layout.cshtml

            }
            else
            {
                _unitOfWork.ShoppingCart.IncrementCount(cartFromDb, shoppingCart.Count);
            }


           
            _unitOfWork.Save();


          //  return RedirectToAction("Index"); //It is using Magic string as Index

            return RedirectToAction(nameof(Index)); //here we are using nameof 'a helper method'
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
