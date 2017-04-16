using System;

namespace NForum.Domain {

	public enum Permissions {
		/// <summary>
		/// No permissions to anything.
		/// </summary>
		None = 0,
		/// <summary>
		/// View/Read permissions.
		/// </summary>
		Read = 1,
		/// <summary>
		/// Post new topics.
		/// </summary>
		Post = 2,
		/// <summary>
		/// Permissions to reply to existing topics/posts.
		/// </summary>
		Reply = 3,
		/// <summary>
		/// Permissions to set the priority of a topic (sticky, announcement, etc.)
		/// </summary>
		Priority = 4,
		/// <summary>
		/// Not yet in use.
		/// </summary>
		Poll = 5,
		/// <summary>
		/// Not yet in use.
		/// </summary>
		Vote = 6,
		/// <summary>
		/// Moderator permissions, permissions to editing posts/topics posted by other users.
		/// </summary>
		Moderator = 7,
		/// <summary>
		/// Edit own topics/posts.
		/// </summary>
		Edit = 8,
		/// <summary>
		/// Permissions to deleting own topics/posts.
		/// </summary>
		Delete = 9,
		/// <summary>
		/// Permissions to attaching files to posts.
		/// </summary>
		Upload = 10
	}
}
