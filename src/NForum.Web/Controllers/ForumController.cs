using NForum.Core.Abstractions.Services;
using System;
using System.Collections.Generic;
using System.Web.Mvc;

namespace NForum.Web.Controllers {

	public class ForumController : Controller {
		private readonly IForumService forumService;
		private readonly ITopicService topicService;
		private readonly IUIService uiService;

		public ForumController(IForumService forumService, ITopicService topicService, IUIService uiService) {
			this.forumService = forumService;
			this.topicService = topicService;
			this.uiService = uiService;
		}

		public ActionResult Index(String id) {
			Core.Forum forum = this.uiService.FindForumPlus2Levels(id);
			IEnumerable<Core.Topic> topics = this.uiService.FindByForum(id, 0, 25);
			return View(new ViewModels.Forum {
				Category = forum.Category,
				Id = forum.Id,
				Name = forum.Name,
				Description = forum.Description,
				SubForums = forum.SubForums,
				Topics = topics
			});
		}
	}
}
