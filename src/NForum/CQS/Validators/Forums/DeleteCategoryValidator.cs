using CreativeMinds.CQS.Validators;
using NForum.CQS.Commands.Forums;
using System;

namespace NForum.CQS.Validators.Forums {

	public class DeleteForumValidator : IValidator<DeleteForumCommand> {

		public ValidationResult Validate(DeleteForumCommand command) {
			ValidationResult result = new ValidationResult();

			if (String.IsNullOrWhiteSpace(command.Id)) {
				result.AddError("TODO; missing id", -1);
			}

			return result;
		}
	}
}
