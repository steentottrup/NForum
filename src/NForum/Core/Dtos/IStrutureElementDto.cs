using System;

namespace NForum.Core.Dtos {

	public interface IStrutureElementDto : ICustomPropertiesHolder {
		Int32 SortOrder { get; }
		String Name { get; }
		String Description { get; }
	}
}
