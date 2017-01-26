using NForum.Core.Refs;
using System;
using System.Collections.Generic;

namespace NForum.Core.Dtos {

	public interface IForumDto : IStrutureElementDto {
		ICategoryRef Category { get; }
		IForumRef ParentForum { get; }
		IEnumerable<String> Path { get; }
	}
}
