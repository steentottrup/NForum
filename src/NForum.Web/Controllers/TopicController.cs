using NForum.Core;
using NForum.Core.Abstractions.Services;
using System;
using System.Web.Mvc;

namespace NForum.Web.Controllers {

	public class TopicController : Controller {
		private readonly ITopicService topicService;
		private readonly IForumService forumService;
		private readonly IUIService uiService;

		public TopicController(ITopicService topicService, IUIService uiService, IForumService forumService) {
			this.topicService = topicService;
			this.uiService = uiService;
			this.forumService = forumService;
		}

		public ActionResult Index(String id) {
			Topic topic = this.uiService.FindTopicById(id, 0, 20);
			ViewModels.Topic model = new ViewModels.Topic {
				Id = topic.Id,
				Text = topic.Text,
				Subject = topic.Subject,
				Replies = topic.Replies
			};
			NForum.Core.Forum forum = this.uiService.FindForumPlus2Levels(topic.ForumId);
			model.Forum = new ViewModels.Forum {
				Category = forum.Category,
				Id = forum.Id,
				Name = forum.Name,
				Description = forum.Description,
				SubForums = forum.SubForums
			};
			model.Category = forum.Category;
			return View(model);
		}
	}
}
