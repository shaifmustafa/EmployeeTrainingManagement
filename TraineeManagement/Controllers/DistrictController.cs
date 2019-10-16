using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using TraineeManagement.BLL.Managers;

namespace TraineeManagement.Controllers
{
    public class DistrictController : Controller
    {
        // GET: District
        public ActionResult Index()
        {
            return View();
        }


        [HttpGet]
        public JsonResult DistrictList()
        {
            return Json(new
            {
                Data = new DistrictManager().GetAll().Select(x => new {
                    x.Id,
                    x.Name
                }).ToList(),
                status = true
            }, JsonRequestBehavior.AllowGet);
        }


        [HttpPost]
        public JsonResult SubDistrictList(Int64 id)
        {
            return Json(new
            {
                Data = new SubDistrictManager().GetAll().Where(x => x.DistrictId == id).Select(x => new {
                    x.Id,
                    x.Name
                }).ToList(),
                status = true
            }, JsonRequestBehavior.AllowGet);
        }

        [HttpGet]
        public JsonResult SubDistricts()
        {
            return Json(new
            {
                Data = new SubDistrictManager().GetAll().Select(x => new {
                    x.Id,
                    x.Name
                }).ToList(),
                status = true
            }, JsonRequestBehavior.AllowGet);
        }

    }
}