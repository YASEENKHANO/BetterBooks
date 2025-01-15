using BetterBooks;
using BetterBooks.DataAccess;
using BetterBooks.DataAccess.Repository;
using BetterBooks.DataAccess.Repository.IRepository;
using BetterBooks.Models;
using Microsoft.AspNetCore.Mvc;

namespace BetterBooks.Controllers
{
    

    public class CategoryController : Controller
    {

        // private readonly ApplicationDbContext _db;
        // private readonly ICategoryRepository _db; //added the service in program.cs
       
        private readonly IUnitOfWork _unitOfWork;
        public CategoryController(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public IActionResult Index()
        {
            // var ObjCategoryList= _db.Categories.ToList();
            //we can use IEnumerable for the same type of data, it is strong typed and we do not need Now to convert it to list so remove it also...
            IEnumerable<Category> ObjCategoryList= _unitOfWork.Category.GetAll();


            return View(ObjCategoryList);
        }



       //Get
        public IActionResult Create()
        {


            return View();
        }


        //Post
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(Category obj)
        {
            if (obj.Name==obj.DisplayOrder.ToString()) {
                ModelState.AddModelError("name", "DisplayOrder should not match DisplayName");//the error message works as key value pairs
            }
            if (ModelState.IsValid)//model state will check wheather the required fields are populated or not
            {
                _unitOfWork.Category.Add(obj);
                _unitOfWork.Save();
                TempData["success"] = "Category Created Successfully!";
                return RedirectToAction("Index");
            }
            return View(obj);
        }

        //For Edit Below
        //Get
        public IActionResult Edit(int? id)
        {
            if(id==null || id==0)
            {
                return NotFound();
               
            }

          //  var categoryFromDB = _db.Categories.Find(id);//it will try to find it

            var categoryFromDBFirst = _unitOfWork.Category.GetFirstOrDefault(x =>x.Id==id);//it will fetch the first record and check with our given record 'id', 
          //  var categoryFromDBSingle = _db.Categories.SingleOrDefault(x => x.Id == id);

            if (categoryFromDBFirst == null) { 
            
             return NotFound();
            }

            return View(categoryFromDBFirst);
        }


        //Post
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(Category obj)
        {
            if (obj.Name == obj.DisplayOrder.ToString())
            {
                ModelState.AddModelError("name", "DisplayOrder should not match DisplayName");//the error message works as key value pairs
            }
            if (ModelState.IsValid)//model state will check wheather the required fields are populated or not
            {
                _unitOfWork.Category.Update(obj);
                _unitOfWork.Save();
                TempData["success"] = "Category Updated Successfully!";
                return RedirectToAction("Index");
            }
            return View(obj);
        }


        //for Delete Below

        //For Edit Below
        //Get
        public IActionResult Delete(int? id)
        {
            if (id == null || id == 0)
            {
                return NotFound();

            }

          //  var categoryFromDB = _db.Categories.Find(id);//it will try to find it

             var categoryFromDBFirst = _unitOfWork.Category.GetFirstOrDefault(x =>x.Id==id);//it will fetch the first record and check with our given record 'id', 
            //  var categoryFromDBSingle = _db.Categories.SingleOrDefault(x => x.Id == id);

            if (categoryFromDBFirst == null)
            {

                return NotFound();
            }

            return View(categoryFromDBFirst);
        }


        ////Post
        //[HttpPost]
        //[ValidateAntiForgeryToken]
        //public IActionResult Delete(Category obj)
        //{

        //        _db.Categories.Remove(obj);
        //        _db.SaveChanges();

        //        return RedirectToAction("Index");

        //}

        //OTHER WAY OF DOING IT IS 

        ////Post
        [HttpPost,ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public IActionResult DeletePost(int? id)
        {
            //var obj= _db.Categories.Find(id); before repository pattern

            var obj = _unitOfWork.Category.GetFirstOrDefault(x => x.Id == id);//it will fetch the first record and check with our given record 'id', 
            if (obj == null) {

                return NotFound();
            }

            // _db.Remove(obj);
            _unitOfWork.Category.Delete(obj);
            _unitOfWork.Save();
            TempData["success"] = "Category deleted Successfully!";
            return RedirectToAction("Index");

        }
    }
}
