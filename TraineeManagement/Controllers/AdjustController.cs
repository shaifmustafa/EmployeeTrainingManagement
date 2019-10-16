using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using TraineeManagement.BLL.Managers;
using TraineeManagement.Models.EntityModels.ViewModels;
using System.Linq.Dynamic;


namespace TraineeManagement.Controllers
{
    public class AdjustController : Controller
    {
        BillManager billManager = new BillManager();
        BilledDetailsManager billedDetailsManager = new BilledDetailsManager();

        AdvanceManager advanceManager = new AdvanceManager();
        EmployeeManager employeeManager = new EmployeeManager();
        DistrictManager districtManager = new DistrictManager();
        SubDistrictManager subDistrictManager = new SubDistrictManager();
        AdvanceDetailsManager advanceDetailsManager = new AdvanceDetailsManager();
        PurposeManager purposeManager = new PurposeManager();
        GradeManager gradeManager = new GradeManager();

        AdjustManager adjustManager = new AdjustManager();

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

            var sEmployeeId = Request.Form.GetValues("columns[0][search][value]").FirstOrDefault().ToLower();

            var pageSize = length != null ? Convert.ToInt32(length) : 0;
            var skip = start != null ? Convert.ToInt16(start) : 0;


            var billInfo = billManager.GetAll();
            var billDetailsInfo = billedDetailsManager.GetAll();
            var employeeInfo = employeeManager.GetAll();
            var advanceInfo = advanceManager.GetAll();
            var advanceDetailsInfo = advanceDetailsManager.GetAll();

            var adjustInfo = adjustManager.GetAll();

            var query = (from adj in adjustInfo
                         join b in billInfo on adj.BillingId equals b.Id
                         where b.BillStatus.Equals("Confirmed")
                         join a in advanceInfo on b.AdvanceId equals a.Id
                         where a.AdvanceStatus.Equals("Confirmed")
                         join e in employeeInfo on a.EmployeeId equals e.Id
                         select new AdjustVM
                         {                             
                             Id = b.Id,
                             AdvanceId = a.Id,
                             AdvanceMemo = a.MemoNo,
                             BillTotal = adj.BillTotal,
                             AdvanceTotal = a.GrandTotal,
                             EmployeeId = e.Id,
                             EmployeeName = e.Name
                         });
            var total = query.Count();

            //SEARCHING...            
            query = query.Where(q => q.EmployeeId.ToString() == sEmployeeId || string.IsNullOrEmpty(sEmployeeId));


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

        public JsonResult GetDetails(Int64 id)
        {
            var res = advanceManager.GetAll();

            var employeeExist = advanceManager.GetAll().Where(x => x.EmployeeId == id).FirstOrDefault();

            var exist = advanceManager.GetById(employeeExist.Id);

            var emp = employeeManager.GetAll();
            var dis = districtManager.GetAll();
            var sub = subDistrictManager.GetAll();
            var advd = advanceDetailsManager.GetAll();
            var pur = purposeManager.GetAll();
            var grd = gradeManager.GetAll();
            var bill = billManager.GetAll();
            var adjustInfo = adjustManager.GetAll();

            if (exist.AdvanceType == "Travelling")
            {
                var query = (from e in emp
                             where e.Id.Equals(id)
                             join a in res on e.Id equals a.EmployeeId
                             where a.AdvanceStatus.Equals("Confirmed")
                             join b in bill on a.Id equals b.AdvanceId
                             where b.BillStatus.Equals("Confirmed")
                             join adj in adjustInfo on b.Id equals adj.BillingId
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
                                 DistrictName = d.Name,
                                 SubDistrictName = s.Name,
                                 SubDistrictId = a.SubDistrictId,
                                 Location = a.Location,
                                 Description = a.Description, 
                                 GrandTotal = a.GrandTotal,
                                 BillId = b.Id,
                                 BillMemo = b.MemoNo,
                                 BillDate = b.BillDate,
                                 BillDescription = b.Description,
                                 BillVendor = b.Vendor,
                                 BillGrandTotal = adj.BillTotal

                             }).ToList();

                return Json(new { Data = query, status = res == null ? false : true }, JsonRequestBehavior.AllowGet);
            }

            else
            {
                var query = (from e in emp
                             where e.Id.Equals(id)
                             join a in res on e.Id equals a.EmployeeId
                             where a.AdvanceStatus.Equals("Confirmed")
                             join g in grd on e.GradeId equals g.Id
                             join b in bill on a.Id equals b.AdvanceId
                             where b.BillStatus.Equals("Confirmed")
                             join adj in adjustInfo on b.Id equals adj.BillingId
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
                                 Topic = a.Topic,
                                 GrandTotal = a.GrandTotal,
                                 BillId = b.Id,
                                 BillMemo = b.MemoNo,
                                 BillDate = b.BillDate,
                                 BillDescription = b.Description,
                                 BillVendor = b.Vendor,
                                 BillGrandTotal = adj.BillTotal

                             }).ToList();

