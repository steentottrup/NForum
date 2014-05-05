using System;

namespace NForum.Core.Abstractions {

	public interface ILogger {
		void Write(String message);
		void Write(String message, Exception ex);
		void WriteFormat(String message, params Object[] args);
		void Write(Exception ex);
	}
}