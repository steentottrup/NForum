using NForum.Core.Dtos;
using NForum.Core.Refs;
using System;

namespace NForum.Domain.Abstractions {

	public abstract class ContentHolder : CustomPropertiesHolder {

		public ContentHolder(String subject, String content, IAuthor author) : base() {
			this.Subject = subject;
			this.Content = content;
			this.Created = this.LastEdited = DateTime.UtcNow;
			//this.CreatedBy = this.LastEditedBy
			// TODO:
		}

		protected ContentHolder(IContentHolder data) : base(data) {
			this.Subject = data.Subject;
			this.Content = data.Content;
			this.CreatedBy = data.CreatedBy;
			this.Created = data.Created;
			this.LastEditedBy = data.LastEditedBy;
			this.LastEdited = data.LastEdited;
		}

		public virtual String Subject { get; protected set; }
		public virtual String Content { get; protected set; }

		public virtual IAuthorRef CreatedBy { get; protected set; }
		public virtual DateTime Created { get; protected set; }
		public virtual IAuthorRef LastEditedBy { get; protected set; }
		public virtual DateTime LastEdited { get; protected set; }
	}
}
