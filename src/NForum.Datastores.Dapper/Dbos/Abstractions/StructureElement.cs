using System;

namespace NForum.Datastores.Dapper.Dbos.Abstractions {

	public abstract class StructureElement : CustomPropertiesHolder {
		public String Name { get; set; }
		public Int32 SortOrder { get; set; }
		public String Description { get; set; }
	}
}
