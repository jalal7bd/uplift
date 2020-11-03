using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Uplift.DataAccess.Data;
using Uplift.DataAccess.Data.Repository;
using Uplift.DataAccess.Data.Repository.IRepository;
using Uplift.Models;
using Uplift.Models.ViewModels;

namespace Uplift.Areas.Admin.Controllers
{
    [Authorize]
    [Area("Admin")]
    public class ProductController : Controller
    {
        private readonly ApplicationDbContext _db;
        private readonly IUnitOfWork _unitOfWork;
        private readonly IWebHostEnvironment _hostEnvironment;
        [BindProperty]
        public ProductVM ProdVM { get; set; }
        public ProductController(IUnitOfWork unitOfWork, IWebHostEnvironment hostEnvironment, ApplicationDbContext db)
        {
            _unitOfWork = unitOfWork;
            _hostEnvironment = hostEnvironment;
            _db = db;
        }
        public IActionResult Index()
        {
            return View();
        }
        // for duplicate check
        public JsonResult CheckDuplicateArticleNo(string articleno)
        {
            //need to add a query if it has id and same aricle no
            var searchData = _db.Product.Where(s => s.ArticleNo == articleno).Count();
            if (searchData > 0)
            {
                return Json(1);
            }
            else
            {
                return Json(0);
            }
        }
        public IActionResult Upsert(int? id)
        {
            ProdVM = new ProductVM()
            {
                Product = new Models.Product(),
                CategoryList = _unitOfWork.Category.GetCategoryListForDropDown(),
                FrequencyList = _unitOfWork.Frequency.GetFrequencyListForDropDown(),
            };
            if (id != null)
            {
                ProdVM.Product = _unitOfWork.Product.Get(id.GetValueOrDefault());
            }
            return View(ProdVM);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Upsert()
        {
            if (ModelState.IsValid)
            {
                string webRootPath = _hostEnvironment.WebRootPath;
                var files = HttpContext.Request.Form.Files;
                if (ProdVM.Product.Id == 0)
                {
                    //new product
                    if (files.Count > 0)
                    {
                        string fileName = Guid.NewGuid().ToString();
                        var uploads = Path.Combine(webRootPath, @"images\products");
                        var extention = Path.GetExtension(files[0].FileName);
                        using (var fileStreams = new FileStream(Path.Combine(uploads, fileName + extention), FileMode.Create))
                        {
                            files[0].CopyTo(fileStreams);
                        }
                        ProdVM.Product.ImageUrl = @"\images\products\" + fileName + extention;
                    }

                    _unitOfWork.Product.Add(ProdVM.Product);
                }
                else
                {
                    //edit product
                    var productFromDb = _unitOfWork.Product.Get(ProdVM.Product.Id);
                    if (files.Count > 0)
                    {
                        string fileName = Guid.NewGuid().ToString();
                        var uploads = Path.Combine(webRootPath, @"images\products");
                        var extention_new = Path.GetExtension(files[0].FileName);
                        if (productFromDb.ImageUrl != null)
                        {
                            var imagePath = Path.Combine(webRootPath, productFromDb.ImageUrl.TrimStart('\\'));
                            if (System.IO.File.Exists(imagePath))
                            {
                                System.IO.File.Delete(imagePath);
                            }
                        }
                        using (var fileStreams = new FileStream(Path.Combine(uploads, fileName + extention_new), FileMode.Create))
                        {
                            files[0].CopyTo(fileStreams);
                        }
                        ProdVM.Product.ImageUrl = @"\images\products\" + fileName + extention_new;
                    }
                    else
                    {
                        ProdVM.Product.ImageUrl = productFromDb.ImageUrl;
                    }
                    _unitOfWork.Product.Update(ProdVM.Product);
                }
                _unitOfWork.Save();
                return RedirectToAction(nameof(Index));
            }
            else
            {
                ProdVM = new ProductVM()
                {
                    Product = new Models.Product(),
                    CategoryList = _unitOfWork.Category.GetCategoryListForDropDown(),
                    FrequencyList = _unitOfWork.Frequency.GetFrequencyListForDropDown(),
                };
                return View(ProdVM);
            }
        }
        #region API CALLS
        [HttpGet]
        public IActionResult GetAll()
        {
            return Json(new { data = _unitOfWork.Product.GetAll(includeProperties: "Category,Frequency") });
        }
        [HttpDelete]
        public IActionResult Delete(int id)
        {
            var productFromDb = _unitOfWork.Product.Get(id);
            string webRootPath = _hostEnvironment.WebRootPath;
            var imagePath = Path.Combine(webRootPath, productFromDb.ImageUrl.TrimStart('\\'));
            if (System.IO.File.Exists(imagePath))
            {
                System.IO.File.Delete(imagePath);
            }
            if (productFromDb == null)
            {
                return Json(new { success = false, message = "Error while deleting..." });
            }
            _unitOfWork.Product.Remove(productFromDb);
            _unitOfWork.Save();
            return Json(new { success = true, message = "Deleted successfully..." });
        }
        #endregion
    }
}
