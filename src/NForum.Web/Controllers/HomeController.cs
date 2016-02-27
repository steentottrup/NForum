using NForum.Core.Abstractions.Services;
using System;
using System.Web.Mvc;

namespace NForum.Web.Controllers {

	public class HomeController : Controller {
		private readonly ICategoryService categoryService;

		public HomeController(ICategoryService categoryService) {
			this.categoryService = categoryService;
		}

		public ActionResult Index() {
			return View(this.categoryService.FindCategoriesPlus2Levels());
		}
	}
}
