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
        public ProductController(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }





        public IActionResult Index()
        {


            IEnumerable<Product> ObjProductList = _unitOfWork.Product.GetAll();


            return View(ObjProductList);

        }

        //Get
        public IActionResult Create()
        {


            return View();
        }


        //Post
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(Product obj)
        {

            if (ModelState.IsValid)//model state will check wheather the required fields are populated or not
            {
                _unitOfWork.Product.Add(obj);
                _unitOfWork.Save();
                TempData["success"] = "Product Created Successfully!";
                return RedirectToAction("Index");
            }
            return View(obj);
        }

        //Upsert method will be the combination of Update + Insert
        //Get
        public IActionResult Upsert(int? id)
        {

            ProductVM productVM = new()
            {
                Product = new(),
                CategoryList = _unitOfWork.Category.GetAll().Select(
               i => SelectListItem{ Text = i.Name,
                    Value = i.Id.ToString()
                } };
           
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



            }


            return View(product);
        }



        //Post
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Upsert(Product obj)
        {

            if (ModelState.IsValid)//model state will check wheather the required fields are populated or not
            {
                _unitOfWork.Product.Update(obj);
                _unitOfWork.Save();
                TempData["success"] = "Product Updated Successfully!";
                return RedirectToAction("Index");
            }
            return View(obj);
        }







        //For Edit Below
        //Get
        public IActionResult Delete(int? id)
        {
            if (id == null || id == 0)
            {
                return NotFound();

            }

            //  var categoryFromDB = _db.Categories.Find(id);//it will try to find it

            var ProductFromDBFirst = _unitOfWork.Product.GetFirstOrDefault(x => x.Id == id);//it will fetch the first record and check with our given record 'id', 
            //  var categoryFromDBSingle = _db.Categories.SingleOrDefault(x => x.Id == id);

            if (ProductFromDBFirst == null)
            {

                return NotFound();
            }

            return View(ProductFromDBFirst);
        }





        ////Post
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public IActionResult DeletePost(int? id)
        {
            //var obj= _db.Categories.Find(id); before repository pattern

            var obj = _unitOfWork.Product.GetFirstOrDefault(x => x.Id == id);//it will fetch the first record and check with our given record 'id', 
            if (obj == null)
            {

                return NotFound();
            }

            // _db.Remove(obj);
            _unitOfWork.Product.Delete(obj);
            _unitOfWork.Save();
            TempData["success"] = "Product deleted Successfully!";
            return RedirectToAction("Index");

        }

    }
}
