using System;
using System.Collections.Generic;
using System.Data.Entity;

namespace NForum.Database.EntityFramework.Repositories {

	public interface IRepository<TEntity> where TEntity : class {
		TEntity Create(TEntity newEntity);
		TEntity FindById(Guid id);
		IDbSet<TEntity> FindAll();
		TEntity Update(TEntity entity);
		void Delete(TEntity entity);
		void DeleteById(Guid id);
	}
}
