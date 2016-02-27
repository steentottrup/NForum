using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace NForum.Core.Abstractions {

	/// <summary>
	/// Represents a request
	/// </summary>
	public interface IState {
		/// <summary>
		/// Gets the url for this request.
		/// </summary>
		Uri Url { get; }
		/// <summary>
		/// The local path part of the url
		/// </summary>
		String LocalPath { get; }
		/// <summary>
		/// Gets the querystring for this request.
		/// </summary>
		IEnumerable<KeyValuePair<String, String>> QueryString { get; }
		/// <summary>
		/// Gets the headers for this request.
		/// </summary>
		IEnumerable<KeyValuePair<String, String>> Headers { get; }
		/// <summary>
		/// Reads the form of the http request
		/// </summary>
		/// <returns></returns>
		Task<IEnumerable<KeyValuePair<String, String>>> ReadForm();
	}
}
