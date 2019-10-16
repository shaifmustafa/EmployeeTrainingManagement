using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using TraineeManagement.BLL.Managers;

namespace TraineeManagement.Controllers
{
    public class PurposeController : Controller
    {
        // GET: Purpose
        public ActionResult Index()
        {
            return View();
        }


        [HttpGet]
        public JsonResult PurposeList()
        {
            return Json(new
            {
                Data = new PurposeManager().GetAll().Select(x => new {
                    x.Id,
                    x.Name
                }).ToList(),
                status = true
            }, JsonRequestBehavior.AllowGet);
        }
    }
}