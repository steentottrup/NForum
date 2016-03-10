using NForum.Core;
using NForum.Core.Abstractions.Services;
using System;
using System.Linq;
using System.Web.Mvc;

namespace NForum.Web.Controllers {

	public class HomeController : Controller {
		private readonly ICategoryService categoryService;
		private readonly IUIService uiService;
		private readonly IForumUserService forumUserService;
		private readonly IForumService forumService;
		private readonly ITopicService topicService;

		public HomeController(ICategoryService categoryService, IUIService uiService, IForumUserService forumUserService, IForumService forumService, ITopicService topicService) {
			this.categoryService = categoryService;
			this.uiService = uiService;
			this.forumUserService = forumUserService;
			this.forumService = forumService;
			this.topicService = topicService;
		}

		public ActionResult Index() {
			if (!this.categoryService.FindAll().Any()) {
				ForumUser user = this.forumUserService.Create("testuser", "test@email.com", "Mister Test User", "todo", "da-DK", "uhm");

				Category firstCategory = this.categoryService.Create("First Category", 1, "");
				Category secondCategory = this.categoryService.Create("Second Category", 2, "");

				Forum forum1 = this.forumService.Create(firstCategory.Id, "first forum", 1, "meh");
				Forum forum2 = this.forumService.CreateSubForum(forum1.Id, "first, first forum", 1, "boh");
				Forum forum3 = this.forumService.Create(firstCategory.Id, "first forum", 2, "meh");

				Topic topic1 = this.topicService.Create(forum2.Id, "first topic", "bla bla bla");
				Topic topic2 = this.topicService.Create(forum2.Id, "second topic", "bla bla bla");


			}

			return View(this.uiService.FindCategoriesPlus2Levels());
		}
	}
}
