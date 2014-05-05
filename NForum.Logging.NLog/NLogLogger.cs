using NForum.Core.Abstractions;
using NLog;
using System;

namespace NForum.Logging.NLog {

	public class NLogLogger : ILogger {
		private static Logger logger = LogManager.GetCurrentClassLogger();

		public NLogLogger() {
		}

		public void Write(String message) {
			logger.Debug(message);
		}

		public void Write(String message, Exception ex) {
			logger.Error(String.Format("Error occured, message: {0}, Exception: {1}, stack trace: {2}", message, ex.Message, ex.StackTrace));
		}

		public void WriteFormat(String message, params Object[] args) {
			logger.Debug(String.Format(message, args));
		}

		public void Write(Exception ex) {
			logger.Error(String.Format("Error occured, exception: {0}, stack trace: {1}", ex.Message, ex.StackTrace));
		}
	}
}