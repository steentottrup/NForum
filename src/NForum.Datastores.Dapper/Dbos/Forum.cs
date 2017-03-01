using NForum.Datastores.Dapper.Dbos.Abstractions;
using System;

namespace NForum.Datastores.Dapper.Dbos {

	public class Forum : StructureElement {
		public Int32 CategoryId { get; set; }
		public Int32? ParentForumId { get; set; }
	}
}
