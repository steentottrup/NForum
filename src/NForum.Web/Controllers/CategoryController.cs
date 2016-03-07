using NForum.Core.Abstractions.Services;
using System;
using System.Web.Mvc;

namespace NForum.Web.Controllers {

	public class CategoryController : Controller {
		private readonly ICategoryService categoryService;
		private readonly IUIService uiService;

		public CategoryController(ICategoryService categoryService, IUIService uiService) {
			this.categoryService = categoryService;
			this.uiService = uiService;
		}

		public ActionResult Index(String id) {
			return View(this.uiService.FindCategoryPlus2Levels(id));
		}
	}
}
