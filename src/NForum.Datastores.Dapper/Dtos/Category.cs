using NForum.Core.Dtos;
using NForum.Core.Refs;
using System;
using System.Collections.Generic;

namespace NForum.Datastores.Dapper.Dtos {

	public class Category : ICategoryDto, ICategoryRef {
		public IDictionary<String, Object> CustomProperties { get; set; }
		public String Description { get; set; }
		public String Id { get; set; }
		public String Name { get; set; }
		public Int32 SortOrder { get; set; }
	}
}
