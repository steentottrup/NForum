using NForum.Core.Dtos;
using NForum.Core.Refs;
using NForum.Domain.Abstractions;
using System;

namespace NForum.Domain {

	public class Reply : ContentHolder {

		public Reply(Topic topic, String subject, String content, ReplyState state) : base(subject, content, null) {
			if (topic == null) {
				throw new ArgumentNullException(nameof(topic));
			}
			this.TopicId = topic.Id;
			this.State = state;
		}

		public Reply(Reply reply, String subject, String content, ReplyState state) : base(subject, content, null) {
			if (reply == null) {
				throw new ArgumentNullException(nameof(reply));
			}
			this.ReplyToId = reply.Id;
			this.TopicId = reply.TopicId;
			this.State = state;
		}

		public Reply(IReplyDto data) : base(data) {
			this.Topic = data.Topic;
			this.TopicId = data.Topic.Id;
			this.ReplyTo = data.ReplyTo;
			this.ReplyToId = data.ReplyTo.Id;
		}

		public virtual String TopicId { get; protected set; }
		public virtual ITopicRef Topic { get; protected set; }

		public virtual String ReplyToId { get; protected set; }
		public virtual IReplyRef ReplyTo { get; protected set; }
		public ReplyState State { get; protected set; }
	}
}
