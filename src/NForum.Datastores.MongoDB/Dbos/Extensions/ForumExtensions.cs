using NForum.Core.Dtos;
using System;
using System.Linq;

namespace NForum.Datastores.MongoDB.Dbos {

	public static class ForumExtensions {

		public static IForumDto ToDto(this Dbos.Forum forum) {
			return new Dtos.Forum {
				Id = forum.Id.ToString(),
				Description = forum.Description,
				Name = forum.Name,
				SortOrder = forum.SortOrder,
				Category = new Dtos.Category { Id = forum.Category.Id.ToString(), Name = forum.Category.Name },
				ParentForum = forum.ParentForum != null ? new Dtos.Forum { Id = forum.ParentForum.Id.ToString(), Name = forum.ParentForum.Name } : null,
				Path = forum.Path.Select(p => p.ToString())
				// TODO:
				//,CustomProperties
			};
		}

		//public static IForumDto ToDto(this Dbos.ForumStructure forum) {
		//	return new Dtos.Forum {
		//		Id = forum.Id.ToString(),
		//		Description = forum.Description,
		//		Name = forum.Name,
		//		SortOrder = forum.SortOrder,
		//		Category = new Dtos.Category { Id = forum.Category.Id.ToString(), Name = forum.Category.Name },
		//		ParentForum = forum.ParentForum != null ? new Dtos.Forum { Id = forum.ParentForum.Id.ToString(), Name = forum.ParentForum.Name } : null
		//		// TODO:
		//		//,CustomProperties
		//	};
		//}
	}
}
