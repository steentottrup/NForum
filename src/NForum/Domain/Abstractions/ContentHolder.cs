using NForum.Core.Dtos;
using NForum.Core.Refs;
using System;
using System.Collections.Generic;

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

		protected virtual Boolean EditorSet { get; set; }
		public virtual void SetEditor(IAuthor editor) {
			if (editor == null) {
				throw new ArgumentNullException(nameof(editor));
			}
			this.LastEdited = DateTime.UtcNow;
			// TODO:
			//this.LastEditedBy = new IAuthorRef
			this.EditorSet = true;
		}

		public virtual void SetSubject(String newSubject) {
			if (String.IsNullOrWhiteSpace(newSubject)) {
				throw new ArgumentNullException(nameof(newSubject));
			}
			this.ValidateEditorOrFail();
			this.Subject = newSubject;
		}

		public virtual void SetContent(String newContent) {
			if (String.IsNullOrWhiteSpace(newContent)) {
				throw new ArgumentNullException(nameof(newContent));
			}
			this.ValidateEditorOrFail();
			this.Content = newContent;
		}

		public override void AddCustomProperty(String key, Object value) {
			this.ValidateEditorOrFail();
			base.AddCustomProperty(key, value);
		}

		public override void ClearAndAddProperties(IDictionary<String, Object> newProperties) {
			this.ValidateEditorOrFail();
			base.ClearAndAddProperties(newProperties);
		}

		public override void ClearCustomProperties() {
			this.ValidateEditorOrFail();
			base.ClearCustomProperties();
		}

		public override void RemoveCustomProperty(String key) {
			this.ValidateEditorOrFail();
			base.RemoveCustomProperty(key);
		}

		protected void ValidateEditorOrFail() {
			if (!this.EditorSet) {
				// TODO:
				throw new Exception("You need to set the editor before changing properties");
			}
		}

		public virtual String Subject { get; protected set; }
		public virtual String Content { get; protected set; }

		public virtual IAuthorRef CreatedBy { get; protected set; }
		public virtual DateTime Created { get; protected set; }
		public virtual IAuthorRef LastEditedBy { get; protected set; }
		public virtual DateTime LastEdited { get; protected set; }
	}
}
