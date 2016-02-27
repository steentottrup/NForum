using NForum.Core.Abstractions.Services;
using System;
using System.Web.Mvc;

namespace NForum.Web.Controllers {

	public class CategoryController : Controller {
		private readonly ICategoryService categoryService;

		public CategoryController(ICategoryService categoryService) {
			this.categoryService = categoryService;
		}

		public ActionResult Index(String id) {
			return View(this.categoryService.FindCategoryPlus2Levels(id));
		}
	}
}
