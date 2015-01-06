using Microsoft.AspNet.Identity;
using NForum.Demo.WebApi.Identity;
using System;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web.Http;

namespace NForum.Demo.WebApi.Controllers {

	[RoutePrefix("api/account")]
	public class AccountController : ApiController {
		private AuthRepository _repo = null;

		public AccountController(AuthRepository repo) {
			this._repo = repo;
		}

		[HttpPost]
		[Route("Register")]
		public HttpResponseMessage Register(UserModel userModel) {
			IdentityResult result = _repo.RegisterUser(userModel);
			return this.Request.CreateResponse<String>("ok");
		}

		private IHttpActionResult GetErrorResult(IdentityResult result) {
			if (result == null) {
				return InternalServerError();
			}

			if (!result.Succeeded) {
				if (result.Errors != null) {
					foreach (string error in result.Errors) {
						ModelState.AddModelError("", error);
					}
				}

				if (ModelState.IsValid) {
					// No ModelState errors are available to send, so just return an empty BadRequest.
					return BadRequest();
				}

				return BadRequest(ModelState);
			}

			return null;
		}
	}
}