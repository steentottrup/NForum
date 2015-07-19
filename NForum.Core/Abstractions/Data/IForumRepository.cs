using System;
using System.Collections.Generic;

namespace NForum.Core.Abstractions.Data {

	public interface IForumRepository : IRepository<Forum> {
		/// <summary>
		/// Get the forum with the given name.
		/// </summary>
		/// <param name="name"></param>
		/// <returns></returns>
		Forum ByName(String name);
		/// <summary>
		/// Get forums that are children of the given category.
		/// </summary>
		/// <param name="category"></param>
		/// <returns></returns>
		IEnumerable<Forum> ByCategory(Category category);
		/// <summary>
		/// Get the parent forum of the given forum.
		/// </summary>
		/// <param name="forum"></param>
		/// <returns></returns>
		Forum ByForum(Forum forum);
		/// <summary>
		/// Get the parent forum of the given topic.
		/// </summary>
		/// <param name="topic"></param>
		/// <returns></returns>
		Forum ByTopic(Topic topic);
		/// <summary>
		/// Get the parent forum of the given post.
		/// </summary>
		/// <param name="post"></param>
		/// <returns></returns>
		Forum ByPost(Post post);
		/// <summary>
		/// Gt the children of the given forum.
		/// </summary>
		/// <param name="forum"></param>
		/// <returns></returns>
		IEnumerable<Forum> Children(Forum forum);
		/// <summary>
		/// Get all descendants of the given forum.
		/// </summary>
		/// <param name="forum"></param>
		/// <returns></returns>
		IEnumerable<Forum> Descendants(Forum forum);
	}
}