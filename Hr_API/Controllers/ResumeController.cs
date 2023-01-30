using Hr_API.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Hr_API.Controllers
{
    [System.Web.Http.Cors.EnableCors(origins: "http://localhost:3000/", headers: "*", methods: "*")]
    public class ResumeController : Controller
    {
        // GET: Resume
        public ActionResult Index()
        {
            return View();
        }

		public ActionResult GetData()
		{
			using (HrPortalEntities db = new HrPortalEntities())
			{
				db.Configuration.LazyLoadingEnabled = true;
				var data = db.Resumes.OrderBy(a => a.Id).ToList();
				return Json(new { data = data }, JsonRequestBehavior.AllowGet);
			}
		}
	}
}