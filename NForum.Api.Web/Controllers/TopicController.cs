using NForum.Core.Abstractions.Providers;
using NForum.Core.Abstractions.Services;
using System;
using System.Net.Http;
using System.Web.Http;

namespace NForum.Api.Web.Controllers {

	[RoutePrefix("api/nforum/topic")]
	public class TopicController : BaseApiController {

		public TopicController(IUserProvider userProvider, IPermissionService permissionService)
			: base(userProvider, permissionService) {

		}

		[HttpGet]
		[Route("list/{id}")]
		public HttpResponseMessage List(Int32 id) {
			return this.Request.CreateResponse<String>("hejsa");
		}
	}
}