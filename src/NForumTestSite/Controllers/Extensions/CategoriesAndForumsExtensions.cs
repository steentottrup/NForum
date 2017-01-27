using NForum.CQS.Queries;
using NForumTestSite.Controllers.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using NForum.Domain;

namespace NForumTestSite.Controllers {

	public static class CategoriesAndForumsExtensions {

		public static ForumStructure ToViewModel(this CategoriesAndForums data) {
			ForumStructure output = new ForumStructure {
				Categories = data.Categories.Select(c => new ForumStructureCategory { Id = c.Id, Name = c.Name, SortOrder = c.SortOrder }).ToList()
			};

			output.Categories.ToList().ForEach(category => {
				List<ForumStructureForum> forums = new List<ForumStructureForum>();
				IEnumerable<NForum.Domain.Forum> fs = data.Forums.Where(f => f.CategoryId == category.Id && String.IsNullOrWhiteSpace(f.ParentForumId) == true).ToList();
				fs.ToList().ForEach(forum => {
					forums.Add(new ForumStructureForum {
						Id = forum.Id,
						Name = forum.Name,
						SortOrder = forum.SortOrder,
						Forums = FetchForums(forum, data.Forums.Where(f => f.CategoryId == category.Id)).ToList()
					});
				});
				category.Forums = forums;
			});

			return output;
		}

		private static IEnumerable<ForumStructureForum> FetchForums(Forum forum, IEnumerable<Forum> fs) {
			List<ForumStructureForum> forums = new List<ViewModels.ForumStructureForum>();
			fs.Where(f => f.ParentForumId == forum.Id).ToList().ForEach(f => {
				forums.Add(new ForumStructureForum {
					Id = forum.Id,
					Name = forum.Name,
					SortOrder = forum.SortOrder,
					Forums = FetchForums(f, fs)
				});
			});

			return forums;
		}
	}
}
