using System;

namespace NForum.Database.EntityFramework.Repositories {

	public class GenericRepository<TEntity> : RepositoryBase<TEntity> where TEntity : class {

		public GenericRepository(NForumDbContext context) : base(context) { }
	}
}
