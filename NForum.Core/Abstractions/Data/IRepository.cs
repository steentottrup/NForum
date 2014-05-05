using System;
using System.Collections.Generic;
using System.Linq.Expressions;

namespace NForum.Core.Abstractions.Data {

	public interface IRepository<TEntity> where TEntity : class {
		TEntity Create(TEntity newEntity);
		TEntity Read(Int32 id);
		IEnumerable<TEntity> ReadAll();
		TEntity Update(TEntity entity);
		void Delete(TEntity entity);
		void Delete(Int32 id);
	}
}