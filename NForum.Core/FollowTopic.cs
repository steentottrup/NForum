using NForum.Core.Abstractions;
using System;
using System.ComponentModel.DataAnnotations;

namespace NForum.Core {

	public class FollowTopic : IFollower {
		public Int32 Id { get; set; }
		public Int32 TopicId { get; set; }
		public Int32 UserId { get; set; }

		public virtual Topic Topic { get; set; }
		public virtual User User { get; set; }
	}
}