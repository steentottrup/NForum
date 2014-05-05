using NForum.Core.Abstractions;
using System;
using System.ComponentModel.DataAnnotations;

namespace NForum.Core {

	public class FollowForum : IFollower {
		public Int32 Id { get; set; }
		public Int32 ForumId { get; set; }
		public Int32 UserId { get; set; }

		public Forum Forum { get; set; }
		public User User { get; set; }
	}
}