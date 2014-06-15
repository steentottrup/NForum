using NForum.Core.Abstractions;
using System;
using System.ComponentModel.DataAnnotations;
using System.Xml.Linq;

namespace NForum.Core {

	public class ForumConfiguration : ICustomPropertyHolder {
		public Int32 Id { get; set; }
		/// <summary>
		/// The name of the configuration settings. The basic forum settings will be store with the name 'NForum'
		/// </summary>
		public String Name { get; set; }
		/// <summary>
		/// Data container for custom properties. Do NOT read or write this property directly, use the extension methods.
		/// </summary>
		public String CustomProperties { get; set; }
		/// <summary>
		/// Xml container for custom properties. Do NOT read or write this property directly, use the extension methods.
		/// </summary>
		public XDocument CustomData { get; set; }

		public static class FieldNames {
			public const String PostsPerPage = "PostsPerPage";
			public const String TopicsPerPage = "TopicsPerPage";
			public const String Theme = "Theme";
			public const String InstallationDate = "InstallDate";
			public const String Version = "Version";
		}
	}
}