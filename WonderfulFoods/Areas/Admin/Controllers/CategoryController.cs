using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Data.Data.Repository.IRepository;
using Microsoft.AspNetCore.Mvc;
using Models;

namespace WonderfulFoods.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class CategoryController : Controller
    {
        private readonly IUnitOfWork _unitOfWork;
        public CategoryController(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public IActionResult Index()
        {
            return View();
        }
        public IActionResult Upsert(int? Id)
        {
            Category category = new Category();
            if(Id == null)
            {
                return View(category);
            }
            category = _unitOfWork.CategoryRepository.Get(Id.GetValueOrDefault());
            if(category == null)
            {
                return NotFound();
            }
            return View(category);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Upsert(Category category)
        {
            if(ModelState.IsValid)
            {
                if(category.Id == 0)
                {
                    _unitOfWork.CategoryRepository.Add(category);
                }
                else
                {
                    _unitOfWork.CategoryRepository.Update(category);
                }
                _unitOfWork.Save();
                return RedirectToAction(nameof(Index));
            }
            return View(category);
        }

        #region Api Calls
        [HttpGet]
        public IActionResult GetAll()
        {
            return Json(new { data = _unitOfWork.CategoryRepository.GetAll() });
        }
        [HttpDelete]
        public IActionResult Delete(int id)
        {
            var objfromdb = _unitOfWork.CategoryRepository.Get(id);
            if(objfromdb == null)
            {
                return Json(new { success = false, message = "Somthing went completely wrong while deleting" });
            }
            _unitOfWork.CategoryRepository.Remove(objfromdb);
            _unitOfWork.Save();
            return Json(new { success = true, message = "Wonderingful! Its gone." });

        }
        #endregion
    }
}
