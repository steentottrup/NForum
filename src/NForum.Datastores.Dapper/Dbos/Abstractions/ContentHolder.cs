using System;

namespace NForum.Datastores.Dapper.Dbos.Abstractions {

	public abstract class ContentHolder : CustomPropertiesHolder {
		public String Subject { get; set; }
		public String Content { get; set; }
		public Int32 CreatedBy { get; set; }
		public DateTime Created { get; set; }
		public Int32 LastEditedBy { get; set; }
		public DateTime LastEdited { get; set; }
	}
}
