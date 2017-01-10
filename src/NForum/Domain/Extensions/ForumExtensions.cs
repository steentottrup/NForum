using NForum.Core.Dtos;
using System;

namespace NForum.Domain {

	public static class ForumExtensions {

		public static Forum ToDomainObject(this IForumDto dto) {
			return new Forum(dto);
		}
	}
}
