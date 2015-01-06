using NForum.Core;
using NForum.Core.Abstractions.Services;
using NForum.Demo.WebApi.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace NForum.Demo.WebApi.Controllers {

	[RoutePrefix("api/nforum/category")]
	public class CategoryController : ApiController {
		protected ICategoryService categoryService;

		public CategoryController(ICategoryService categoryService)
			: base() {
			this.categoryService = categoryService;
		}

		[HttpGet]
		[Route("")]
		public HttpResponseMessage List() {
			// TODO: Access!?
			return this.Request.CreateResponse<CategoryRead[]>(
				this.categoryService.Read().Select(c => c.ToModel()).ToArray()
			);
		}

		[HttpGet]
		[Route("{id}")]
		public HttpResponseMessage Read(Int32 id) {
			Category cat = this.categoryService.Read(id);
			if (cat == null) {
				return this.Request.CreateErrorResponse(HttpStatusCode.NotFound, String.Format("Could not locate category with id {0}", id));
			}
			return this.Request.CreateResponse<CategoryRead>(cat.ToModel());
		}

		[HttpPost]
		[Route("")]
		public HttpResponseMessage Create([FromBody]CategoryCreate model) {
			// TODO: Access!!
			Category category = this.categoryService.Create(model.Name, model.Description, model.SortOrder);
			return this.Request.CreateResponse<CategoryRead>(category.ToModel());
		}

		[HttpPut]
		[Route("{id}")]
		public HttpResponseMessage Update(Int32 id, [FromBody]CategoryUpdate model) {
			// TODO: Access!!
			Category category = this.categoryService.Read(id);
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