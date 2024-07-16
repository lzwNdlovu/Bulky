using Bulky.Models.Models;
using Bulky.Utility;
using BulkyWeb.Repository.IRepository;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Authorization.Infrastructure;
using Microsoft.AspNetCore.Mvc;

namespace BulkyWeb.Areas.Admin.Controllers
{
    [Area("Admin")]
    //[Authorize(Roles = SD.Role_Admin)]
    public class CategoryController : Controller
    {
        private readonly IUnitOfWork _unitOfWork;
        public CategoryController(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }
        public IActionResult Index()
        {
            List<Category> objCategoryList = _unitOfWork.Category.GetAll().ToList();
            return View(objCategoryList);
        }
        public IActionResult Create()
        {
            return View();
        }
        [HttpPost]
        public IActionResult Create(Category obj)
        {
            if (obj.Name.ToLower() == obj.DisplayOrder.ToString())
            {
                ModelState.AddModelError("Name", "Display Order cannot exactly match the name.");
            }
            // if (obj.Name.ToLower() == "test")
            // {
            //  ModelState.AddModelError("", "test is an invalid value");
            // }

            if (ModelState.IsValid)
            {
                _unitOfWork.Category.Add(obj);
                _unitOfWork.Save();
                TempData["Success"] = "Catergory Created Successfully";
                return RedirectToAction("Index", "Category");
            }
            return View();
        }
        public IActionResult Edit(int? id)
        {
            if (id == null || id == 0)
            {
                return NotFound();
            }

            Category? categoryFromDb = _unitOfWork.Category.Get(u => u.Id == id);
            // Category? categoryFromDb1 = _db.Categories.FirstOrDefault(u => u.Id == id);
            // Category? categoryFromDb2 = _db.Categories.Where(u =>u.Id==id).FirstOrDefault();


            if (categoryFromDb == null)
            {
                return NotFound();
            }
            return View(categoryFromDb);

        }
        [HttpPost]
        public IActionResult Edit(Category obj)
        {
            // if (obj.Name.ToLower() == "test")
            // {
            //  ModelState.AddModelError("", "test is an invalid value");
            // }

            if (ModelState.IsValid)
            {
                _unitOfWork.Category.Update(obj);
                _unitOfWork.Save();
                TempData["Success"] = "Catergory Updated Successfully";
                return RedirectToAction("Index", "Category");
            }
            return View();


        }
        public IActionResult Delete(int? id)
        {
            if (id == null || id == 0)
            {
                return NotFound();
            }

            Category? categoryFromDb = _unitOfWork.Category.Get(u => u.Id == id);
            // Category? categoryFromDb1 = _db.Categories.FirstOrDefault(u => u.Id == id);
            // Category? categoryFromDb2 = _db.Categories.Where(u =>u.Id==id).FirstOrDefault();


            if (categoryFromDb == null)
            {
                return NotFound();
            }
            return View(categoryFromDb);

        }
        [HttpPost, ActionName("Delete")]
        public IActionResult DeletePOST(int id)
        {
            // if (obj.Name.ToLower() == "test")
            // {
            //  ModelState.AddModelError("", "test is an invalid value");
            // }
            Category? obj = _unitOfWork.Category.Get(u => u.Id == id);
            if (obj == null)
            {
                return NotFound();
            }
            _unitOfWork.Category.Remove(obj);
            _unitOfWork.Save();
            TempData["Success"] = "Catergory Deleted Successfully";
            return RedirectToAction("Index", "Category");




        }
    }
}
