using NForum.Core.Abstractions;
using System;

namespace NForum.Core.Events {

	public class CategoryUpdated {
		public IAuthenticatedUser Author { get; set; }
		public String CategoryId { get; set; }
		public String Name { get; set; }
	}
}
