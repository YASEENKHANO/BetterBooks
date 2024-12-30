using BetterBooksWeb.Data;
using BetterBooksWeb.Models;
using Microsoft.AspNetCore.Mvc;

namespace BetterBooksWeb.Controllers
{
    

    public class CategoryController : Controller
    {
        
        private readonly ApplicationDbContext _db;

        public CategoryController(ApplicationDbContext db)
        {
               _db = db;
        }

        public IActionResult Index()
        {
           // var ObjCategoryList= _db.Categories.ToList();
           //we can use IEnumerable for the same type of data, it is strong typed and we do not need Now to convert it to list so remove it also...
            IEnumerable<Category> ObjCategoryList= _db.Categories;


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
                _db.Categories.Add(obj);
                _db.SaveChanges();
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

            var categoryFromDB = _db.Categories.Find(id);//it will try to find it

           // var categoryFromDBFirst = _db.Categories.FirstOrDefault(x =>x.Id==id);//it will fetch the first record and check with our given record 'id', 
          //  var categoryFromDBSingle = _db.Categories.SingleOrDefault(x => x.Id == id);

            if (categoryFromDB == null) { 
            
             return NotFound();
            }

            return View(categoryFromDB);
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
                _db.Categories.Update(obj);
                _db.SaveChanges();
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

            var categoryFromDB = _db.Categories.Find(id);//it will try to find it

            // var categoryFromDBFirst = _db.Categories.FirstOrDefault(x =>x.Id==id);//it will fetch the first record and check with our given record 'id', 
            //  var categoryFromDBSingle = _db.Categories.SingleOrDefault(x => x.Id == id);

            if (categoryFromDB == null)
            {

                return NotFound();
            }

            return View(categoryFromDB);
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
            var obj= _db.Categories.Find(id);
            if (obj == null) {

                return NotFound();
            }

            _db.Categories.Remove(obj);
            _db.SaveChanges();
            TempData["success"] = "Category deleted Successfully!";
            return RedirectToAction("Index");

        }
    }
}
