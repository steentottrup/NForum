using System;

namespace NForum.Infrastructure {

	public interface IBoardConfiguration {
		Boolean AllowAnonymousVisitors { get; }
		String GetAdminGroupName();
	}
}
