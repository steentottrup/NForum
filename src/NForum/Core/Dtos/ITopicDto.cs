using NForum.Core.Refs;
using System;

namespace NForum.Core.Dtos {

	public interface ITopicDto : IContentHolder {
		IForumRef Forum { get; }
	}
}
