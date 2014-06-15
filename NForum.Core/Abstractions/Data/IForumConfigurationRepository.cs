using System;

namespace NForum.Core.Abstractions.Data {

	public interface IForumConfigurationRepository {
		ForumConfiguration Create(ForumConfiguration config);
		ForumConfiguration ByName(String name);
		ForumConfiguration Update(ForumConfiguration config);
	}
}