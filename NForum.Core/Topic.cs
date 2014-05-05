using NForum.Core.Abstractions;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Xml.Linq;

namespace NForum.Core {

	public class Topic : ICustomPropertyHolder, IAuthoredElement, ICloneable {
		public Int32 Id { get; set; }
		public Int32 ForumId { get; set; }
		public Int32 AuthorId { get; set; }
		public Int32 EditorId { get; set; }

		public Int32? LatestPostId { get; set; }

		public DateTime Created { get; set; }
		public DateTime Changed { get; set; }

		public TopicState State { get; set; }
		public TopicType Type { get; set; }

		public String Subject { get; set; }
		public String Message { get; set; }

		public String CustomProperties { get; set; }
		public XDocument CustomData { get; set; }

		public virtual Forum Forum { get; set; }
		public virtual User Author { get; set; }
		public virtual User Editor { get; set; }
		public virtual Post LatestPost { get; set; }

		public virtual ICollection<Post> Posts { get; set; }

		public Object Clone() {
			return new Topic {
				Id = this.Id,
				ForumId = this.ForumId,
				AuthorId = this.AuthorId,
				EditorId = this.EditorId,
				LatestPostId = this.LatestPostId,
				Created = this.Created,
				Changed = this.Changed,
				State = this.State,
				Type = this.Type,
				Subject = this.Subject,
				Message = this.Message,
				CustomProperties = this.CustomProperties
			};
		}
	}
}