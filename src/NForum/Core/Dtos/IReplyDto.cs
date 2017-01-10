using NForum.Core.Refs;
using System;

namespace NForum.Core.Dtos {

	public interface IReplyDto : IContentHolder {
		IReplyRef ReplyTo { get; }
		ITopicRef Topic { get; }
	}
}
