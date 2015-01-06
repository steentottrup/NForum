using NForum.Core.Abstractions;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace NForum.Demo.WebApi.Providers {

	public class WebApiState : IState {

		public Uri Url {
			get {
				throw new NotImplementedException();
			}
		}

		public String LocalPath {
			get {
				throw new NotImplementedException();
			}
		}

		public IEnumerable<KeyValuePair<String, String>> QueryString {
			get {
				throw new NotImplementedException();
			}
		}

		public IEnumerable<KeyValuePair<String, String>> Headers {
			get {
				throw new NotImplementedException();
			}
		}

		public Task<IEnumerable<KeyValuePair<String, String>>> ReadForm() {
			throw new NotImplementedException();
		}
	}
}