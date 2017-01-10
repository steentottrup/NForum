using CreativeMinds.CQS.Queries;
using NForum.Core.Dtos;
using NForum.Datastores;
using NForum.Domain;
using System;
using System.Linq;
using System.Security.Principal;
using System.Threading.Tasks;

namespace NForum.CQS.Queries {

	public class ReadCategoriesWithForumsQueryHandler : IQueryHandler<ReadCategoriesWithForumsQuery, CategoriesAndForums> {
		private readonly ICategoryDatastore datastore;
		private readonly IPrincipal currentUser;

		public ReadCategoriesWithForumsQueryHandler(ICategoryDatastore datastore, IPrincipal user) {
			this.datastore = datastore;
			this.currentUser = user;
		}

		public CategoriesAndForums Handle(ReadCategoriesWithForumsQuery query) {
			ICategoriesAndForumsDto data = this.datastore.ReadAllWithForums();

			// TODO: filter out what the user does not have access to!!

			return new CategoriesAndForums {
				Categories = data.Categories.Select(c => c.ToDomainObject()),
				Forums = data.Forums.Select(f => f.ToDomainObject())
			};
		}

		public Task<CategoriesAndForums> HandleAsync(ReadCategoriesWithForumsQuery query) {
			throw new NotImplementedException();
		}
	}
}
