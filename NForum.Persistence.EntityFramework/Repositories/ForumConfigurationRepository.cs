using NForum.Core;
using NForum.Core.Abstractions.Data;
using System;
using System.Data.Entity;
using System.Linq;

namespace NForum.Persistence.EntityFramework.Repositories {

	public class ForumConfigurationRepository : IForumConfigurationRepository {
		protected IDbSet<ForumConfiguration> set;
		protected DbContext context;

		public ForumConfigurationRepository(UnitOfWork uow) {
			this.set = uow.ForumConfigurations;
			this.context = uow;
		}

		public ForumConfiguration Create(ForumConfiguration config) {
			ForumConfiguration entity = this.set.Add(config);
			this.context.SaveChanges();
			return entity;
		}

		public ForumConfiguration ByName(String name) {
			return this.set.FirstOrDefault(b => b.Name == name);
		}

		public ForumConfiguration Update(ForumConfiguration config) {
			// TODO: ???
			this.context.SaveChanges();
			return config;
		}
	}
}