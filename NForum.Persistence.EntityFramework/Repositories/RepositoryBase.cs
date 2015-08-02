using NForum.Core.Abstractions.Data;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.Core.Objects;
using System.Linq;
using System.Linq.Expressions;

namespace NForum.Persistence.EntityFramework.Repositories {

	public abstract class RepositoryBase<TEntity> : IRepository<TEntity> where TEntity : class {
		protected IDbSet<TEntity> set;
		protected UnitOfWork context;

		protected RepositoryBase(UnitOfWork context) {
			this.context = context;
			this.set = this.context.Set<TEntity>();
		}

		protected IQueryable<TEntity> Set {
			get {
				return this.set.AsNoTracking<TEntity>();
			}
		}

		public virtual TEntity Create(TEntity newEntity) {
			TEntity entity = this.set.Add(newEntity);
			this.context.SaveChanges();
			return entity;
		}

		public virtual TEntity Read(Expression<Func<TEntity, Boolean>> expression) {
			return this.set.SingleOrDefault(expression);

			return this.set.AsNoTracking<TEntity>().SingleOrDefault(expression);
		}

		public IEnumerable<TEntity> ReadAll() {
			return this.Set;
		}

		public virtual TEntity Update(TEntity entity) {
			//this.set.Attach(entity);
			this.context.SaveChanges();
			return entity;
		}
		public virtual void Delete(TEntity entity) {
			this.set.Remove(entity);
			this.context.SaveChanges();
		}
		//public virtual void Delete(Int32 id) {
		//	this.Delete(this.Read(id));
		//}
	}
}