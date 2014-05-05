using System;
using System.Xml.Linq;

namespace NForum.Core.Abstractions {

	public interface ICustomPropertyHolder {
		String CustomProperties { get; set; }
		XDocument CustomData { get; set; }
	}
}