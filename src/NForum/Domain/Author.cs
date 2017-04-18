using NForum.Core.Refs;
using NForum.Domain.Abstractions;
using System;

namespace NForum.Domain {

	public class Author : IAuthor {
		public String Id { get; protected set; }

		public String Name { get; protected set; }

		//public String GetFullname() {
		//	throw new NotImplementedException();
		//}

		//public String GetId() {
		//	return this.Id;
		//}

		//public String GetUsername() {
		//	throw new NotImplementedException();
		//}
	}
}
