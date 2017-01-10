using NForum.Core.Refs;
using System;

namespace NForum.Core.Dtos {

	public interface IForumDto : IStrutureElementDto {
		ICategoryRef Category { get; }
		IForumRef ParentForum { get; }
	}
}
