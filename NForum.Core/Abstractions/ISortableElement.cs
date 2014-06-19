using System;

namespace NForum.Core.Abstractions {

	/// <summary>
	/// An interface for sortable elements.
	/// </summary>
	public interface ISortableElement {
		Int32 SortOrder { get; set; }
	}
}