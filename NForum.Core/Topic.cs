using NForum.Core.Abstractions;
using System;
using System.Collections.Generic;
using System.Xml.Linq;

namespace NForum.Core {

	/// <summary>
	/// Class representing a topic in a forum
	/// </summary>
	public class Topic : BaseTopic, ICustomPropertyHolder, IAuthoredElement, ICloneable {
		/// <summary>
		/// The unique identifier of the topic
		/// </summary>
		public Int32 Id { get; set; }

		/// <summary>
		/// The parent forum
		/// </summary>
		public virtual Forum Forum { get; set; }
		/// <summary>
		/// The topic author
		/// </summary>
		public virtual User Author { get; set; }
		/// <summary>
		/// The latest editor of the topic
		/// </summary>
		public virtual User Editor { get; set; }
		//public virtual Post LatestPost { get; set; }

		/// <summary>
		/// Collection of posts belonging to the topic
		/// </summary>
		public virtual ICollection<Post> Posts { get; set; }

		public Object Clone() {
			return new Topic {
				Id = this.Id,
				ForumId = this.ForumId,
				AuthorId = this.AuthorId,
				EditorId = this.EditorId,
				//LatestPostId = this.LatestPostId,
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