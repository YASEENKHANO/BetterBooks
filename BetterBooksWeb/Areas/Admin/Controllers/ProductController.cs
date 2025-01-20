using BetterBooks.DataAccess.Repository.IRepository;
using BetterBooks.Models;
using BetterBooks.Models.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace BetterBooksWeb.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class ProductController : Controller
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IWebHostEnvironment _hostEnvironment; //to add the file to the folder
        public ProductController(IUnitOfWork unitOfWork, IWebHostEnvironment hostEnvironment)
        {
            _unitOfWork = unitOfWork;
            _hostEnvironment = hostEnvironment;
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

            ProductVM productVM = new()
            {
                Product = new(),

                CategoryList = _unitOfWork.Category.GetAll().Select(
               i => new SelectListItem
               {
                   Text = i.Name,
                   Value = i.Id.ToString()
               }),
                CoverTypeList = _unitOfWork.CoverType.GetAll().Select(
              i => new SelectListItem
              {
                  Text = i.Name,
                  Value = i.Id.ToString()
              })

            };

            if (id == null || id == 0)
            {
                //Create new Product logic here

                //ViewBag.CategoryList = CategoryList; //'CategoryList' is the name of ViewBag
                //ViewData["CoverTypeList"] = CoverTypeList;


                return View(productVM);

            }
            else
            {
                //Update product here
               // productVM.Product = _unitOfWork.Product.GetFirstOrDefault(u=> u.Id == id);
                productVM.Product = _unitOfWork.Product.GetFirstOrDefault(u => u.Id == id);

                return View(productVM);
            }

        }



        //Post
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Upsert(ProductVM obj, IFormFile? file)
        {

            if (ModelState.IsValid)//model state will check wheather the required fields are populated or not
            {

                string wwwRootPath = _hostEnvironment.WebRootPath;
                if (file != null)
                {
                    string fileName = Guid.NewGuid().ToString();

                    var uploads = Path.Combine(wwwRootPath, @"images\products\");
                    var extension = Path.GetExtension(file.FileName);

                    //deleting the previous image during update
                    if (obj.Product.ImageUrl != null) { 
                    
                        var oldImagePath= Path.Combine(wwwRootPath,obj.Product.ImageUrl.TrimStart('\\'));

                        if (System.IO.File.Exists(oldImagePath)) { 
                            System.IO.File.Delete(oldImagePath);
                        
                        }
                    }


                    //copying the image from the specified location
                    using (var fileStreams = new FileStream(Path.Combine(uploads, fileName + extension), FileMode.Create))
                    {

                        file.CopyTo(fileStreams);

                        //updating the image URl
                        obj.Product.ImageUrl = @"\images\products\" + fileName + extension;

                    }

                    //Add Part 
                    if (obj.Product.Id == 0)
                    {

                        _unitOfWork.Product.Add(obj.Product);

                    }
                    else
                    {
                        _unitOfWork.Product.Update(obj.Product);
                    }


                    _unitOfWork.Save();
                    TempData["success"] = "Product Updated Successfully!";
                    return RedirectToAction("Index");
                }
                return View(obj);
            }
            return View(obj);
        }







        //For Edit Below
        //Get
        //public IActionResult Delete(int? id)
        //{
        //    if (id == null || id == 0)
        //    {
        //        return NotFound();

        //    }

        //    //  var categoryFromDB = _db.Categories.Find(id);//it will try to find it

        //    var ProductFromDBFirst = _unitOfWork.Product.GetFirstOrDefault(x => x.Id == id);//it will fetch the first record and check with our given record 'id', 
        //    //  var categoryFromDBSingle = _db.Categories.SingleOrDefault(x => x.Id == id);

        //    if (ProductFromDBFirst == null)
        //    {

        //        return NotFound();
        //    }

        //    return View(ProductFromDBFirst);
        //}





       


        #region API CALLS
        [HttpGet]
        public IActionResult GetAll(int id)
        {

            var productList = _unitOfWork.Product.GetAll(includeProperties:"Category,CoverType");


            return Json(new { data = productList });

        }


        ////Post
        [HttpDelete]
       
        public IActionResult Delete(int? id)
        {
            //var obj= _db.Categories.Find(id); before repository pattern

            var obj = _unitOfWork.Product.GetFirstOrDefault(x => x.Id == id);//it will fetch the first record and check with our given record 'id', 
            if (obj == null)
            {

                return Json(new
                {
                    success=false,message="Error while deleting!"
                });
            }

            //Deleting the old image with record
            var oldImagePath = Path.Combine(_hostEnvironment.WebRootPath, obj.ImageUrl.TrimStart('\\'));

            if (System.IO.File.Exists(oldImagePath))
            {
                System.IO.File.Delete(oldImagePath);

            }


            // _db.Remove(obj);
            _unitOfWork.Product.Remove(obj);
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
