using CreativeMinds.CQS.Validators;
using NForum.CQS.Commands.Forums;
using System;

namespace NForum.CQS.Validators.Forums {

	public class UpdateForumValidator : IValidator<UpdateForumCommand> {
		protected readonly IIdValidator idValidator;

		public UpdateForumValidator(IIdValidator idValidator) {
			this.idValidator = idValidator;
		}

		public ValidationResult Validate(UpdateForumCommand command) {
			ValidationResult result = new ValidationResult();

			if (String.IsNullOrWhiteSpace(command.Id)) {
				result.AddError("TODO; missing id", -1);
			}
			else {
				if (!this.idValidator.IsValid(command.Id)) {
					result.AddError("TODO; invalid id", -1);
				}
			}

			if (String.IsNullOrWhiteSpace(command.Name)) {
				result.AddError("TODO; missing name", -1);
			}

			return result;
		}
	}
}
