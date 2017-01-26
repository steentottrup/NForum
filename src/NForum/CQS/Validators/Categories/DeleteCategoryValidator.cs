using CreativeMinds.CQS.Validators;
using NForum.CQS.Commands.Categories;
using System;

namespace NForum.CQS.Validators.Categories {

	public class DeleteCategoryValidator : IValidator<DeleteCategoryCommand> {

		public ValidationResult Validate(DeleteCategoryCommand command) {
			ValidationResult result = new ValidationResult();

			if (String.IsNullOrWhiteSpace(command.Id)) {
				result.AddError("TODO; missing id", -1);
			}

			return result;
		}
	}
}
