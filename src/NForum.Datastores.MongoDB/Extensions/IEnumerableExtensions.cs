using System;
using System.Collections.Generic;
using System.Linq;

namespace NForum.Datastores.MongoDB {

	public static class IEnumerableExtensions {

		public static IEnumerable<Dbos.ForumStructure> Flatten(this IEnumerable<Dbos.ForumStructure> elements) {
			return elements.SelectMany(e => e.Forums.Flatten()).Concat(elements);
		}
	}
}
