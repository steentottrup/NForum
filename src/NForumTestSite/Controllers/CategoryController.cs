using CreativeMinds.CQS.Commands;
using Microsoft.AspNetCore.Mvc;
using NForum.CQS.Commands.Categories;
using NForumTestSite.Controllers.ViewModels;
using System;

namespace NForumTestSite.Controllers {

	public class CategoryController : Controller {
		private readonly ICommandDispatcher commandDispatcher;

		public CategoryController(ICommandDispatcher commandDispatcher) {
			this.commandDispatcher = commandDispatcher;
		}

		[HttpGet]
		public IActionResult Create() {
			return View(new CreateCategoryModel());
		}

		[HttpPost]
		public IActionResult Create(CreateCategoryModel model) {
			if (ModelState.IsValid) {
				this.commandDispatcher.Dispatch<CreateCategoryCommand>(new CreateCategoryCommand {
					Name  = model.Name,
					Description = model.Description,
					SortOrder = model.SortOrder
				});
			}

			return View();
		}
	}
}
