using NForum.Core.Dtos;
using NForum.Domain;
using System;

namespace NForum.Datastores {

	public interface IPermissionDatastore {
		Int32 GetPermission(String forumId, String userId);
	}
}
