using Hr_API.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Hr_API.Controllers
{
    [System.Web.Http.Cors.EnableCors(origins: "http://localhost:3000/", headers: "*", methods: "*")]
    public class LoginController : Controller
    {
        // GET: Login
        public ActionResult Index()
        {
            return View();
        }

		public ActionResult GetData1()
		{
			using (HrPortalEntities db = new HrPortalEntities())
			{
				db.Configuration.LazyLoadingEnabled = true;
				var data = db.Logins.OrderBy(a => a.Id).ToList();
				return Json(new { data = data }, JsonRequestBehavior.AllowGet);
			}
		}
	}
}