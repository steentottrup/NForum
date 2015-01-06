using NForum.Api.Web.Models;
using System;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace NForum.Api.Web.Controllers {

	public abstract class BaseApiController : ApiController {

		protected HttpResponseMessage NotFoundError(String message) {
			return this.Request.CreateResponse<ErrorModel>(HttpStatusCode.NotFound, new ErrorModel {
				Errors = new String[] { message }
			});
		}
	}
}