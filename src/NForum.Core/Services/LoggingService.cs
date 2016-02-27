using NForum.Core.Abstractions.Services;
using System;
using NForum.Core.Abstractions;

namespace NForum.Core.Services {

	public class LoggingService : ILoggingService {
		protected readonly ICoreLogger coreLogger;
		protected readonly IApplicationLogger applicationLogger;

		public LoggingService(ICoreLogger coreLogger, IApplicationLogger appLogger) {
			this.coreLogger = coreLogger;
			this.applicationLogger = appLogger;
		}

		public IApplicationLogger Application { get { return this.applicationLogger; } }

		public ICoreLogger Core { get { return this.coreLogger; } }
	}
}
