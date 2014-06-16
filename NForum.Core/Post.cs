﻿using NForum.Core.Abstractions;
using System;
using System.ComponentModel.DataAnnotations;
using System.Xml.Linq;

namespace NForum.Core {

	public class Post : ICustomPropertyHolder, IAuthoredElement, ICloneable {
		public Int32 Id { get; set; }
		public Int32 TopicId { get; set; }
		public Int32 AuthorId { get; set; }
		public Int32 EditorId { get; set; }

		public Int32? ParentPostId { get; set; }

		public DateTime Created { get; set; }
		public DateTime Changed { get; set; }

		public PostState State { get; set; }

		public String Subject { get; set; }
		public String Message { get; set; }

		public String CustomProperties { get; set; }
		public XDocument CustomData { get; set; }

		public virtual Topic Topic { get; set; }
		public virtual User Author { get; set; }
		public virtual User Editor { get; set; }
		public virtual Post ParentPost { get; set; }

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