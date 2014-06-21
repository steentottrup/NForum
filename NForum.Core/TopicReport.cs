using NForum.Core.Abstractions;
using System;
using System.ComponentModel.DataAnnotations;

namespace NForum.Core {

	public class TopicReport : IReport {
		public Int32 Id { get; set; }
		public Int32 TopicId { get; set; }
		public DateTime Timestamp { get; set; }
		/// <summary>
		/// The reason given for reporting the topic.
		/// </summary>
		public String Reason { get; set; }
		/// <summary>
		/// The user that reported the topic.
		/// </summary>
		public Int32 ReporterId { get; set; }
		public Boolean Resolved { get; set; }
		public DateTime? ResolvedTimestamp { get; set; }
		public Int32? ResolverId { get; set; }

		public virtual User Reporter { get; set; }
		public virtual User Resolver { get; set; }
		public virtual Topic Topic { get; set; }
	}
}