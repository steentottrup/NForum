using CreativeMinds.CQS.Validators;
using NForum.CQS.Commands.Categories;
using System;
using System.Threading.Tasks;

namespace NForum.CQS.Validators.Categories {

	public class DeleteCategoryValidator : IValidator<DeleteCategoryCommand> {
		public ValidationResult Validate(DeleteCategoryCommand command) {
			throw new NotImplementedException();
		}

		//public Task<ValidationResult> ValidateAsync(DeleteCategoryCommand command) {
		//	throw new NotImplementedException();
		//}
	}
}
