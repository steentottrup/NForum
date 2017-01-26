using CreativeMinds.CQS.Commands;
using CreativeMinds.CQS.Queries;
using Microsoft.AspNetCore.Mvc;
using NForum.CQS.Commands.Categories;
using NForum.CQS.Queries;
using NForumTestSite.Controllers.ViewModels;
using System;

namespace NForumTestSite.Controllers {

	public class CategoryController : Controller {
		private readonly ICommandDispatcher commandDispatcher;
		private readonly IQueryDispatcher queryDispatcher;

		public CategoryController(ICommandDispatcher commandDispatcher, IQueryDispatcher queryDispatcher) {
			this.commandDispatcher = commandDispatcher;
			this.queryDispatcher = queryDispatcher;
		}

		[HttpGet]
		public IActionResult Create() {
			CategoriesAndForums all = this.queryDispatcher.Dispatch<ReadCategoriesWithForumsQuery, CategoriesAndForums>(new ReadCategoriesWithForumsQuery { });
	


			return View(new CreateCategoryModel(all.ToViewModel()));
		}

		[HttpPost]
		public IActionResult Create(CreateCategoryModel model) {
			if (ModelState.IsValid) {
				this.commandDispatcher.Dispatch<CreateCategoryCommand>(new CreateCategoryCommand {
					Name = model.Name,
					Description = model.Description,
					SortOrder = model.SortOrder
				});
			}

			CategoriesAndForums all = this.queryDispatcher.Dispatch<ReadCategoriesWithForumsQuery, CategoriesAndForums>(new ReadCategoriesWithForumsQuery { });

			return View(new CreateCategoryModel(all.ToViewModel()));
		}
	}
}
