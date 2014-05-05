using NForum.Core.Abstractions;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Xml.Linq;

namespace NForum.Core {

	public class Forum : INamedElement, ISortableElement, ICustomPropertyHolder, ICloneable {
		public Int32 Id { get; set; }
		public Int32 CategoryId { get; set; }
		public Int32? ParentForumId { get; set; }
		public Int32? LatestTopicId { get; set; }
		public Int32? LatestPostId { get; set; }

		public String Name { get; set; }
		public String Description { get; set; }
		public Int32 SortOrder { get; set; }

		/// <summary>
		/// Data container for custom properties. Do NOT read or write this property directly, use the extension methods.
		/// </summary>
		public String CustomProperties { get; set; }
		/// <summary>
		/// Xml container for custom properties. Do NOT read or write this property directly, use the extension methods.
		/// </summary>
		public XDocument CustomData { get; set; }

		public virtual Category Category { get; set; }
		public virtual Forum ParentForum { get; set; }
		public virtual Topic LatestTopic { get; set; }
		public virtual Post LatestPost { get; set; }

		public virtual ICollection<Topic> Topics { get; set; }

		public Object Clone() {
			return new Forum {
				Id = this.Id,
				CategoryId = this.CategoryId,
				ParentForumId = this.ParentForumId,
				LatestPostId = this.LatestPostId,
				LatestTopicId = this.LatestTopicId,
				Name = this.Name,
				Description = this.Description,
				SortOrder = this.SortOrder,
				CustomProperties = this.CustomProperties
			};
		}
	}
}