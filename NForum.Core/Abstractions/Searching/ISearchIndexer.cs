using System;
using System.Collections.Generic;

namespace NForum.Core.Abstractions.Searching {

	public interface ISearchIndexer {
		void Index(Topic topic, Boolean includePosts);
		void Index(Post post);
		void Remove(Forum forum);
		void Remove(Topic topic);
		void Remove(Post post);
		void Clear();

		Boolean Enabled { get; }
		String Name { get; }
	}
}