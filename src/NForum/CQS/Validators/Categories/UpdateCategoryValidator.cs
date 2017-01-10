using CreativeMinds.CQS.Validators;
using NForum.CQS.Commands.Categories;
using System;
using System.Threading.Tasks;

namespace NForum.CQS.Validators.Categories {

	public class UpdateCategoryValidator : IValidator<UpdateCategoryCommand> {
		public ValidationResult Validate(UpdateCategoryCommand command) {
			throw new NotImplementedException();
		}

		//public Task<ValidationResult> ValidateAsync(UpdateCategoryCommand command) {
		//	throw new NotImplementedException();
		//}
	}
}
