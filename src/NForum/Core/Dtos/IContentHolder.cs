using NForum.Core.Refs;
using System;

namespace NForum.Core.Dtos {

	public interface IContentHolder : ICustomPropertiesHolder {
		String Subject { get; }
		String Content { get; }
		DateTime Created { get; }
		DateTime LastEdited { get; }
		IAuthorRef CreatedBy { get; }
		IAuthorRef LastEditedBy { get; }
	}
}