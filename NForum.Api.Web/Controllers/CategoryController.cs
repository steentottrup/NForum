using NForum.Api.Web.Models;
using NForum.Core;
using NForum.Core.Abstractions.Services;
using System;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace NForum.Api.Web.Controllers {

	[RoutePrefix("api/nforum/category")]
	public class CategoryController : BaseApiController {
		protected ICategoryService categoryService;

		// TODO: IoC/DI
		//public CategoryController(ICategoryService categoryService) {
		//	this.categoryService = categoryService;
		//}

		[HttpGet]
		[Route("")]
		public HttpResponseMessage List() {
			//return this.Request.CreateResponse<CategoryRead[]>(
			//	this.categoryService.Read().Select(c => c.ToModel()).ToArray()
			//);
			return this.Request.CreateResponse<String>("hejsa");
		}

		//[HttpDelete]
		//[Route("{id}")]
		//public HttpResponseMessage Delete(Int32 id) {
		//	Category cat = this.categoryService.Read(id);
		//	if (cat == null) {
		//		return this.NotFoundError(String.Format("Could not locate category with id {0}", id));
		//	}
		//	this.categoryService.Delete(cat);
		//	return this.Request.CreateResponse<String>(HttpStatusCode.OK, "ok");
		//}

		[HttpGet]
		[Route("{id}")]
		public HttpResponseMessage Read(Int32 id) {
			return this.Request.CreateResponse<String>("hej");
			return this.Request.CreateResponse<CategoryRead>(new CategoryRead { Id = 1 });

			Category cat = this.categoryService.Read(id);
			if (cat == null) {
				return this.Request.CreateErrorResponse(HttpStatusCode.NotFound, String.Format("Could not locate category with id {0}", id));
			}
			return this.Request.CreateResponse<CategoryRead>(cat.ToModel());
		}

		//[HttpPut]
		//public HttpResponseMessage Create(CategoryCreate model) {
		//	if (model == null) {
		//		return this.Request.CreateResponse<ErrorModel>(HttpStatusCode.BadRequest, new ErrorModel { Errors = new String[] { "Missing model" } });
		//	}
		//	if (String.IsNullOrWhiteSpace(model.Name)) {
		//		return this.Request.CreateResponse<ErrorModel>(HttpStatusCode.BadRequest, new ErrorModel { Errors = new String[] { "Missing name" } });
		//	}
		//	Category newCategory = this.categoryService.Create(model.Name, model.Description, model.SortOrder);
		//	return this.Request.CreateResponse<CategoryRead>(newCategory.ToModel());
		//}

		//[HttpPost]
		//[Route("{id}")]
		//public HttpResponseMessage Update(Int32 id, CategoryUpdate model) {
		//	if (model == null) {
		//		return this.Request.CreateResponse<ErrorModel>(HttpStatusCode.BadRequest, new ErrorModel { Errors = new String[] { "Missing model" } });
		//	}
		//	if (String.IsNullOrWhiteSpace(model.Name)) {
		//		return this.Request.CreateResponse<ErrorModel>(HttpStatusCode.BadRequest, new ErrorModel { Errors = new String[] { "Missing name" } });
		//	}

		//	Category cat = this.categoryService.Read(model.Id);
		//	if (cat == null) {
		//		return this.NotFoundError(String.Format("Could not locate category with id {0}", model.Id));
		//	}

		//	cat.Name = model.Name;
		//	cat.SortOrder = model.SortOrder;
		//	cat.Description = model.Description;
		//	// TODO: More !?
		//	cat = this.categoryService.Update(cat);

		//	return this.Request.CreateResponse<CategoryRead>(cat.ToModel());
		//}
	}
}