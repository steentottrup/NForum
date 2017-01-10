using NForum.Core.Dtos;
using NForum.Core.Refs;
using NForum.Domain.Abstractions;
using System;

namespace NForum.Domain {

	public class Topic : ContentHolder {

		public Topic(ITopicDto data) : base(data) {
			this.Forum = data.Forum;
			this.ForumId = data.Forum.Id;
		}

		public virtual String ForumId { get; protected set; }
		public virtual IForumRef Forum { get; protected set; }
	}
}
