using System;
using System.Collections.Generic;

namespace NForum.Core.Abstractions.Services {

	public interface IForumService {
		Forum Create(Category category, String name, Int32 sortOrder);
		Forum Create(Category category, String name, String description, Int32 sortOrder);
		Forum Create(Forum parentForum, String name, Int32 sortOrder);
		Forum Create(Forum parentForum, String name, String description, Int32 sortOrder);

		Forum Read(Int32 id);
		Forum Read(String name);
		IEnumerable<Forum> Read(Category category);
		IEnumerable<Forum> Read();

		Forum Update(Forum forum);

		void Delete(Forum forum);
	}
}