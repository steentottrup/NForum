using NForum.Core.Dtos;
using NForum.Core.Refs;
using NForum.Domain.Abstractions;
using System;

namespace NForum.Domain {

	public class Forum : StructureElement {

		public Forum(IForumDto data) : base(data) {
			this.Category = data.Category;
			this.CategoryId = data.Category.Id;
			this.ParentForum = data.ParentForum;
			this.ParentForumId = data.ParentForum.Id;
		}

		public virtual String ParentForumId { get; protected set; }
		public virtual IForumRef ParentForum { get; protected set; }

		public virtual String CategoryId { get; protected set; }
		public virtual ICategoryRef Category { get; protected set; }
	}
}
