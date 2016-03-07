using NForum.Core.Abstractions;
using System;
using System.Net;

namespace NForum.Tests.Common {

	public class FakeUser : IAuthenticatedUser {

		public String EmailAddress { get; set; }

		public String Id { get; set; }

		public IPAddress IPAddress {
			get {
				return IPAddress.Parse("127.0.0.1");
			}
		}

		public String Name { get; set; }

		public String UserAgent {
			get {
				// TODO:
				return "Firefox";
			}
		}
	}
}