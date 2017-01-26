using NForum.Core.Dtos;
using NForum.Domain;
using System;
using System.Collections.Generic;

namespace NForum.Datastores {

	public interface IForumDatastore {
		IForumDto Create(Forum forum);
		IForumDto ReadById(String id);
		IEnumerable<IForumDto> ReadByPath(IEnumerable<String> ids);
		IEnumerable<IForumDto> ReadByCategoryId(String categoryId);
		IForumDto Update(Forum forum);
		void DeleteById(String id);
		void DeleteWithSubElementsById(String id);

		IForumDto MoveToCategory(String forumId, String categoryId);
		IForumDto MoveToForum(String forumId, String parentForumId);
	}
}
