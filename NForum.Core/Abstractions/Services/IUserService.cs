using System;

namespace NForum.Core.Abstractions.Services {

	/// <summary>
	/// The UserService interface.
	/// </summary>
	public interface IUserService {
		/// <summary>
		/// Method for reading a user.
		/// </summary>
		/// <param name="id">The id of the user needed.</param>
		/// <returns>The user with the given id, or null.</returns>
		User Read(Int32 id);
		/// <summary>
		/// Method for reading a user.
		/// </summary>
		/// <param name="name">The name of the user needed.</param>
		/// <returns>The user with the given name, or null.</returns>
		User Read(String name);

		User Create(String providerId, String name, String emailAddress, String culture, String timezone);
	}
}