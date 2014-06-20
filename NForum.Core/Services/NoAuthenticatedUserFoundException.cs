using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace NForum.Core.Services {

	public abstract class NForumException : ApplicationException {
	}

	public class NoAuthenticatedUserFoundException : NForumException {
	}
}