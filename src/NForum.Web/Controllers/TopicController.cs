using NForum.Core.Abstractions.Services;
using System;
using System.Web.Mvc;

namespace NForum.Web.Controllers {

	public class TopicController : Controller {
		private readonly ITopicService topicService;

		public TopicController(ITopicService topicService) {
			this.topicService = topicService;
		}

		//public ActionResult Index(String id) {
		//	return View(this.topicService.FindCategoryPlus2Levels(id));
		//}
	}
}
