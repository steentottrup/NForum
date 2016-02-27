using NForum.Core.Abstractions;
using System;

namespace NForum.Core.Logging {

	public class NullLogger : ILogger, IApplicationLogger, ICoreLogger {
		public void DebugWrite(string message) {
		}

		public void DebugWrite(string message, Exception ex) {
		}

		public void DebugWriteFormat(string message, params object[] args) {
		}

		public void ErrorWrite(Exception ex) {
		}

		public void ErrorWrite(string message) {
		}

		public void ErrorWrite(string message, Exception ex) {
		}

		public void ErrorWriteFormat(string message, params object[] args) {
		}

		public void InfoWrite(string message) {
		}

		public void InfoWrite(string message, Exception ex) {
		}

		public void InfoWriteFormat(string message, params object[] args) {
		}

		public void TraceWrite(string message) {
		}

		public void TraceWrite(string message, Exception ex) {
		}

		public void TraceWriteFormat(string message, params object[] args) {
		}
	}
}
