using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace NForum.Core.Abstractions {
    /// <summary>
    /// Represents a request
    /// </summary>
	public interface IRequest{
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
        INameValueCollection QueryString { get; }

        /// <summary>
        /// Gets the headers for this request.
        /// </summary>
        INameValueCollection Headers { get; }

        /// <summary>
        /// Gets the owin environment
        /// </summary>
        IDictionary<String, Object> Environment { get; }

        /// <summary>
        /// Reads the form of the http request
        /// </summary>
        /// <returns></returns>
        Task<INameValueCollection> ReadForm();
    }
}