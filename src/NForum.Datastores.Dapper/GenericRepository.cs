using Dapper;
using NForum.Datastores.Dapper.DatAnnotations;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Reflection;

namespace NForum.Datastores.Dapper {

	public class GenericRepository<TEntity> : IRepository<TEntity> where TEntity : class {
		protected readonly IDbConnection connection;

		public GenericRepository(IDbConnection connection) {
			this.connection = connection;
		}

		public TEntity Create(TEntity newEntity) {
			PropertyContainer propertyContainer = ParseProperties(newEntity);
			String sql = $"INSERT INTO [{typeof(TEntity).Name}] ({String.Join(", ", propertyContainer.ValueNames)}) VALUES (@{String.Join(", @", propertyContainer.ValueNames)}) SELECT CAST(scope_identity() AS int)";

			Int32 id = this.connection.Query<Int32>(sql, propertyContainer.ValuePairs, commandType: CommandType.Text).First();
			SetId(newEntity, id, propertyContainer.IdPairs);
			return newEntity;
		}

		public void Delete(TEntity entity) {
			Tuple<String, PropertyContainer> arguments = PrepareDelete(entity);
			Execute(this.connection, CommandType.Text, arguments.Item1, arguments.Item2.IdPairs);
		}

		public void DeleteById(Int32 id) {
			Tuple<String, PropertyContainer> arguments = PrepareDeleteById(id);
			Execute(this.connection, CommandType.Text, arguments.Item1, arguments.Item2);
		}

		public IEnumerable<TEntity> FindAll() {
			return this.connection.Query<TEntity>(PrepareFindAll(), commandType: CommandType.Text);
		}

		public TEntity FindById(Int32 id) {
			Tuple<String, PropertyContainer> arguments = PrepareFindById(id);
			return this.connection.Query<TEntity>(arguments.Item1, arguments.Item2.IdPairs, commandType: CommandType.Text).SingleOrDefault();
		}

		public TEntity Update(TEntity entity) {
			Tuple<String, PropertyContainer> arguments = PrepareUpdate(entity);
			Execute(this.connection, CommandType.Text, arguments.Item1, arguments.Item2.AllPairs);
			return entity;
		}



		private static Tuple<String, PropertyContainer> PrepareUpdate(TEntity entity) {
			PropertyContainer propertyContainer = ParseProperties(entity);
			String sqlIdPairs = GetSqlPairs(propertyContainer.IdNames);
			String sqlValuePairs = GetSqlPairs(propertyContainer.ValueNames);
			String sql = $"UPDATE [{typeof(TEntity).Name}] SET {sqlValuePairs} WHERE {sqlIdPairs}";

			return new Tuple<String, PropertyContainer>(sql, propertyContainer);
		}

		private static Tuple<String, PropertyContainer> PrepareFindById(Int32 id) {
			PropertyContainer propertyContainer = new PropertyContainer();
			propertyContainer.AddId("Id", id);
			String sql = $"SELECT * FROM [{typeof(TEntity).Name}] WHERE Id = @Id";

			return new Tuple<String, PropertyContainer>(sql, propertyContainer);
		}

		private static String PrepareFindAll() {
			return $"SELECT * FROM [{typeof(TEntity).Name}]";
		}

		private static Tuple<String, PropertyContainer> PrepareDeleteById(Int32 id) {
			PropertyContainer propertyContainer = new PropertyContainer();
			propertyContainer.AddId("Id", id);
			String sql = $"DELETE FROM [{typeof(TEntity).Name}] WHERE Id=@Id";

			return new Tuple<String, PropertyContainer>(sql, propertyContainer);
		}

		private static Int32 Execute(IDbConnection connection, CommandType commandType, String sql, Object parameters = null) {
			return connection.Execute(sql, parameters, commandType: commandType);
		}

		private static Tuple<String, PropertyContainer> PrepareDelete(TEntity entity) {
			PropertyContainer propertyContainer = ParseProperties(entity);
			String sqlIdPairs = GetSqlPairs(propertyContainer.IdNames);
			String sql = $"DELETE FROM [{typeof(TEntity).Name}] WHERE {sqlIdPairs}";

			return new Tuple<String, PropertyContainer>(sql, propertyContainer);
		}

		private static void SetId<T>(T obj, Int32 id, IDictionary<String, Object> propertyPairs) {
			if (propertyPairs.Count == 1) {
				String propertyName = propertyPairs.Keys.First();
				PropertyInfo propertyInfo = obj.GetType().GetProperty(propertyName);
				if (propertyInfo.PropertyType == typeof(Int32)) {
					propertyInfo.SetValue(obj, id, null);
				}
			}
		}

		private static string GetSqlPairs(IEnumerable<String> keys, String separator = ", ") {
			List<String> pairs = keys.Select(key => $"{key} = @{key}").ToList();
			return String.Join(separator, pairs);
		}

		// No idea where I got this code, but I will out credit on, as soon as I relocate it online.

		private static PropertyContainer ParseProperties<T>(T obj) {
			PropertyContainer propertyContainer = new PropertyContainer();

			String typeName = typeof(T).Name;
			String[] validKeyNames = new String[] { "Id", $"{typeName}Id", $"{typeName}_Id" };

			PropertyInfo[] properties = typeof(T).GetProperties();
			foreach (var property in properties) {
				// Skip reference types (but still include string!)
				if (property.PropertyType.GetTypeInfo().IsClass && property.PropertyType != typeof(string))
					continue;

				// Skip methods without a public setter
				if (property.GetSetMethod() == null)
					continue;

				// Skip methods specifically ignored
				if (property.IsDefined(typeof(DapperIgnoreAttribute), false))
					continue;

				var name = property.Name;
				var value = typeof(T).GetProperty(property.Name).GetValue(obj, null);

				if (property.IsDefined(typeof(DapperKey), false) || validKeyNames.Contains(name)) {
					propertyContainer.AddId(name, value);
				}
				else {
					propertyContainer.AddValue(name, value);
				}
			}

			return propertyContainer;
		}

		private class PropertyContainer {
			private readonly Dictionary<string, object> _ids;
			private readonly Dictionary<string, object> _values;

			#region Properties

			internal IEnumerable<string> IdNames {
				get { return _ids.Keys; }
			}

			internal IEnumerable<string> ValueNames {
				get { return _values.Keys; }
			}

			internal IEnumerable<string> AllNames {
				get { return _ids.Keys.Union(_values.Keys); }
			}

			internal IDictionary<string, object> IdPairs {
				get { return _ids; }
			}

			internal IDictionary<string, object> ValuePairs {
				get { return _values; }
			}

			internal IEnumerable<KeyValuePair<string, object>> AllPairs {
				get { return _ids.Concat(_values); }
			}

			#endregion

			#region Constructor

			internal PropertyContainer() {
				_ids = new Dictionary<string, object>();
				_values = new Dictionary<string, object>();
			}

			#endregion

			#region Methods

			internal void AddId(string name, object value) {
				_ids.Add(name, value);
			}

			internal void AddValue(string name, object value) {
				_values.Add(name, value);
			}

			#endregion
		}
	}
}
