using NForum.Core.Abstractions;
using System;

namespace NForum.Tests.CommonMocks {

	public class ConsoleLogger : ILogger {

		public void Write(String message) {
			Console.WriteLine(message);
		}

		public void Write(String message, Exception ex) {
			this.WriteFormat(message + Environment.NewLine + "{0}" + Environment.NewLine + "{1}", ex.Message, ex.StackTrace);
		}

		public void WriteFormat(String message, params Object[] args) {
			Console.WriteLine(String.Format(message, args));
		}

		public void Write(Exception ex) {
			this.WriteFormat("", ex);
		}
	}
}