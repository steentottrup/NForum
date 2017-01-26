using CreativeMinds.CQS.Validators;
using NForum.CQS.Commands.Forums;
using System;

namespace NForum.CQS.Validators.Forums {

	public class CreateForumValidator : IValidator<CreateForumCommand> {

		public ValidationResult Validate(CreateForumCommand command) {
			ValidationResult result = new ValidationResult();

			if (String.IsNullOrWhiteSpace(command.CategoryId) && String.IsNullOrWhiteSpace(command.ParentForumId)) {
				result.AddError("TODO; missing parent", -1);
			}

			if (String.IsNullOrWhiteSpace(command.Name)) {
				result.AddError("TODO; missing name", -1);
			}

			return result;
		}
	}
}
