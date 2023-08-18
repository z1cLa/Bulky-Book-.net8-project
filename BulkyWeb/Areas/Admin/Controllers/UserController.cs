using Bulky.DataAccess.Data;
using Bulky.DataAccess.Repository.IRepository;
using Bulky.Models;
using Bulky.Models.ViewModels;
using Bulky.Utility;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;

namespace BulkyWeb.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Authorize(Roles = SD.Role_Admin)]
    public class UserController : Controller
    {
        private readonly ApplicationDbContext _db;
        public UserController(ApplicationDbContext db)
        {
            _db = db;
        }
        public IActionResult Index()
        {
            return View();
        }




        #region API CALLS

        [HttpGet]
        public IActionResult GetAll() 
        {
            List<ApplicationUser> objUserList = _db.ApplicationUsers.Include(u=>u.Company).ToList();



            return Json(new {data = objUserList});
        }

        [HttpDelete]
        public IActionResult Delete (int? id)
        {

            var CompanyToBeDeleted = _unitOfWork.Company.Get(u => u.Id == id);
            if(CompanyToBeDeleted == null)
            {
                return Json(new { succes = false, message = "Error while deleting" });
            }

            _unitOfWork.Company.Remove(CompanyToBeDeleted); 
            _unitOfWork.Save();

            return Json(new { succes = true, message = "Delete successful" });
        }


        #endregion

    }
}
