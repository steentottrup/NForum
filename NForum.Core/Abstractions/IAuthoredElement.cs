using System;

namespace NForum.Core.Abstractions {

	public interface IAuthoredElement {
		Int32 AuthorId { get; set; }
		Int32 EditorId { get; set; }
		DateTime Created { get; set; }
		DateTime Changed { get; set; }
	}
}