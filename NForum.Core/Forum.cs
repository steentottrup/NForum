using NForum.Core.Abstractions;
using System;
using System.Collections.Generic;
using System.Xml.Linq;
	
namespace NForum.Core {

	/// <summary>
	/// Class representing a forum
	/// </summary>
	public class Forum : INamedElement, ISortableElement, ICustomPropertyHolder, ICloneable {
		/// <summary>
		/// The unique identifier of the topic
		/// </summary>
		public Int32 Id { get; set; }
		/// <summary>
		/// The id of the category
		/// </summary>
		public Int32 CategoryId { get; set; }
		/// <summary>
		/// The id of the parent forum, if this forum has a parent forum
		/// </summary>
		public Int32? ParentForumId { get; set; }
		//public Int32? LatestTopicId { get; set; }
		//public Int32? LatestPostId { get; set; }

		/// <summary>
		/// The name of the forum
		/// </summary>
		public String Name { get; set; }
		/// <summary>
		/// The description of the forum
		/// </summary>
		public String Description { get; set; }
		/// <summary>
		/// The sort order of the forum when listed with other forum at the same level
		/// </summary>
		public Int32 SortOrder { get; set; }

		/// <summary>
		/// Data container for custom properties. Do NOT read or write to this property directly, use the extension methods.
		/// </summary>
		public String CustomProperties { get; set; }
		/// <summary>
		/// Xml container for custom properties. Do NOT read or write to this property directly, use the extension methods.
		/// </summary>
		public XDocument CustomData { get; set; }

		/// <summary>
		/// The category
		/// </summary>
		public virtual Category Category { get; set; }
		/// <summary>
		/// The parent forum, if any
		/// </summary>
		public virtual Forum ParentForum { get; set; }
		//public virtual Topic LatestTopic { get; set; }
		//public virtual Post LatestPost { get; set; }

		/// <summary>
		/// Collection of topic belonging to the forum
		/// </summary>
		public virtual ICollection<Topic> Topics { get; set; }

		public Object Clone() {
			return new Forum {
				Id = this.Id,
				CategoryId = this.CategoryId,
				ParentForumId = this.ParentForumId,
				//LatestPostId = this.LatestPostId,
				//LatestTopicId = this.LatestTopicId,
				Name = this.Name,
				Description = this.Description,
				SortOrder = this.SortOrder,
				CustomProperties = this.CustomProperties
			};
		}
	}
}