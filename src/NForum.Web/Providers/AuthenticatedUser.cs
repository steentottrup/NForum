using System;
using System.Net;
using NForum.Core.Abstractions;

namespace NForum.Web.Providers {

	public class AuthenticatedUser : IAuthenticatedUser {

		public String EmailAddress { get; set; }

		public String Id { get; set; }

		public IPAddress IPAddress { get; set; }

		public String Name { get; set; }

		public String UserAgent { get; set; }
	}
}