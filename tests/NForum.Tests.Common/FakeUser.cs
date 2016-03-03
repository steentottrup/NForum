using NForum.Core.Abstractions;
using System;
using System.Net;

namespace NForum.Tests.Common {
	public class FakeUser : IAuthenticatedUser {

		public String EmailAddress {
			get {
				return "mr-fake@fake.com";
			}
		}

		public String Id {
			get {
				return Guid.NewGuid().ToString();
			}
		}

		public IPAddress IPAddress {
			get {
				return IPAddress.Parse("127.0.0.1");
			}
		}

		public String Name {
			get {
				return "Mr. Fake User";
			}
		}

		public String UserAgent {
			get {
				// TODO:
				return "Firefox";
			}
		}
	}
}