                return Json(new { Data = query, status = res == null ? false : true }, JsonRequestBehavior.AllowGet);
            }


        }



        [HttpPost]

        public JsonResult GetAdvanceBillTableDetailsWithId(Int64 id)
        {
            var employee = employeeManager.GetAll();
            var bill = billManager.GetAll();
            var billTable = billedDetailsManager.GetAll();
            var av = advanceManager.GetAll();
            var advd = advanceDetailsManager.GetAll();
            var pur = purposeManager.GetAll();

            var query = (from e in employee
                         where e.Id.Equals(id)
                         join a in av on e.Id equals a.EmployeeId
                         where a.AdvanceStatus.Equals("Confirmed")
                         join ad in advd on a.Id equals ad.AdvanceId
                         join b in bill on a.Id equals b.AdvanceId
                         where b.BillStatus.Equals("Confirmed")
                         join bd in billTable on b.Id equals bd.BilledId
                         join p in pur on ad.PurposeId equals p.Id
                         join pr in pur on bd.PurposeId equals pr.Id
                         select new AdvanceVM
                         {
                             Id = a.Id,
                             AdvanceDetailsId = ad.Id,
                             PurposeId = p.Id,
                             PurposeName = p.Name,
                             Qty = ad.Quantity,
                             Amount = ad.Amount,
                             Total = ad.Total,
                             GrandTotal = a.GrandTotal,

                             BillId = b.Id,
                             BillDetailsId = bd.Id,
                             BillPurposeId = p.Id,
                             BillPurposeName = p.Name,
                             BillQty = bd.Quantity,
                             BillAmount = bd.Amount,
                             BillTotal = bd.Total,
                             BillGrandTotal = b.GrandTotal

                         }).ToList();

            return Json(new { Data = query, status = advd == null ? false : true }, JsonRequestBehavior.AllowGet);
        }



        [HttpPost]

        public JsonResult BillDetails(Int64 id)
        {
            var bill = billManager.GetAll();

            var exist = advanceManager.GetById(id);

            var emp = employeeManager.GetAll();
            var dis = districtManager.GetAll();
            var sub = subDistrictManager.GetAll();
            var advd = advanceDetailsManager.GetAll();
            var pur = purposeManager.GetAll();
            var ad = advanceManager.GetAll();

            var query = (from e in emp
                         where e.Id.Equals(id)
                         join a in ad on e.Id equals a.EmployeeId
                         join b in bill on a.Id equals b.AdvanceId
                         select new BillVM
                         {
                             Id = b.Id,
                             EmployeeId = e.Id,
                             EmployeeName = e.Name,
                             AdvanceId = a.Id,
                             MemoNo = a.MemoNo,
                             BillStatus = b.BillStatus,
                             BillDate = b.BillDate,
                             Vendor = b.Vendor,
                             Description = b.Description

                         }).ToList();

            return Json(new { Data = query, status = bill == null ? false : true }, JsonRequestBehavior.AllowGet);


        }


        [HttpPost]

        public JsonResult GetBillTable(Int64 id)
        {
            var adv = advanceManager.GetAll();
            var emp = employeeManager.GetAll();
            var bill = billManager.GetAll();
            var bdm = billedDetailsManager.GetAll();
            var pur = purposeManager.GetAll();

            var query = (from e in emp
                         where e.Id.Equals(id)
                         join a in adv on e.Id equals a.EmployeeId
                         join b in bill on a.Id equals b.AdvanceId
                         join bd in bdm on b.Id equals bd.BilledId
                         join p in pur on bd.PurposeId equals p.Id
                         select new BillVM
                         {
                             Id = a.Id,
                             BillDetailsId = bd.Id,
                             PurposeId = p.Id,
                             PurposeName = p.Name,
                             Qty = bd.Quantity,
                             Amount = bd.Amount,
                             Total = bd.Total,
                             GrandTotal = b.GrandTotal

                         }).ToList();

            return Json(new { Data = query, status = query == null ? false : true }, JsonRequestBehavior.AllowGet);
        }




        [HttpPost]

        public JsonResult GetAdvanceDetails(Int64 id)
        {
            var res = advanceManager.GetAll();

            var memoExist = advanceManager.GetAll().Where(x => x.Id == id).FirstOrDefault();

            var exist = advanceManager.GetById(memoExist.Id);

            var emp = employeeManager.GetAll();
            var dis = districtManager.GetAll();
            var sub = subDistrictManager.GetAll();
            var advd = advanceDetailsManager.GetAll();
            var pur = purposeManager.GetAll();
            var grd = gradeManager.GetAll();

            if (exist.AdvanceType == "Travelling")
            {
                var query = (from e in emp
                             where e.Id.Equals(id)
                             join a in res on e.Id equals a.EmployeeId
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
                                 EmployeeGrade = g.Grades,
                                 FromDate = a.FromDate,
                                 ToDate = a.ToDate,
                                 DistrictId = a.DistrictId,
                                 DistrictName = d.Name,
                                 SubDistrictId = a.SubDistrictId,
                                 SubDistrictName = s.Name,
                                 Location = a.Location,
                                 Description = a.Description

                             }).ToList();

                return Json(new { Data = query, status = res == null ? false : true }, JsonRequestBehavior.AllowGet);
            }

            else
            {
                var query = (from e in emp
                             where e.Id.Equals(id)
                             join a in res on e.Id equals a.EmployeeId
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

        public JsonResult GetAdvanceTable(Int64 id)
        {
            var emp = employeeManager.GetAll();
            var av = advanceManager.GetAll();
            var advd = advanceDetailsManager.GetAll();
            var pur = purposeManager.GetAll();

            var query = (from e in emp
                         where e.Id.Equals(id)
                         join a in av on e.Id equals a.EmployeeId
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
                             Total = ad.Total,
                             GrandTotal = a.GrandTotal

                         }).ToList();

            return Json(new { Data = query, status = advd == null ? false : true }, JsonRequestBehavior.AllowGet);
        }

    }
}