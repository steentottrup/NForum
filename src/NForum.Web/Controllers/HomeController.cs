using NForum.Core.Abstractions.Services;
using System;
using System.Web.Mvc;

namespace NForum.Web.Controllers {

	public class HomeController : Controller {
		private readonly ICategoryService categoryService;
		private readonly IUIService uiService;

		public HomeController(ICategoryService categoryService, IUIService uiService) {
			this.categoryService = categoryService;
			this.uiService = uiService;
		}

		public ActionResult Index() {
			return View(this.uiService.FindCategoriesPlus2Levels());
		}
	}
}
