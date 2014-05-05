using System;
using System.ComponentModel.DataAnnotations;

namespace NForum.Core {

	public class GroupMember {
		public Int32 Id { get; set; }
		public Int32 UserId { get; set; }
		public Int32 GroupId { get; set; }

		public Group Group { get; set; }
		public User User { get; set; }
	}
}