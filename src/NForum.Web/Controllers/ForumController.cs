using NForum.Core.Abstractions.Services;
using System;
using System.Collections.Generic;
using System.Web.Mvc;

namespace NForum.Web.Controllers {

	public class ForumController : Controller {
		private readonly IForumService forumService;
		private readonly ITopicService topicService;

		public ForumController(IForumService forumService, ITopicService topicService) {
			this.forumService = forumService;
			this.topicService = topicService;
		}

		public ActionResult Index(String id) {
			Core.Forum forum = this.forumService.FindForumPlus2Levels(id);
			IEnumerable<Core.Topic> topics = this.topicService.FindByForum(id, 0, 25);
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
