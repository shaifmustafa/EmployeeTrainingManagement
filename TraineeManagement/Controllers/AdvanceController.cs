using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using TraineeManagement.BLL.Managers;
using TraineeManagement.Models.EntityModels.ViewModels;
using System.Linq.Dynamic;
using TraineeManagement.Models.EntityModels;

namespace TraineeManagement.Controllers
{
    public class AdvanceController : Controller
    {
        AdvanceManager advanceManager = new AdvanceManager();
        EmployeeManager employeeManager = new EmployeeManager();
        DistrictManager districtManager = new DistrictManager();
        SubDistrictManager subDistrictManager = new SubDistrictManager();
        AdvanceDetailsManager advanceDetailsManager = new AdvanceDetailsManager();
        PurposeManager purposeManager = new PurposeManager();
        GradeManager gradeManager = new GradeManager();


        // GET: Advance
        public ActionResult Index()
        {
            return View();
        }



        [HttpPost]
        public JsonResult GetList()
        {
            var draw = Request.Form.GetValues("draw").FirstOrDefault();
            //Find paging info
            var start = Request.Form.GetValues("start").FirstOrDefault();
            var length = Request.Form.GetValues("length").FirstOrDefault();
            //Find order columns info
            var sortColumnIndex = Request.Form.GetValues("order[0][column]").FirstOrDefault();
            var sortColumnName = Request.Form.GetValues("columns[" + sortColumnIndex + "][data]").FirstOrDefault();
            var sortColumnDir = Request.Form.GetValues("order[0][dir]").FirstOrDefault();
            //find search columns info
            var search = Request.Form.GetValues("search[value]").FirstOrDefault().ToLower();
            var sMemoNo = Request.Form.GetValues("columns[0][search][value]").FirstOrDefault().ToLower();
            var sEmployeeId = Request.Form.GetValues("columns[1][search][value]").FirstOrDefault().ToLower();            
            var sFromDate = Request.Form.GetValues("columns[2][search][value]").FirstOrDefault();
            var sToDate = Request.Form.GetValues("columns[3][search][value]").FirstOrDefault().ToLower();

            var pageSize = length != null ? Convert.ToInt32(length) : 0;
            var skip = start != null ? Convert.ToInt16(start) : 0;
            var advanceInfo = advanceManager.GetAll();
            var employeeInfo = employeeManager.GetAll();
            var query = (from a in advanceInfo
                         join e in employeeInfo on a.EmployeeId equals e.Id
                         select new AdvanceVM
                         {
                             Id = a.Id,
                             AdvanceType = a.AdvanceType,
                             MemoNo = a.MemoNo,
                             AdvanceStatus = a.AdvanceStatus,
                             EmployeeId = a.EmployeeId,                            
                             EmployeeName = e.Name,
                             FromDate = a.FromDate,
                             ToDate = a.ToDate,
                             GrandTotal = a.GrandTotal
                         });
            var total = query.Count();

            //SEARCHING...
            query = query.Where(q => q.MemoNo == sMemoNo || string.IsNullOrEmpty(sMemoNo));
            query = query.Where(q => q.EmployeeId.ToString() == sEmployeeId || string.IsNullOrEmpty(sEmployeeId));

            if ((!string.IsNullOrEmpty(sFromDate)) && (!string.IsNullOrEmpty(sToDate)))
            {
                var sdFromDate = DateTime.Parse(sFromDate);
                var sdToDate = DateTime.Parse(sToDate);

                query = query.Where(q => q.FromDate.Date >= sdFromDate.Date && q.ToDate.Date <= sdToDate.Date).ToList();
            }            


            //SORTING...  (For sorting we need to add a reference System.Linq.Dynamic)
            if (!(string.IsNullOrEmpty(sortColumnName) && string.IsNullOrEmpty(sortColumnDir)))
            {
                query = query.OrderBy(sortColumnName + " " + sortColumnDir);
            }
            var filtered = query.Count();
            if (pageSize != -1)
            {
                query = query.Skip(skip).Take(pageSize);
            }

            return Json(new { draw, recordsFiltered = filtered, recordsTotal = total, data = query.ToList() },
                     JsonRequestBehavior.AllowGet);
        }


        [HttpPost]

