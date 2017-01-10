using NForum.Core.Dtos;
using NForum.Core.Refs;
using NForum.Domain.Abstractions;
using System;

namespace NForum.Domain {

	public class Reply : ContentHolder {

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
	}
}
