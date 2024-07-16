using Bulky.Models.Models;
using Bulky.Models.Models.ViewModels;
using Bulky.Utility;
using BulkyWeb.Repository;
using BulkyWeb.Repository.IRepository;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.Collections.Generic;

namespace BulkyWeb.Areas.Admin.Controllers
{

    [Area("Admin")]
   // [Authorize(Roles = SD.Role_Admin)]

    public class CompanyController : Controller
    {
        private readonly IUnitOfWork _unitOfWork;
        public CompanyController(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
            
        }
        public IActionResult Index()
        {
            List<Company> objCompanyList = _unitOfWork.Company.GetAll().ToList();
            //IEnumerable<SelectListItem> CategoryList = _unitOfWork.Category.GetAll()
            //    .Select(u => new SelectListItem
            //    {
            //        Text = u.Name,
            //        Value = u.Id.ToString()
            //    });
            // ViewBag.CategoryList = CategoryList;
            //ViewData["CategoryList"] = CategoryList;
            return View(objCompanyList);
        }
        // public IActionResult Create()
        public IActionResult Upsert(int? id)

        {
            
            if (id == null || id == 0)
            {
                //Create
                return View(new Company());

            }
            else
            {
                //Update
                Company companyObj = _unitOfWork.Company.Get(u => u.Id == id);
                return View(companyObj);
            }


        }
        [HttpPost]
        // public IActionResult Create(ProductVM productVM)
        public IActionResult Upsert(Company CompanyObj)

        {
            //if (obj.Title.ToLower() == obj.ISBN.ToString())
            //{
            //    ModelState.AddModelError("Name", "Display Order cannot exactly match the name.");
            //}
            // if (obj.Name.ToLower() == "test")
            // {
            //  ModelState.AddModelError("", "test is an invalid value");
            // }

            if (ModelState.IsValid)
            {
                
               

                if (CompanyObj.Id == 0)
                {
                    _unitOfWork.Company.Add(CompanyObj);

                }
                else
                {
                    _unitOfWork.Company.Update(CompanyObj);
                }
                _unitOfWork.Save();
                TempData["Success"] = "Company Created Successfully";
                return RedirectToAction("Index");
            }
            else
            {
               

                return View(CompanyObj);
            }


        }


        //public IActionResult Edit(int? id)
        //{
        //    if (id == null || id == 0)
        //    {
        //        return NotFound();
        //    }

        //    Product? ProductFromDb = _unitOfWork.Product.Get(u => u.Id == id);
        //    // Category? categoryFromDb1 = _db.Categories.FirstOrDefault(u => u.Id == id);
        //    // Category? categoryFromDb2 = _db.Categories.Where(u =>u.Id==id).FirstOrDefault();


        //    if (ProductFromDb == null)
        //    {
        //        return NotFound();
        //    }
        //    return View(ProductFromDb);

        //}
        [HttpPost]
        public IActionResult Edit(Company obj)
        {
            // if (obj.Name.ToLower() == "test")
            // {
            //  ModelState.AddModelError("", "test is an invalid value");
            // }

            if (ModelState.IsValid)
            {
                _unitOfWork.Company.Update(obj);
                _unitOfWork.Save();
                TempData["Success"] = "Company Updated Successfully";
                return RedirectToAction("Index", "Product");
            }
            return View();

        }

        //    public IActionResult Delete(int? id)
        //{
        //    if (id == null || id == 0)
        //    {
        //        return NotFound();
        //    }

        //    Product? ProductFromDb = _unitOfWork.Product.Get(u => u.Id == id);
        //    // Category? categoryFromDb1 = _db.Categories.FirstOrDefault(u => u.Id == id);
        //    // Category? categoryFromDb2 = _db.Categories.Where(u =>u.Id==id).FirstOrDefault();


        //    if (ProductFromDb == null)
        //    {
        //        return NotFound();
        //    }
        //    return View(ProductFromDb);

        //}
        //[HttpPost, ActionName("Delete")]
        //public IActionResult DeletePOST(int id)
        //{
        //    // if (obj.Name.ToLower() == "test")
        //    // {
        //    //  ModelState.AddModelError("", "test is an invalid value");
        //    // }
        //    Product? obj = _unitOfWork.Product.Get(u => u.Id == id);
        //    if (obj == null)
        //    {
        //        return NotFound();
        //    }
        //    _unitOfWork.Product.Remove(obj);
        //    _unitOfWork.Save();
        //    TempData["Success"] = "Product Deleted Successfully";
        //    return RedirectToAction("Index", "Product");




        //}
        #region API CALLS

        [HttpGet]
        public IActionResult GetAll()
        {
            List<Company> objCompanyList = _unitOfWork.Company.GetAll().ToList();
            return Json(new { data = objCompanyList });
        }


        [HttpDelete]
        public IActionResult Delete(int? id)
        {
            var CompanyToBeDeleted = _unitOfWork.Company.Get(u => u.Id == id);
            if (CompanyToBeDeleted == null)
            {
                return Json(new { success = false, message = "Error while deleting" });
            }

           
           
            _unitOfWork.Company.Remove(CompanyToBeDeleted);
            _unitOfWork.Save();
            return Json(new { success = true, message = "Deleted Successful" });
        }
        #endregion


    }
}
