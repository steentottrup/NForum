using System;

namespace NForum.Core.Abstractions {

	public interface ISortableElement {
		Int32 SortOrder { get; set; }
	}
}
