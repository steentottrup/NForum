using System;

namespace NForum.Core.Abstractions.Services {

	public interface IForumConfigurationService {
		ForumConfiguration Read();
		ForumConfiguration Update(ForumConfiguration config);
		//ForumConfiguration Update(IAddOnConfiguration config);
	}
}