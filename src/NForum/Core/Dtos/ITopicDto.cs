using NForum.Core.Refs;
using NForum.Domain;
using System;

namespace NForum.Core.Dtos {

	public interface ITopicDto : IContentHolder {
		IForumRef Forum { get; }
		TopicState State { get; }
		TopicType Type { get; }
	}
}