        public JsonResult Save(Advance model)
        {
            if (ModelState.IsValid && model.AdvanceDetails != null && model.AdvanceDetails.Count > 0)
            {
                if (advanceManager.SaveOrUpdate(model))                
                    return Json(new { info = "Saved", status = true }, JsonRequestBehavior.AllowGet);
                return Json(new { info = "Not Saved", status = false }, JsonRequestBehavior.AllowGet);
            }

            else
            {
                return Json(new { info = "Not Saved", status = false }, JsonRequestBehavior.AllowGet);
            }

        }


        [HttpPost]

        public JsonResult GetDetails(Int64 id)
        {
            var res = advanceManager.GetAll();

            var exist = advanceManager.GetById(id);

            var emp = employeeManager.GetAll();
            var dis = districtManager.GetAll();
            var sub = subDistrictManager.GetAll();
            var advd = advanceDetailsManager.GetAll();
            var pur = purposeManager.GetAll();
            var grd = gradeManager.GetAll();

            if (exist.AdvanceType == "Travelling")
            {                
                var query = (from a in res
                             where a.Id.Equals(id)
                             join e in emp on a.EmployeeId equals e.Id
                             join g in grd on e.GradeId equals g.Id
                             join d in dis on a.DistrictId equals d.Id
                             join s in sub on a.SubDistrictId equals s.Id
                             select new AdvanceVM
                             {
                                 Id = a.Id,
                                 MemoNo = a.MemoNo,
                                 AdvanceType = a.AdvanceType,
                                 AdvanceStatus = a.AdvanceStatus,
                                 EmployeeId = a.EmployeeId,
                                 EmployeeName = e.Name,                                 
                                 EmployeeCode = e.Code,
                                 GradeId = g.Id,
                                 EmployeeGrade = g.Grades,
                                 FromDate = a.FromDate,
                                 ToDate = a.ToDate,
                                 DistrictId = a.DistrictId,
                                 SubDistrictId = a.SubDistrictId,
                                 Location = a.Location,
                                 Description = a.Description

                             }).ToList();

                return Json(new { Data = query, status = res == null ? false : true }, JsonRequestBehavior.AllowGet);
            }

            else
            {
                var query = (from a in res
                             where a.Id.Equals(id)
                             join e in emp on a.EmployeeId equals e.Id
                             join g in grd on e.GradeId equals g.Id
                             select new AdvanceVM
                             {
                                 Id = a.Id,
                                 MemoNo = a.MemoNo,
                                 AdvanceType = a.AdvanceType,
                                 AdvanceStatus = a.AdvanceStatus,
                                 EmployeeId = a.EmployeeId,
                                 EmployeeName = e.Name,                             
                                 EmployeeCode = e.Code,
                                 EmployeeGrade = g.Grades,
                                 FromDate = a.FromDate,
                                 ToDate = a.ToDate,                                 
                                 Location = a.Location,
                                 Description = a.Description,
                                 Trainer = a.Trainer,
                                 Topic = a.Topic

                             }).ToList();

                return Json(new { Data = query, status = res == null ? false : true }, JsonRequestBehavior.AllowGet);
            }


        }


        [HttpPost]

        public JsonResult GetTableDetails(Int64 id)
        {
            var av = advanceManager.GetAll();            
            var advd = advanceDetailsManager.GetAll();
            var pur = purposeManager.GetAll();

            var query = (from a in av
                         where a.Id.Equals(id)
                         join ad in advd on a.Id equals ad.AdvanceId
                         join p in pur on ad.PurposeId equals p.Id
                         select new AdvanceVM
                         {
                             Id = a.Id,
                             AdvanceDetailsId = ad.Id,
                             PurposeId = p.Id,
                             PurposeName = p.Name,
                             Qty = ad.Quantity,
                             Amount = ad.Amount,
                             Total = ad.Total

                         }).ToList();

            return Json(new { Data = query, status = advd == null ? false : true }, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public JsonResult Confirm(Int64 id)
        {
            if (advanceManager.Confrim(id))
            {
                return Json(new { info = "Done!", status = true }, JsonRequestBehavior.AllowGet);
            }
            else
            {
                return Json(new { info = "Failed!!", status = false }, JsonRequestBehavior.AllowGet);
            }
        }


        [HttpPost]
        public JsonResult Delete(Int64 id)
        {
            if (advanceManager.Delete(id))
            {
                return Json(new { info = "Done!", status = true }, JsonRequestBehavior.AllowGet);
            }
            else
            {
                return Json(new { info = "Done!!", status = false }, JsonRequestBehavior.AllowGet);
            }
        }

    }
}