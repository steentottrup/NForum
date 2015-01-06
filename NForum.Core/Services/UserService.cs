using NForum.Core.Abstractions.Data;
using NForum.Core.Abstractions.Services;
using System;

namespace NForum.Core.Services {

	public class UserService : IUserService {
		protected readonly IUserRepository userRepository;

		public UserService(IUserRepository userRepository) {
			this.userRepository = userRepository;
		}

		public User Read(Int32 id) {
			throw new NotImplementedException();
		}

		public User Read(String name) {
			throw new NotImplementedException();
		}

		public User Create(String providerId, String name, String emailAddress, String culture, String timezone) {
			// TODO: Validate input!
			return this.userRepository.Create(new User {
				ProviderId = providerId,
				Name = name,
				EmailAddress = emailAddress,
				Culture = culture,
				TimeZone = timezone
			});
		}
	}
}