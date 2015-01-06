using NForum.Api.Web.Models;
using NForum.Core;
using NForum.Core.Abstractions.Providers;
using NForum.Core.Abstractions.Services;
using NForum.Core.Services;
using System;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace NForum.Api.Web.Controllers {

	[RoutePrefix("api/nforum/forum")]
	public class ForumController : BaseApiController {
		protected ICategoryService categoryService;
		protected IForumService forumService;

		public ForumController(ICategoryService categoryService,
									IForumService forumService,
									IUserProvider userProvider,
									IPermissionService permissionService)
			: base(userProvider, permissionService) {

			this.categoryService = categoryService;
			this.forumService = forumService;
		}

		[HttpGet]
		[Route("list/{id}")]
		public HttpResponseMessage List(Int32 id) {
			Category category = null;
			try {
				category = this.categoryService.Read(id);
			}
			catch (PermissionException) {
				return this.Request.CreateResponse(HttpStatusCode.Forbidden);
			}
			if (category == null) {
				return this.Request.CreateErrorResponse(HttpStatusCode.NotFound, String.Format("Could not locate category with id {0}", id));
			}

			return this.Request.CreateResponse<ForumRead[]>(
				// This method only reads the accessible categories, so no need to double check!
				this.forumService.Read(category).Select(f => f.ToModel()).ToArray()
			);
		}

		[HttpGet]
		[Route("{id}")]
		public HttpResponseMessage Read(Int32 id) {
			Forum forum = null;
			try {
				forum = this.forumService.Read(id);
			}
			catch (PermissionException) {
				return this.Request.CreateResponse(HttpStatusCode.Forbidden);
			}
			if (forum == null) {
				return this.Request.CreateErrorResponse(HttpStatusCode.NotFound, String.Format("Could not locate forum with id {0}", id));
			}
			return this.Request.CreateResponse<ForumRead>(forum.ToModel());
		}

		//[HttpPost]
		//[Route("")]
		//[Authorize]
		//public HttpResponseMessage Create([FromBody]ForumCreate model) {
		//	if (!this.permissionService.CanCreateCategory(this.userProvider.CurrentUser)) {
		//		return this.Request.CreateResponse(HttpStatusCode.Forbidden);
		//	}
		//	Forum forum = this.forumService.Create(model.Name, model.Description, model.SortOrder);
		//	return this.Request.CreateResponse<ForumRead>(forum.ToModel());
		//}
	}
}
