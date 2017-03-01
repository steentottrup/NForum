using System;
using System.Collections.Generic;

namespace NForum.Datastores.Dapper {

	public interface IRepository<TEntity> where TEntity : class {
		TEntity Create(TEntity newEntity);
		TEntity FindById(Int32 id);
		IEnumerable<TEntity> FindAll();
		TEntity Update(TEntity entity);
		void Delete(TEntity entity);
		void DeleteById(Int32 id);
	}
}
