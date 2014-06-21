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
		/// The Id of the forum tracked.
		/// </summary>
		public Int32 ForumId { get; set; }
		/// <summary>
		/// The Id of the user tracked.
		/// </summary>
		public Int32 UserId { get; set; }
		/// <summary>
		/// The forum tracked.
		/// </summary>
		public virtual Forum Forum { get; set; }
		/// <summary>
		/// The user tracked.
		/// </summary>
		public virtual User User { get; set; }
	}
}