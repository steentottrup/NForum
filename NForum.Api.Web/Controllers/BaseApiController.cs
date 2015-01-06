using NForum.Api.Web.Models;
using NForum.Core.Abstractions.Providers;
using NForum.Core.Abstractions.Services;
using System;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace NForum.Api.Web.Controllers {

	public abstract class BaseApiController : ApiController {
		protected readonly IUserProvider userProvider;
		protected readonly IPermissionService permissionService;

		protected BaseApiController(IUserProvider userProvider, IPermissionService permissionService)
			: base() {

			this.userProvider = userProvider;
			this.permissionService = permissionService;
		}

		protected HttpResponseMessage NotFoundError(String message) {
			return this.Request.CreateResponse<Error>(HttpStatusCode.NotFound, new Error {
				Errors = new String[] { message }
			});
		}
	}
}