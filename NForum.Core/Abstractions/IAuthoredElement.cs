using System;

namespace NForum.Core.Abstractions {

	/// <summary>
	/// Interface for elements with authors/editors and dates for creation/changes.
	/// </summary>
	public interface IAuthoredElement {
		Int32 AuthorId { get; set; }
		Int32 EditorId { get; set; }
		DateTime Created { get; set; }
		DateTime Changed { get; set; }
	}
}