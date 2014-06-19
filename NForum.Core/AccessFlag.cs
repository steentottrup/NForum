using System;

namespace NForum.Core {

	[Flags]
	public enum AccessFlag {
		/// <summary>
		/// No access to anything.
		/// </summary>
		None = 0,
		/// <summary>
		/// View/Read access.
		/// </summary>
		Read = 1,
		/// <summary>
		/// Post new topics.
		/// </summary>
		Create = 2,
		/// <summary>
		/// Access to replying to existing topics/posts.
		/// </summary>
		Reply = 4,
		/// <summary>
		/// Access to set the priority of a topic (sticky, announcement, etc.)
		/// </summary>
		Priority = 8,
		/// <summary>
		/// Not yet in use.
		/// </summary>
		Poll = 16,
		/// <summary>
		/// Not yet in use.
		/// </summary>
		Vote = 32,
		/// <summary>
		/// Moderator access, access to editing/deleting posts/topics posted by other users.
		/// </summary>
		Moderator = 64,
		/// <summary>
		/// Edit own topics/posts.
		/// </summary>
		Update = 128,
		/// <summary>
		/// Access to deleting own topics/posts.
		/// </summary>
		Delete = 256,
		/// <summary>
		/// Access to attaching files to posts.
		/// </summary>
		Upload = 512
	}
}