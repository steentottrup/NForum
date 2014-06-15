using Microsoft.Owin;
using NForum.Core.Abstractions;
using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace NForum.Core.Owin {

	public class WebServerRequest : IRequest {
		private INameValueCollection queryString;
		private INameValueCollection headers;

		private readonly OwinRequest request;

		public WebServerRequest(IDictionary<String, Object> environment) {
			this.request = new OwinRequest(environment);
		}

		public Uri Url {
			get {
				return this.request.Uri;
			}
		}

		public String LocalPath {
			get {
				return (this.request.PathBase + this.request.Path).Value;
			}
		}

		public INameValueCollection QueryString {
			get {
				return LazyInitializer.EnsureInitialized(
					ref this.queryString, () => {
						return new ReadableStringCollectionWrapper(this.request.Query);
					});
			}
		}

		public INameValueCollection Headers {
			get {
				return LazyInitializer.EnsureInitialized(
					ref this.headers, () => {
						return new ReadableStringCollectionWrapper(this.request.Headers);
					});
			}
		}

		public IDictionary<String, Object> Environment {
			get {
				return this.request.Environment;
			}
		}

		public async Task<INameValueCollection> ReadForm() {
			IFormCollection form = await this.request.ReadFormAsync();
			return new ReadableStringCollectionWrapper(form);
		}
	}
}
