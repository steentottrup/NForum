using System;
using System.Collections.Generic;
using System.Data.Entity;

namespace NForum.Database.EntityFramework.Repositories {

	public abstract class RepositoryBase<TEntity> : IRepository<TEntity> where TEntity : class {
		protected readonly IDbSet<TEntity> set;
		private readonly NForumDbContext context;

		protected RepositoryBase(NForumDbContext context) {
			this.context = context;
			this.set = this.context.Set<TEntity>();
		}

		public virtual TEntity Create(TEntity newEntity) {
			TEntity entity = this.set.Add(newEntity);
			this.context.SaveChanges();
			return entity;
		}

		public virtual TEntity FindById(Guid id) {
			TEntity e = this.set.Find(id);
			return e;
		}

		public IDbSet<TEntity> FindAll() {
			return this.set;
		}

		public virtual TEntity Update(TEntity entity) {
			// TODO: ???
			this.context.SaveChanges();
			return entity;
		}

		public virtual void Delete(TEntity entity) {
			this.set.Remove(entity);
			this.context.SaveChanges();
		}

		public virtual void DeleteById(Guid id) {
			this.Delete(this.set.Find(id));
		}
	}
}