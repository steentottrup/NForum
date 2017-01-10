using NForum.Core.Dtos;
using System;
using System.Collections.Generic;

namespace NForum.Datastores.EF.Dtos {

	public class Category : ICategoryDto {
		public IDictionary<String, Object> CustomProperties { get; set; }

		public String Description { get; set; }

		public String Id { get; set; }

		public String Name { get; set; }

		public Int32 SortOrder { get; set; }
	}
}
