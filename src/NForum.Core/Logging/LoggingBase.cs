using NForum.Core.Abstractions;
using System;

namespace NForum.Core.Logging {

	public abstract class LoggingBase : ILogger {

		public virtual void DebugWrite(String message) {
			this.DebugWrite(message, null);
		}

		public abstract void DebugWrite(String message, Exception ex);

		public virtual void DebugWriteFormat(String message, params Object[] args) {
			this.DebugWrite(String.Format(message, args), null);
		}

		public virtual void ErrorWrite(Exception ex) {
			this.DebugWrite(String.Empty, ex);
		}

		public virtual void ErrorWrite(String message) {
			this.ErrorWrite(message, null);
		}

		public abstract void ErrorWrite(String message, Exception ex);

		public virtual void ErrorWriteFormat(String message, params Object[] args) {
			this.ErrorWrite(String.Format(message, args));
		}

		public virtual void InfoWrite(String message) {
			this.InfoWrite(message, null);
		}

		public abstract void InfoWrite(String message, Exception ex);

		public virtual void InfoWriteFormat(String message, params Object[] args) {
			this.InfoWrite(String.Format(message, args));
		}

		public virtual void TraceWrite(String message) {
			this.TraceWrite(message, null);
		}

		public abstract void TraceWrite(String message, Exception ex);

		public virtual void TraceWriteFormat(String message, params Object[] args) {
			this.TraceWrite(String.Format(message, args));
		}
	}
}
