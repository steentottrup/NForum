using System;
using System.Collections.Generic;

namespace NForum.IoC {

	public interface IDependencyResolver {
		T GetService<T>();
		Object GetService(Type serviceType);
		IEnumerable<T> GetServices<T>();
		IEnumerable<Object> GetServices(Type serviceType);
		void Register(Type serviceType, Func<Object> activator);
		void Register(Type serviceType, IEnumerable<Func<Object>> activators);
	}
}