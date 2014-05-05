using NForum.Core.Abstractions.Data;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;

namespace NForum.Persistence.EntityFramework.Repositories {

	public abstract class RepositoryBase<TEntity> : IRepository<TEntity> where TEntity : class {
		protected IDbSet<TEntity> set;
		protected DbContext context;

		protected RepositoryBase(DbContext context) {
			this.set = context.Set<TEntity>();
		}

		public virtual TEntity Create(TEntity newEntity) {
			return this.set.Add(newEntity);
		}

		public virtual TEntity Read(Int32 id) {
			return this.set.Find(id);
		}

		public IEnumerable<TEntity> ReadAll() {
			return this.set;
		}

		public virtual TEntity Update(TEntity entity) {
			// TODO: ???
			return entity;
		}

		public virtual void Delete(TEntity entity) {
			this.set.Remove(entity);
		}

		public virtual void Delete(Int32 id) {
			this.Delete(this.set.Find(id));
		}
	}
}