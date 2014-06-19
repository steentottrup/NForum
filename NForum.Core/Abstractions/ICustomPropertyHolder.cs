using System;
using System.Xml.Linq;

namespace NForum.Core.Abstractions {

	/// <summary>
	/// Interface for elements with custom properties.
	/// </summary>
	public interface ICustomPropertyHolder {
		String CustomProperties { get; set; }
		XDocument CustomData { get; set; }
	}
}