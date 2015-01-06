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

	[RoutePrefix("api/nforum/category")]
	public class CategoryController : BaseApiController {
		protected ICategoryService categoryService;

		public CategoryController(ICategoryService categoryService,
									IUserProvider userProvider,
									IPermissionService permissionService)
			: base(userProvider, permissionService) {

			this.categoryService = categoryService;
		}

		[HttpGet]
		[Route("list")]
		public HttpResponseMessage List() {
			return this.Request.CreateResponse<CategoryRead[]>(
				// This method only reads the accessible categories, so no need to double check!
				this.categoryService.Read().Select(c => c.ToModel()).ToArray()
			);
		}

		[HttpGet]
		[Route("{id}")]
		public HttpResponseMessage Read(Int32 id) {
			Category cat = null;
			try {
				cat = this.categoryService.Read(id);
			}
			catch (PermissionException) {
				return this.Request.CreateResponse(HttpStatusCode.Forbidden);
			}
			if (cat == null) {
				return this.Request.CreateErrorResponse(HttpStatusCode.NotFound, String.Format("Could not locate category with id {0}", id));
			}
			return this.Request.CreateResponse<CategoryRead>(cat.ToModel());
		}

		[HttpPost]
		[Route("")]
		[Authorize]
		public HttpResponseMessage Create([FromBody]CategoryCreate model) {
			if (!this.permissionService.CanCreateCategory(this.userProvider.CurrentUser)) {
				return this.Request.CreateResponse(HttpStatusCode.Forbidden);
			}
			Category category = this.categoryService.Create(model.Name, model.Description, model.SortOrder);
			return this.Request.CreateResponse<CategoryRead>(category.ToModel());
		}

		[HttpPut]
		[Route("{id}")]
		[Authorize]
		public HttpResponseMessage Update(Int32 id, [FromBody]CategoryUpdate model) {
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
			category.Name = model.Name;
			category.Description = model.Description;
			category.SortOrder = model.SortOrder;
			category = this.categoryService.Update(category);
			return this.Request.CreateResponse<CategoryRead>(category.ToModel());
		}
	}
}