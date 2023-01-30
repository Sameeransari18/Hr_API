using Hr_API.Models;
using Microsoft.AspNetCore.Cors;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web.Http.Cors;

namespace Hr_API.Controllers
{
    [System.Web.Http.Cors.EnableCors(origins: "http://localhost:3000/", headers: "*", methods: "*")]
    public class ResumeAPIController : ApiController
    {
		//EnableCorsAttribute cors = new EnableCorsAttribute("*", "*", "*");
		// API/Values

		public IEnumerable<Resume> Get()
		{
			using (HrPortalEntities db = new HrPortalEntities())
			{
				return db.Resumes.ToList();
			}
		}

		public HttpResponseMessage Get(string id)
		{
			using (HrPortalEntities db = new HrPortalEntities())
			{
				var entity = db.Resumes.FirstOrDefault(e => e.FirstName == id);

				if (entity != null)
				{
					return Request.CreateResponse(HttpStatusCode.OK, entity);
				}
				else
				{
					return Request.CreateErrorResponse(HttpStatusCode.NotFound, "Employee with NAME = " + id.ToString() + " not found");
				}
			}
		}

		// POST - API
		public HttpResponseMessage Post([FromBody] Resume res)
		{
			try
			{
				using (HrPortalEntities db = new HrPortalEntities())
				{
					res.Date= DateTime.Now;
					db.Resumes.Add(res);
					db.SaveChanges();
					var message = Request.CreateResponse(HttpStatusCode.Created, res);

					message.Headers.Location = new Uri(Request.RequestUri + res.Id.ToString());
					return message;
				}
			}
			catch (Exception ex)
			{
				return Request.CreateErrorResponse(HttpStatusCode.BadRequest, ex);
			}
		}
	}
}
