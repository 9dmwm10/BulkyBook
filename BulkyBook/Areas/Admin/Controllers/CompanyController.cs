using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BulkyBook.Models;
using BulkyBook.Utility;
using BulkyBooks.DataAccess.Repository;
using BulkyBooks.DataAccess.Repository.IRepository;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using BulkyBook.DataAccess.Repository.IRepository;

namespace BulkyBook.Areas.Admin.Controllers
{
                   
    [Area("Admin")]
    [Authorize(Roles = SD.Role_Admin + "," + SD.Role_Employee)]
    public class CompanyController : Controller
    {
        private readonly IUnitOfWork _unitOfWork;

        public CompanyController(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }
        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Upsert(int? id)
        {
            Company company = new Company();
            if(id != null)
            {
                company = _unitOfWork.Companies.Get(id.GetValueOrDefault());
            }

            return View(company);
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Upsert(Company company)
        {
            if(ModelState.IsValid)
            {
                if (company.id == 0)
                    _unitOfWork.Companies.Add(company);
                else
                    _unitOfWork.Companies.Update(company);

                _unitOfWork.Save();
                return RedirectToAction(nameof(Index));
            }

            return View(company);
        }
        #region API_Calls


        [HttpGet]
        public IActionResult GetAll()
        {
            var results = _unitOfWork.Companies.GetAll();

            return Json(new { data = results });
        }
        [HttpDelete]
        public IActionResult Delete(int id)
        {
            var del_item = _unitOfWork.Companies.Get(id);
            if (del_item != null)
            {
                _unitOfWork.Companies.Remove(id);
                _unitOfWork.Save();
                return Json(new { success = true, message = "Delete succesfully!" });
            }
            else
                return Json(new { success = false, message = "Something went wrong :(" });
        }

        #endregion
    }
}
