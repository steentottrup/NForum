using NForum.Core.Abstractions.Services;
using System;
using System.Web.Mvc;

namespace NForum.Web.Controllers {

	public class ForumController : Controller {
		private readonly IForumService forumService;

		public ForumController(IForumService forumService) {
			this.forumService = forumService;
		}

		public ActionResult Index(String id) {
			return View(this.forumService.FindForumPlus2Levels(id));
		}
	}
}
