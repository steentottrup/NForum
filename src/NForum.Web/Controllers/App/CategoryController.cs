using NForum.Core.Abstractions.Services;
using System;
using System.Web.Mvc;

namespace NForum.Web.Controllers.App {

	[RoutePrefix("app/categories")]
	public class CategoryController : BaseAppController {
		private readonly ICategoryService catService;

		public CategoryController(ICategoryService catService) {
			this.catService = catService;
		}

		[HttpGet]
		[Route("")]
		public JsonResult Get() {
			return this.Json(this.catService.FindAll());
		}
	}
}
