using System;
using System.Data.Entity.Migrations;

namespace NForum.Persistence.EntityFramework.Migrations {

	public class MigrationConf : DbMigrationsConfiguration<UnitOfWork> {

		public MigrationConf() : base() {
			this.AutomaticMigrationsEnabled = true;
		}
	}
}