using System;

namespace NForum.Core.Abstractions.Providers {

	public interface IUserProvider {
		User CurrentUser { get; }
		Boolean Authenticated { get; }
	}
}