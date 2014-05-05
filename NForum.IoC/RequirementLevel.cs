using System;

namespace NForum.IoC {

	public enum RequirementLevel {
		/// <summary>
		/// Use this if the application or part of it will fail because of this requirement missing.
		/// </summary>
		Fatal,
		/// <summary>
		/// Use this if part of the application will not work as expected because of this requirement missing.
		/// This could be a missing SMTP configuration, the application will work, but no e-mails will be sent.
		/// </summary>
		Warning,
		/// <summary>
		/// Information, use this for debugging
		/// </summary>
		Info
	}
}