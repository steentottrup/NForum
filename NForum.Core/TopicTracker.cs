using NForum.Core.Abstractions;
using System;
using System.ComponentModel.DataAnnotations;

namespace NForum.Core {

	public class TopicTracker : ITracker {
		public Int32 Id { get; set; }
		private DateTime lastViewed;
		/// <summary>
		/// The timestamp indicating when the user last viewed the topic.
		/// </summary>
		[Required]
		public DateTime LastViewed {
			get {
				return this.lastViewed;
			}
			set {
				this.lastViewed = value.FixTimeZone();
			}
		}
		/// <summary>
		/// The topic tracked.
		/// </summary>
		[Required]
		public Int32 TopicId { get; set; }
		/// <summary>
		/// The user tracked.
		/// </summary>
		[Required]
		public Int32 UserId { get; set; }

		public virtual Topic Topic { get; set; }
		public virtual User User { get; set; }
	}
}