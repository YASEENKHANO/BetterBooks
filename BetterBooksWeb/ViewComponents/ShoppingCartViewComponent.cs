using BetterBooks.DataAccess.Repository;
using BetterBooks.DataAccess.Repository.IRepository;
using BetterBooks.Utitlity;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace BetterBooksWeb.ViewComponents
{
    public class ShoppingCartViewComponent : ViewComponent
    {
        //The steps for Dependency Injection are as follows:
        //1. Create a constructor
        //2. Create a private readonly field
        //3. Assign the private readonly field to the constructor parameter
        //4. Create a private readonly field for the repository
        //5. Assign the private readonly field to the repository
        //6. Create a method to get the shopping cart
        //7. Return the view with the shopping cart
        //8. Call the method in the view
        //9. Add the view component to the view
        //10. Add the view component to the layout
      


        //let's get the shopping cart list from the database using the unit of work
        private readonly IUnitOfWork _unitOfWork;
        public ShoppingCartViewComponent( IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork; 
        }

        //during binding this view component to the view, why we name the view Default.cshtml because the view component name is ShoppingCartViewComponent, so the view name should be Default.cshtml, if we name the view as ShoppingCart.cshtml then it will not work


        // we can call this view component in the view by using "<i class="bi bi-cart">&nbsp; (@Model)</i>" .



        //here we are using the invoke async method to get the shopping cart why we use invoke async because we are using the session and session is async
        //IVIewComponentResult is the return type of the invoke async method
        public async Task<IViewComponentResult> InvokeAsync()
        {
            //claims identity is used to get the user identity
            var claimsIdentity = (ClaimsIdentity)User.Identity;
            
            //claim is used to get the claim type
            var claim = claimsIdentity.FindFirst(ClaimTypes.NameIdentifier);

            //count is used to get the count of the shopping cart
            var count = 0;

            //if the claim is not null then we will get the count of the shopping cart
            if (claim != null)
            {
                //if the session is not null then we will get the count of the shopping cart
                if (HttpContext.Session.GetInt32(SD.SessionCart) != null)
                {
                    //we are storing the count of the shopping cart in the session
                    return View(HttpContext.Session.GetInt32(SD.SessionCart));
                }
                else
                {
                    //we are storing the count of the shopping cart in the session but we are getting the count of the shopping cart from the database based on the user id as how many products are there in the shopping cart
                    HttpContext.Session.SetInt32(SD.SessionCart, _unitOfWork.ShoppingCart.GetAll(x => x.ApplicationUserId== claim.Value).ToList().Count); 


                    return View(HttpContext.Session.GetInt32(SD.SessionCart));
                }
                //count = _unitOfWork.ShoppingCart.GetAll(u => u.ApplicationUserId == claim.Value).ToList().Count;
            }
            else
            {
                //this part can hapen when the user is not logged in or the user sign out
                HttpContext.Session.Clear();
                return View(0);

            }
             
            
          
        }


    }
}
