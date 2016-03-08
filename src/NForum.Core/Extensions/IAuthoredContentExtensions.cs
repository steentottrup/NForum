using NForum.Core.Abstractions;
using System;

namespace NForum.Core {

	public static class IAuthoredContentExtensions {

		public static Boolean ModeratorChanged(this IAuthoredContent content) {
			return content.EditorId != content.CreatorId;
		}
	}
}
