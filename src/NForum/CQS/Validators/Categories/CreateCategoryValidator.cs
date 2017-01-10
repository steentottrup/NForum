using CreativeMinds.CQS.Validators;
using NForum.CQS.Commands.Categories;
using System;
using System.Threading.Tasks;

namespace NForum.CQS.Validators.Categories {

	public class CreateCategoryValidator : IValidator<CreateCategoryCommand> {


		public ValidationResult Validate(CreateCategoryCommand command) {
			ValidationResult result = new ValidationResult();

			if (String.IsNullOrWhiteSpace(command.Name)) {
				result.AddError("TODO; missing name", -1);
			}

			return result;
		}

		//public Task<ValidationResult> ValidateAsync(CreateCategoryCommand command) {
		//	ValidationResult result = new ValidationResult();

		//	if (String.IsNullOrWhiteSpace(command.Name)) {
		//		result.AddError("TODO; missing name", -1);
		//	}

		//	return new Task<ValidationResult>(() => {
		//		return result;
		//	});
		//}
	}
}
