using BetterBooks.DataAccess.Repository.IRepository;
using BetterBooks.Models;
using BetterBooks.Utitlity;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace BetterBooksWeb.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Authorize(Roles = SD.Role_Admin)]
    public class CoverTypesController : Controller
    {
        private readonly IUnitOfWork _unitOfWork;
        public CoverTypesController(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }





        public IActionResult Index()
        {


            IEnumerable<CoverType> ObjCoverList = _unitOfWork.CoverType.GetAll();


            return View(ObjCoverList);

        }

        //Get
        public IActionResult Create()
        {


            return View();
        }


        //Post
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(CoverType obj)
        {

            if (ModelState.IsValid)//model state will check wheather the required fields are populated or not
            {
                _unitOfWork.CoverType.Add(obj);
                _unitOfWork.Save();
                TempData["success"] = "CoverType Created Successfully!";
                return RedirectToAction("Index");
            }
            return View(obj);
        }

        //For Edit Below
        //Get
        public IActionResult Edit(int? id)
        {
            if (id == null || id == 0)
            {
                return NotFound();

            }

            //  var categoryFromDB = _db.Categories.Find(id);//it will try to find it

            var CoverTypeFromDBFirst = _unitOfWork.CoverType.GetFirstOrDefault(x => x.Id == id);//it will fetch the first record and check with our given record 'id', 
                                                                                                //  var categoryFromDBSingle = _db.Categories.SingleOrDefault(x => x.Id == id);

            if (CoverTypeFromDBFirst == null)
            {

                return NotFound();
            }

            return View(CoverTypeFromDBFirst);
        }



        //Post
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(CoverType obj)
        {

            if (ModelState.IsValid)//model state will check wheather the required fields are populated or not
            {
                _unitOfWork.CoverType.Update(obj);
                _unitOfWork.Save();
                TempData["success"] = "CoverType Updated Successfully!";
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

            var coverTypeFromDBFirst = _unitOfWork.CoverType.GetFirstOrDefault(x => x.Id == id);//it will fetch the first record and check with our given record 'id', 
            //  var categoryFromDBSingle = _db.Categories.SingleOrDefault(x => x.Id == id);

            if (coverTypeFromDBFirst == null)
            {

                return NotFound();
            }

            return View(coverTypeFromDBFirst);
        }





        ////Post
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public IActionResult DeletePost(int? id)
        {
            //var obj= _db.Categories.Find(id); before repository pattern

            var obj = _unitOfWork.CoverType.GetFirstOrDefault(x => x.Id == id);//it will fetch the first record and check with our given record 'id', 
            if (obj == null)
            {

                return NotFound();
            }

            // _db.Remove(obj);
            _unitOfWork.CoverType.Delete(obj);
            _unitOfWork.Save();
            TempData["success"] = "CoverType deleted Successfully!";
            return RedirectToAction("Index");

        }

    }
}
