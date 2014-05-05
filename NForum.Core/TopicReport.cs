using NForum.Core.Abstractions;
using System;
using System.ComponentModel.DataAnnotations;

namespace NForum.Core {

	public class TopicReport : IReport {
		public Int32 Id { get; set; }
		[Required]
		public Int32 TopicId { get; set; }
		private DateTime timestamp;
		/// <summary>
		/// Timestamp of when the topic was reported.
		/// </summary>
		[Required]
		public DateTime Timestamp {
			get {
				return this.timestamp;
			}
			set {
				this.timestamp = value.FixTimeZone();
			}
		}
		/// <summary>
		/// The reason given for reporting the topic.
		/// </summary>
		[Required]
		[StringLength(Int32.MaxValue)]
		public String Reason { get; set; }
		/// <summary>
		/// The user that reported the topic.
		/// </summary>
		[Required]
		public Int32 ReporterId { get; set; }
		[Required]
		public Boolean Resolved { get; set; }
		private DateTime? resolvedTimestamp;
		public DateTime? ResolvedTimestamp {
			get {
				return this.resolvedTimestamp;
			}
			set {
				this.resolvedTimestamp = value.FixTimeZone();
			}
		}
		public Int32? ResolverId { get; set; }

		public virtual User Reporter { get; set; }
		public virtual User Resolver { get; set; }
		public virtual Topic Topic { get; set; }
	}
}