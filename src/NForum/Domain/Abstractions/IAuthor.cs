using System;

namespace NForum.Domain.Abstractions {

	public interface IAuthor {
		String Id { get; }
		String Username { get; }
		String Fullname { get; }
	}
}
