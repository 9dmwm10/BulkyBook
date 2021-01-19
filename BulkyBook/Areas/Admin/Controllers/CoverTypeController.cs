using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BulkyBook.Models;
using BulkyBook.Utility;
using BulkyBooks.DataAccess.Repository;
using BulkyBooks.DataAccess.Repository.IRepository;
using Dapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using BulkyBook.DataAccess.Repository.IRepository;

namespace BulkyBook.Areas.Admin.Controllers
{

    [Area("Admin")]
    [Authorize(Roles = SD.Role_Admin)]
    public class CoverTypeController : Controller
    {

        private readonly IUnitOfWork _unitOfWork;


        public CoverTypeController(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }
        public IActionResult Index()
        {
            return View();
        }

        [HttpGet]
        public IActionResult Upsert(int? id)
        {
            CoverType ctnew = new CoverType();
            if (id == null)
            {
                return View(ctnew);
            }
            var parametr = new DynamicParameters();
            parametr.Add("@Id", id);      
            ctnew = _unitOfWork.Sp_Call.OneRecord<CoverType>(SD.Proc_CoverType_Get, parametr);
            if(ctnew != null)
                return View(ctnew);
            return NotFound();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Upsert(CoverType ct)
        {
            if (ModelState.IsValid)
            {
                var parametr = new DynamicParameters();
                parametr.Add("@Name", ct.Name); ;
                if(ct.Id == 0)
                {
                    _unitOfWork.Sp_Call.Execute(SD.Proc_CoverType_Create, parametr);
                    _unitOfWork.Save();
                    return RedirectToAction(nameof(Index));
                }
                else
                {
                    parametr.Add("@Id", ct.Id);
                    _unitOfWork.Sp_Call.Execute(SD.Proc_CoverType_Update, parametr);
                    _unitOfWork.Save();
                    return RedirectToAction(nameof(Index));
                }
            }
            return NotFound();
        }


        #region Api Call
        [HttpGet]
        public IActionResult GetAll()
        {
            var results = _unitOfWork.Sp_Call.List<CoverType>(SD.Proc_CoverType_GetAll, null);

            return Json(new { data = results });
        }

        [HttpDelete]

        public IActionResult Delete(int id)
        {
            var parametr = new DynamicParameters();
            parametr.Add("@Id", id);
            var objToDelete = _unitOfWork.Sp_Call.OneRecord<CoverType>(SD.Proc_CoverType_Get, parametr);
            if(objToDelete != null)
            {
                _unitOfWork.Sp_Call.Execute(SD.Proc_CoverType_Delete, parametr);
                _unitOfWork.Save();
                return Json(new { success = true, message = "Deleted Succesfully" });
            }
            return Json(new { success = false, message = "Something went wrong :/" });
        }
        #endregion
    }
}
