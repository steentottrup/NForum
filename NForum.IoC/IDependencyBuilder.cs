using System;
using System.Collections.Generic;

namespace NForum.IoC {

	/// <summary>
	/// Dependency builder interface.
	/// </summary>
	public interface IDependencyBuilder {
		/// <summary>
		/// Method for configuring the container.
		/// </summary>
		/// <param name="container"></param>
		void Configure(IDependencyContainer container);
		/// <summary>
		/// Validate the builders requirements.
		/// </summary>
		/// <param name="feedback">Container for returned feedback.</param>
		void ValidateRequirements(IList<ApplicationRequirement> feedback);
	}
}
