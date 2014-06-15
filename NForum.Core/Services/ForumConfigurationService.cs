using NForum.Core.Abstractions;
using NForum.Core.Abstractions.Data;
using NForum.Core.Abstractions.Services;
using System;

namespace NForum.Core.Services {

	public class ForumConfigurationService : IForumConfigurationService {
		private IForumConfigurationRepository forumConfigRepo;
		private const String baseConfigName = "NForum";

		public ForumConfigurationService(IForumConfigurationRepository forumConfigRepo) {
			this.forumConfigRepo = forumConfigRepo;
		}

		public ForumConfiguration Read() {
			return this.Read(baseConfigName);
		}

		private ForumConfiguration Read(String name) {
			ForumConfiguration config = this.forumConfigRepo.ByName(name);
			if (config == null) {
				config = this.forumConfigRepo.Create(new ForumConfiguration {
					Name = name
				});
			}

			return config;
		}

		public ForumConfiguration Update(ForumConfiguration config) {
			if (config == null) {
				throw new ArgumentNullException("config");
			}
			return this.forumConfigRepo.Update(config);
		}

		//public ForumConfiguration Update(IAddOnConfiguration config) {
		//	if (config == null) {
		//		throw new ArgumentNullException("config");
		//	}
		//	return this.forumConfigRepo.Update(config);
		//}
	}
}