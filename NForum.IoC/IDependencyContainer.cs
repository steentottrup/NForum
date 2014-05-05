using System;
using System.Collections.Generic;

namespace NForum.IoC {

	/// <summary>
	/// Interface for a dependency container.
	/// </summary>
	public interface IDependencyContainer {
		void Register<TService, TImplementation>() where TImplementation : TService;
		void RegisterGeneric(Type service, Type implementation);
		void RegisterGenericPerRequest(Type service, Type implementation);
		void RegisterSingleton<TService>(TService instance);
		void RegisterSingleton<TService, TImplementation>() where TImplementation : TService;
		void RegisterPerRequest<TService, TImplementation>() where TImplementation : TService;
		void RegisterPerRequest<TService, TImplementation>(IDictionary<String, Object> constructorParameters) where TImplementation : TService;
		void Register<TService, TImplementation>(IDictionary<String, Object> constructorParameters) where TImplementation : TService;
		void UnRegister<TService>();

		/// <summary>
		/// Method for configuring the container.
		/// </summary>
		void Configure();
	}
}