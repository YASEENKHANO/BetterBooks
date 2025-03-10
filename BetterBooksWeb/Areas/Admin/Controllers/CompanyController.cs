using BetterBooks.DataAccess.Repository.IRepository;
using BetterBooks.Models;
using BetterBooks.Models.ViewModels;
using BetterBooks.Utitlity;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace BetterBooksWeb.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Authorize(Roles = SD.Role_Admin)]
    public class CompanyController : Controller
    {
        private readonly IUnitOfWork _unitOfWork;
          public CompanyController(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
         
        }





        public IActionResult Index()
        {


            return View();

        }

        //Get
        public IActionResult Create()
        {


            return View();
        }



        //Upsert method will be the combination of Update + Insert
        //Get
        public IActionResult Upsert(int? id)
        {

            Company company = new();

            if (id == null || id == 0)
            { 
                return View(company);

            }
            else
            {
               company = _unitOfWork.Company.GetFirstOrDefault(u => u.Id == id);

                return View(company);
            }

        }



        //Post
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Upsert(Company obj)
        {
             
            if (ModelState.IsValid)//model state will check wheather the required fields are populated or not
            {

                    //Add Part 
                    if (obj.Id == 0)
                    {

                        _unitOfWork.Company.Add(obj);
                    TempData["success"] = "Company Created Successfully!";

                }
                else
                    {
                        _unitOfWork.Company.Update(obj);
                    TempData["success"] = "Company Updated Successfully!";

                }


                _unitOfWork.Save();
                   return RedirectToAction("Index");
                }
                return View(obj);
            }

        #region API CALLS
        [HttpGet]
        public IActionResult GetAll(int id)
        {

            var companyList = _unitOfWork.Company.GetAll();


            return Json(new { data = companyList });

        }


        ////Post
        [HttpDelete]
       
        public IActionResult Delete(int? id)
        {
            //var obj= _db.Categories.Find(id); before repository pattern

            var obj = _unitOfWork.Company.GetFirstOrDefault(x => x.Id == id);//it will fetch the first record and check with our given record 'id', 
            if (obj == null)
            {

                return Json(new
                {
                    success=false,message="Error while deleting!"
                });
            }
            
            _unitOfWork.Company.Remove(obj);
            _unitOfWork.Save();

            return Json(new
            {
                success = true,
                message = "Delete Successful!"
            });

        }

        #endregion

    }
}
