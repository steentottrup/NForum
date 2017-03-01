using CreativeMinds.CQS.Validators;
using NForum.CQS.Commands.Forums;
using System;

namespace NForum.CQS.Validators.Forums {

	public class DeleteForumValidator : IValidator<DeleteForumCommand> {
		protected readonly IIdValidator idValidator;

		public DeleteForumValidator(IIdValidator idValidator) {
			this.idValidator = idValidator;
		}

		public ValidationResult Validate(DeleteForumCommand command) {
			ValidationResult result = new ValidationResult();

			if (String.IsNullOrWhiteSpace(command.Id)) {
				result.AddError("TODO; missing id", -1);
			}
			else {
				if (!this.idValidator.IsValid(command.Id)) {
					result.AddError("TODO; invalid id", -1);
				}
			}

			return result;
		}
	}
}
