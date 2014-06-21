using NForum.Core.Abstractions;
using System;
using System.ComponentModel.DataAnnotations;
using System.Xml.Linq;

namespace NForum.Core {

	public class User : ICustomPropertyHolder {
		public Int32 Id { get; set; }
		public String Name { get; set; }
		public String FullName { get; set; }
		public String EmailAddress { get; set; }
		public String ProviderId { get; set; }
		public String Culture { get; set; }
		public String TimeZone { get; set; }

		public String CustomProperties { get; set; }
		public XDocument CustomData { get; set; }
	}
}