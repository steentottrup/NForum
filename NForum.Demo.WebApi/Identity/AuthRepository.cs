using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using NForum.Core.Abstractions.Services;
using System;
using System.Threading.Tasks;

namespace NForum.Demo.WebApi.Identity {

	public class AuthRepository : IDisposable {
		private AuthContext _ctx;
		private IUserService userService;

		private UserManager<IdentityUser> _userManager;

		public AuthRepository(IUserService userService) {
			this.userService = userService;
			_ctx = new AuthContext();
			_userManager = new UserManager<IdentityUser>(new UserStore<IdentityUser>(_ctx));
		}

		public IdentityResult RegisterUser(UserModel userModel) {
			IdentityUser user = new IdentityUser {
				UserName = userModel.UserName
			};

			var result = _userManager.Create(user, userModel.Password);

			if (result.Succeeded) {
				// TODO: ?!?!?
				this.userService.Create(user.Id, user.UserName, userModel.Email, "en-GB", "whatnot");
			}

			return result;
		}

		public async Task<IdentityUser> FindUser(String userName, String password) {
			IdentityUser user = await _userManager.FindAsync(userName, password);

			return user;
		}

		public void Dispose() {
			_ctx.Dispose();
			_userManager.Dispose();

		}
	}
}