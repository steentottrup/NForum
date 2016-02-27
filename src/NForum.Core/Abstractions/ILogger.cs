using System;

namespace NForum.Core.Abstractions {

	public interface ILogger {
		void DebugWrite(String message);
		void DebugWrite(String message, Exception ex);
		void DebugWriteFormat(String message, params Object[] args);

		void InfoWrite(String message);
		void InfoWrite(String message, Exception ex);
		void InfoWriteFormat(String message, params Object[] args);

		void TraceWrite(String message);
		void TraceWrite(String message, Exception ex);
		void TraceWriteFormat(String message, params Object[] args);

		void ErrorWrite(String message);
		void ErrorWrite(String message, Exception ex);
		void ErrorWriteFormat(String message, params Object[] args);
		void ErrorWrite(Exception ex);
	}
}
