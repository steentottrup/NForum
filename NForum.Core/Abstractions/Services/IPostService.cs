using System;
using System.Collections.Generic;

namespace NForum.Core.Abstractions.Services {

	/// <summary>
	/// The PostService interface.
	/// </summary>
	public interface IPostService {
		/// <summary>
		/// Method for creating a new post as a reply to a topic.
		/// </summary>
		/// <param name="topic">The topic to create the new post in.</param>
		/// <param name="subject">The subject of the new post.</param>
		/// <param name="message">The message of the new post.</param>
		/// <param name="customProperties"></param>
		/// <returns>The new post.</returns>
		Post Create(Topic topic, String subject, String message, IDictionary<String, Object> customProperties = null);
		/// <summary>
		/// Method for creating a new post as a reply to an existing post.
		/// </summary>
		/// <param name="post">The post to create the new post in reply to.</param>
		/// <param name="subject">The subject of the new post.</param>
		/// <param name="message">The message of the new post.</param>
		/// <param name="customProperties"></param>
		/// <returns>The new post.</returns>
		Post Create(Post post, String subject, String message, IDictionary<String, Object> customProperties = null);

		/// <summary>
		/// Method for reading a post.
		/// </summary>
		/// <param name="id">The id of the post needed.</param>
		/// <returns>The post with the given id, or null.</returns>
		Post Read(Int32 id);
		/// <summary>
		/// Method for reading a list of posts.
		/// </summary>
		/// <param name="topic">The topic the posts should be fetched from.</param>
		/// <param name="pageIndex">The page requested (0 indexed).</param>
		/// <returns>A list of posts from the given topic.</returns>
		IEnumerable<Post> Read(Topic topic, Int32 pageIndex);
		/// <summary>
		/// Method for reading a list of posts.
		/// </summary>
		/// <param name="topic">The topic the posts should be fetched from.</param>
		/// <param name="pageIndex">The page requested (0 indexed).</param>
		/// <param name="includeQuarantined">Include quarantined posts.</param>
		/// <param name="includeDeleted">Include deleted posts.</param>
		/// <returns>A list of posts from the given topic.</returns>
		IEnumerable<Post> Read(Topic topic, Int32 pageIndex, Boolean includeQuarantined, Boolean includeDeleted);

		/// <summary>
		/// Method for updating a post.
		/// </summary>
		/// <param name="post">The properties from this post will be update in the data storage.</param>
		/// <returns>The updated post.</returns>
		Post Update(Post post);

		/// <summary>
		/// Method for updating state on a post.
		/// </summary>
		/// <param name="post">The post that gets the state property updated.</param>
		/// <param name="newState">The new state.</param>
		/// <returns>The post with state updated.</returns>
		Post Update(Post post, PostState newState);

		/// <summary>
		/// Method for deleting a post.
		/// </summary>
		/// <param name="post">The post to delete.</param>
		void Delete(Post post);
	}
}