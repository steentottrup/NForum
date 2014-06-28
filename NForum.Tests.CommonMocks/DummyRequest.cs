using NForum.Core.Abstractions;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace NForum.Tests.CommonMocks {

	public class DummyRequest : IState {

		public Uri Url {
			get { throw new NotImplementedException(); }
		}

		public string LocalPath {
			get { throw new NotImplementedException(); }
		}

		public IEnumerable<KeyValuePair<string, string>> QueryString {
			get { throw new NotImplementedException(); }
		}

		public IEnumerable<KeyValuePair<string, string>> Headers {
			get { throw new NotImplementedException(); }
		}

		public Task<IEnumerable<KeyValuePair<string, string>>> ReadForm() {
			throw new NotImplementedException();
		}
	}
}