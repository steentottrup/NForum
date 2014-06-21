using NForum.Core.Abstractions;
using System;
using System.ComponentModel.DataAnnotations;

namespace NForum.Core {

	public class TopicTracker : ITracker {
		public Int32 Id { get; set; }
		/// <summary>
		/// The timestamp indicating when the user last viewed the topic.
		/// </summary>
		public DateTime LastViewed { get; set; }
		/// <summary>
		/// The Id of the topic tracked.
		/// </summary>
		public Int32 TopicId { get; set; }
		/// <summary>
		/// The Id of the user tracked.
		/// </summary>
		public Int32 UserId { get; set; }
		/// <summary>
		/// The topic tracked.
		/// </summary>
		public virtual Topic Topic { get; set; }
		/// <summary>
		/// The user tracked.
		/// </summary>
		public virtual User User { get; set; }
	}
}