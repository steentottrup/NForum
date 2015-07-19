using NForum.Core.Abstractions;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Xml.Linq;

namespace NForum.Core {

	public class Post :  BasePost, ICustomPropertyHolder, IAuthoredElement, ICloneable {
		public Int32 Id { get; set; }

		public virtual Forum Forum { get; set; }
		public virtual Topic Topic { get; set; }
		public virtual User Author { get; set; }
		public virtual User Editor { get; set; }
		public virtual Post ParentPost { get; set; }

		public virtual ICollection<Attachment> Attachments { get; set; }

		public Object Clone() {
			return new Post {
				Id = this.Id,
				TopicId = this.TopicId,
				AuthorId = this.AuthorId,
				EditorId = this.EditorId,
				ParentPostId = this.ParentPostId,
				Created = this.Created,
				Changed = this.Changed,
				State = this.State,
				Subject = this.Subject,
				Message = this.Message,
				CustomProperties = this.CustomProperties
			};
		}
	}
}