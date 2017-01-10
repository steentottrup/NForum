using CreativeMinds.CQS.Queries;
using Microsoft.AspNetCore.Mvc;
using NForum.CQS.Queries;
using System;

namespace NForumTestSite.Controllers {

	public class HomeController : Controller {
		private readonly IQueryDispatcher queryDispatcher;

		public HomeController(IQueryDispatcher queryDispatcher) {
			this.queryDispatcher = queryDispatcher;
		}

		public IActionResult Index() {
			ReadCategoriesWithForumsQuery query = new ReadCategoriesWithForumsQuery { };
			CategoriesAndForums result = this.queryDispatcher.Dispatch<ReadCategoriesWithForumsQuery, CategoriesAndForums>(query);
			return View(result);
		}

		public IActionResult About() {
			ViewData["Message"] = "Your application description page.";

			return View();
		}

		public IActionResult Contact() {
			ViewData["Message"] = "Your contact page.";

			return View();
		}

		public IActionResult Error() {
			return View();
		}
	}
}
