using System;

namespace NForum.Datastores.Dapper.Dbos.Abstractions {

	public abstract class CustomPropertiesHolder {
		public Int32 Id { get; set; }
		public String CustomProperties { get; set; }
	}
}
