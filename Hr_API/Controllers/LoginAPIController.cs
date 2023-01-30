using Hr_API.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web.Http.Cors;

namespace Hr_API.Controllers
{
    [EnableCors(origins: "http://localhost:3000/", headers: "*", methods: "*")]
    public class LoginAPIController : ApiController
    {
		public IEnumerable<Login> Get()
		{
			using (HrPortalEntities db = new HrPortalEntities())
			{
				return db.Logins.ToList();
			}
		}

		public HttpResponseMessage Get(string id)
		{
			using (HrPortalEntities db = new HrPortalEntities())
			{
				var entity = db.Logins.FirstOrDefault(e => e.UserName == id);

				if (entity != null)
				{
					return Request.CreateResponse(HttpStatusCode.OK, entity);
				}
				else
				{
					return Request.CreateErrorResponse(HttpStatusCode.NotFound, "Username = " + id.ToString() + " not found");
				}
			}
		}

		// POST - API
		public HttpResponseMessage Post([FromBody] Login res)
		{
			try
			{
				using (HrPortalEntities db = new HrPortalEntities())
				{
					db.Logins.Add(res);
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
