using System;
using System.Xml.Linq;

namespace NForum.Core {

	public abstract class BaseTopic {
		/// <summary>
		/// The id of the parent forum
		/// </summary>
		public Int32 ForumId { get; set; }
		/// <summary>
		/// The id of the author of the topic
		/// </summary>
		public Int32 AuthorId { get; set; }
		/// <summary>
		/// The id of the latest editor of the topic
		/// </summary>
		public Int32 EditorId { get; set; }

		//public Int32? LatestPostId { get; set; }

		/// <summary>
		/// Timestamp for the creation of the topic
		/// </summary>
		public DateTime Created { get; set; }
		/// <summary>
		/// Timestamp for the latest change
		/// </summary>
		public DateTime Changed { get; set; }

		/// <summary>
		/// Topic state
		/// </summary>
		public TopicState State { get; set; }
		/// <summary>
		/// Topic type
		/// </summary>
		public TopicType Type { get; set; }

		/// <summary>
		/// Subject of the topic
		/// </summary>
		public String Subject { get; set; }
		/// <summary>
		/// The content of the topic
		/// </summary>
		public String Message { get; set; }

		/// <summary>
		/// Data container for custom properties. Do NOT read or write to this property directly, use the extension methods.
		/// </summary>
		public String CustomProperties { get; set; }
		/// <summary>
		/// Xml container for custom properties. Do NOT read or write to this property directly, use the extension methods.
		/// </summary>
		public XDocument CustomData { get; set; }
	}
}