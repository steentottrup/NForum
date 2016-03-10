using NForum.Core;
using System;

namespace NForum.Database.EntityFramework {

	public static class ForumUserExtensions {

		public static ForumUser ToModel(this Dbos.ForumUser user) {
			return new ForumUser {
				Id = user.Id.ToString(),
				Culture = user.Culture,
				Deleted = user.Deleted,
				EmailAddress = user.EmailAddress,
				ExternalId = user.ExternalId,
				Fullname = user.Fullname,
				Timezone = user.Timezone,
				UseFullname = user.UseFullname,
				Username = user.Username
			};
		}
	}
}
