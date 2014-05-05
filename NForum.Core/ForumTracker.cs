using NForum.Core.Abstractions;
using System;
using System.ComponentModel.DataAnnotations;

namespace NForum.Core {

	public class ForumTracker : ITracker {
		public Int32 Id { get; set; }
		/// <summary>
		/// The timestamp indicating when the user last viewed the forum.
		/// </summary>
		public DateTime LastViewed { get; set; }
		/// <summary>
		/// The forum tracked.
		/// </summary>
		public Int32 ForumId { get; set; }
		/// <summary>
		/// The user tracked.
		/// </summary>
		public Int32 UserId { get; set; }

		public Forum Forum { get; set; }
		public User User { get; set; }
	}
}