using NForum.Core;
using NForum.Core.Abstractions.Data;
using System;

namespace NForum.Persistence.EntityFramework.Repositories {

	public class UserRepository : RepositoryBase<User>, IUserRepository {
		private readonly UnitOfWork uow;

		public UserRepository(UnitOfWork uow)
			: base(uow) {
			this.uow = uow;
		}

	}
}