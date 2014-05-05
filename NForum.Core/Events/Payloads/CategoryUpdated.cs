using System;

namespace NForum.Core.Events.Payloads {

	public class CategoryUpdated {
		public Category Category { get; set; }
		public Category UpdatedCategory { get; set; }
	}
}