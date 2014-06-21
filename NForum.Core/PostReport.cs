using NForum.Core.Abstractions;
using System;
using System.ComponentModel.DataAnnotations;

namespace NForum.Core {

	public class PostReport : IReport {
		public Int32 Id { get; set; }
		public Int32 PostId { get; set; }
		/// <summary>
		/// Timestamp of when the post was reported.
		/// </summary>
		public DateTime Timestamp { get; set; }
		/// <summary>
		/// The reason given for reporting the post.
		/// </summary>
		public String Reason { get; set; }
		/// <summary>
		/// The user that reported the post.
		/// </summary>
		public Int32 ReporterId { get; set; }
		public Boolean Resolved { get; set; }
		public DateTime? ResolvedTimestamp { get; set; }
		public Int32? ResolverId { get; set; }

		public virtual User Reporter { get; set; }
		public virtual User Resolver { get; set; }
		public virtual Post Post { get; set; }
	}
}