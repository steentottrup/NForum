using System;

namespace NForum.Core.Abstractions {

	public interface INamedElement {
		String Name { get; set; }
		String Description { get; set; }
	}
}
