using NForum.Domain;
using System;
using System.Collections.Generic;

namespace NForum.CQS.Queries {

	public class CategoriesAndForums {
		public IEnumerable<Category> Categories { get; set; }
		public IEnumerable<Forum> Forums { get; set; }
	}
}
