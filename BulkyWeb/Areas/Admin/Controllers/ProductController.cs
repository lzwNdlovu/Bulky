﻿using BulkyWeb.Models;
using BulkyWeb.Models.ViewModels;
using BulkyWeb.Repository.IRepository;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.Collections.Generic;

namespace BulkyWeb.Areas.Admin.Controllers
{

    [Area("Admin")]
    public class ProductController : Controller
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IWebHostEnvironment _webHostEnvironment;
        public ProductController(IUnitOfWork unitOfWork,IWebHostEnvironment webHostEnvironment)
        {
            _unitOfWork = unitOfWork;
            _webHostEnvironment = webHostEnvironment;
        }
        public IActionResult Index()
        {
            List<Product> objProductList = _unitOfWork.Product.GetAll().ToList();
            //IEnumerable<SelectListItem>CategoryList = _unitOfWork.Category.GetAll()
            //    .Select(u => new SelectListItem
            //    {
            //        Text= u.Name,
            //        Value=u.Id.ToString()
            //    });
            return View(objProductList);
        }
       // public IActionResult Create()
        public IActionResult Upsert(int? id)

        {
            ProductVM productVM = new()
            {

                CategoryList = _unitOfWork.Category.GetAll()
                .Select(u => new SelectListItem
                {
                    Text = u.Name,
                    Value = u.Id.ToString()
                }),

                Product = new Product()
            };
            if (id == null || id == 0) 
            {
                //Create
            return View(productVM);

            }
            else
            {
                //Update
                productVM.Product = _unitOfWork.Product.Get(u=>u.Id==id);
                return View(productVM);
            }
               
           
        }
        [HttpPost]
       // public IActionResult Create(ProductVM productVM)
        public IActionResult Upsert(ProductVM productVM,IFormFile? file)

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
                string wwwRootPath = _webHostEnvironment.WebRootPath;
                if (file != null)
                {
                    string fileName = Guid.NewGuid().ToString()+ Path.GetExtension(file.FileName);
                    string productPath = Path.Combine(wwwRootPath, @"images\product");
                    if (!string.IsNullOrEmpty(productVM.Product.ImageUrl))
                    {
                        //Delete old image
                        var oldImagePath = Path.Combine(wwwRootPath, productVM.Product.ImageUrl.TrimStart('\\'));

                        if (System.IO.File.Exists(oldImagePath))
                        {
                            System.IO.File.Delete(oldImagePath);

                        }
                    }

                    using ( var fileStream = new FileStream(Path.Combine(productPath, fileName),FileMode.Create)) 
                    {
                        file.CopyTo(fileStream);
                    }
                    productVM.Product.ImageUrl = @"\image\product" + fileName;
                }

                if (productVM.Product.Id == 0) 
                { 
                _unitOfWork.Product.Add(productVM.Product);

                }
                else 
                {
                    _unitOfWork.Product.Update(productVM.Product);
                }
                _unitOfWork.Save();
                TempData["Success"] = "Product Created Successfully";
                return RedirectToAction("Index", "Product");
            }
            else
            {
                productVM.CategoryList = _unitOfWork.Category.GetAll()
              .Select(u => new SelectListItem
              {
                  Text = u.Name,
                  Value = u.Id.ToString()
              });

                 return View(productVM);
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
        public IActionResult Edit(Product obj)
        {
            // if (obj.Name.ToLower() == "test")
            // {
            //  ModelState.AddModelError("", "test is an invalid value");
            // }

            if (ModelState.IsValid)
            {
                _unitOfWork.Product.Update(obj);
                _unitOfWork.Save();
                TempData["Success"] = "Product Updated Successfully";
                return RedirectToAction("Index", "Product");
            }
            return View();


        }
        public IActionResult Delete(int? id)
        {
            if (id == null || id == 0)
            {
                return NotFound();
            }

            Product? ProductFromDb = _unitOfWork.Product.Get(u => u.Id == id);
            // Category? categoryFromDb1 = _db.Categories.FirstOrDefault(u => u.Id == id);
            // Category? categoryFromDb2 = _db.Categories.Where(u =>u.Id==id).FirstOrDefault();


            if (ProductFromDb == null)
            {
                return NotFound();
            }
            return View(ProductFromDb);

        }
        [HttpPost, ActionName("Delete")]
        public IActionResult DeletePOST(int id)
        {
            // if (obj.Name.ToLower() == "test")
            // {
            //  ModelState.AddModelError("", "test is an invalid value");
            // }
            Product? obj = _unitOfWork.Product.Get(u => u.Id == id);
            if (obj == null)
            {
                return NotFound();
            }
            _unitOfWork.Product.Remove(obj);
            _unitOfWork.Save();
            TempData["Success"] = "Product Deleted Successfully";
            return RedirectToAction("Index", "Product");




        }
    }
}

