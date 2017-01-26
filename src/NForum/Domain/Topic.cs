using NForum.Core.Dtos;
using NForum.Core.Refs;
using NForum.Domain.Abstractions;
using System;

namespace NForum.Domain {

	public class Topic : ContentHolder {

		// TODO:
		public Topic(Forum forum, String subject, String content, TopicType type, TopicState state) : base(subject, content, null) {
			this.ForumId = forum.Id;
			this.Type = type;
			this.State = state;
		}

		public Topic(ITopicDto data) : base(data) {
			this.Forum = data.Forum;
			this.ForumId = data.Forum.Id;
		}

		public virtual String ForumId { get; protected set; }
		public virtual IForumRef Forum { get; protected set; }
		public virtual TopicType Type { get; protected set; }
		public TopicState State { get; protected set; }
	}
}
