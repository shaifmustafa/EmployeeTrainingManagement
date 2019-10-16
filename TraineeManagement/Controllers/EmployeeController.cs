using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using TraineeManagement.BLL.Managers;
using TraineeManagement.Models.EntityModels.ViewModels;
using TraineeManagement.Repository.Repositories;

namespace TraineeManagement.Controllers
{
    public class EmployeeController : Controller
    {

        EmployeeManager employeeManager = new EmployeeManager();
        GradeManager gradeManager = new GradeManager();
        AdvanceManager advanceManager = new AdvanceManager();

        // GET: Employee
        public ActionResult Index()
        {
            return View();
        }



        [HttpGet]
        public JsonResult EmployeeList()
        {
            return Json(new
            {
                Data = new EmployeeManager().GetAll().Select(x => new {
                    x.Id,
                    x.Name                    
                }).ToList(),
                status = true
            }, JsonRequestBehavior.AllowGet);
        }



        [HttpPost]
        public JsonResult EmployeeCode(Int64 id)
        {
            return Json(new
            {
                Data = new EmployeeManager().GetAll().Where(x => x.Id == id).Select(x => new {
                    x.Id,                    
                    x.Code
                }).ToList(),
                status = true
            }, JsonRequestBehavior.AllowGet);
        }


        [HttpPost]
        public JsonResult EmployeeGradeById(Int64 id)
        {
            var empInfo = employeeManager.GetAll();
            var gradeInfo = gradeManager.GetAll();

            return Json(new
            {
                Data = (from e in empInfo where e.Id.Equals(id)
                        join g in gradeInfo on e.GradeId equals g.Id
                        select new GradeVM
                        {
                            Id = e.Id,
                            GradeId = g.Id,
                            GradeName = g.Grades
                        }),
                status = true
            }, JsonRequestBehavior.AllowGet);
        }


        [HttpGet]
        public JsonResult EmployeeGrades()
        {
            var adv = advanceManager.GetAll();
            var emp = employeeManager.GetAll();

            return Json(new
            {
                Data = new GradeManager().GetAll().Select(x => new{
                    
                    x.Id,
                    x.Grades

                }).ToList(),
                status = true
            }, JsonRequestBehavior.AllowGet);
        }


        [HttpPost]
        public JsonResult GetEmployeeDeatilsByCode(string id)
        {
            var empInfo = employeeManager.GetAll();
            var gradeInfo = gradeManager.GetAll();

            var query = (from e in empInfo
                         where e.Code.Equals(id)
                         join g in gradeInfo on e.GradeId equals g.Id
                         select new GradeVM
                         {
                             Id = e.Id,
                             Name = e.Name,
                             GradeId = g.Id,
                             GradeName = g.Grades
                         }).ToList();

            return Json(new { Data = query, status = true }, JsonRequestBehavior.AllowGet);
        }

    }
}