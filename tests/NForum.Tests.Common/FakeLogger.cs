using NForum.Core.Abstractions;
using NForum.Core.Logging;
using System;
using System.Text;

namespace NForum.Tests.Common {

	public class FakeLogger : LoggingBase, IApplicationLogger, ICoreLogger {
		private readonly StringBuilder debugLog = new StringBuilder();
		private readonly StringBuilder infoLog = new StringBuilder();
		private readonly StringBuilder errorLog = new StringBuilder();
		private readonly StringBuilder traceLog = new StringBuilder();

		public override void DebugWrite(String message, Exception ex) {
			String msg = message;
			// TODO: Exception!!
			if (!String.IsNullOrWhiteSpace(msg)) {
				this.debugLog.AppendLine(message);
			}
		}

		public override void ErrorWrite(String message, Exception ex) {
			String msg = message;
			// TODO: Exception!!
			if (!String.IsNullOrWhiteSpace(msg)) {
				this.errorLog.AppendLine(message);
			}
		}

		public override void InfoWrite(String message, Exception ex) {
			String msg = message;
			// TODO: Exception!!
			if (!String.IsNullOrWhiteSpace(msg)) {
				this.infoLog.AppendLine(message);
			}
		}

		public override void TraceWrite(String message, Exception ex) {
			String msg = message;
			// TODO: Exception!!
			if (!String.IsNullOrWhiteSpace(msg)) {
				this.traceLog.AppendLine(message);
			}
		}
	}
}
