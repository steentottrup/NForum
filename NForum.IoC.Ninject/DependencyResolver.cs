using Ninject;
using Ninject.Parameters;
using System;
using System.Collections.Generic;

namespace NForum.IoC.Ninject {

	public class DependencyResolver : System.Web.Mvc.IDependencyResolver, NForum.IoC.IDependencyResolver {
		private readonly IKernel kernel;

		public DependencyResolver(IKernel kernel) {
			this.kernel = kernel;
		}

		public object GetService(Type serviceType) {
			return this.kernel.TryGet(serviceType, new IParameter[0]);
		}

		public IEnumerable<Object> GetServices(Type serviceType) {
			return this.kernel.GetAll(serviceType, new IParameter[0]);
		}

		public T GetService<T>() {
			return this.kernel.TryGet<T>(new IParameter[0]);
		}

		public IEnumerable<T> GetServices<T>() {
			return this.kernel.GetAll<T>(new IParameter[0]);
		}
	}
}