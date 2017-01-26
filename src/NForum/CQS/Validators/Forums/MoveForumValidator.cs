using CreativeMinds.CQS.Validators;
using NForum.CQS.Commands.Forums;
using System;

namespace NForum.CQS.Validators.Forums {

	public class MoveForumValidator : IValidator<MoveForumCommand> {

		public ValidationResult Validate(MoveForumCommand command) {
			ValidationResult result = new ValidationResult();

			if (String.IsNullOrWhiteSpace(command.Id)) {
				result.AddError("TODO; missing id", -1);
			}

			if (String.IsNullOrWhiteSpace(command.DestinationCategoryId) && String.IsNullOrWhiteSpace(command.DestinationForumId)) {
				result.AddError("TODO; missing parent", -1);
			}

			return result;
		}
	}
}
