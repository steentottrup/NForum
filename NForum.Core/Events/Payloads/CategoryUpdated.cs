using System;

namespace NForum.Core.Events.Payloads {

	public class CategoryUpdated {

		public CategoryUpdated(Category original) {
			this.OriginalCategory = original;
		}

		public Category OriginalCategory { get; private set; }
		public Category UpdatedCategory { get; set; }
	}
}