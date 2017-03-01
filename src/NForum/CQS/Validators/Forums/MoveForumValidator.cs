using CreativeMinds.CQS.Validators;
using NForum.CQS.Commands.Forums;
using System;

namespace NForum.CQS.Validators.Forums {

	public class MoveForumValidator : IValidator<MoveForumCommand> {
		protected readonly IIdValidator idValidator;

		public MoveForumValidator(IIdValidator idValidator) {
			this.idValidator = idValidator;
		}

		public ValidationResult Validate(MoveForumCommand command) {
			ValidationResult result = new ValidationResult();

			if (String.IsNullOrWhiteSpace(command.Id)) {
				result.AddError("TODO; missing id", -1);
			}
			else {
				if (!this.idValidator.IsValid(command.Id)) {
					result.AddError("TODO; invalid id", -1);
				}
			}

			if (String.IsNullOrWhiteSpace(command.DestinationCategoryId) && String.IsNullOrWhiteSpace(command.DestinationForumId)) {
				result.AddError("TODO; missing parent", -1);
			}

			if (!String.IsNullOrWhiteSpace(command.DestinationCategoryId) && !this.idValidator.IsValid(command.DestinationCategoryId)) {
				result.AddError("TODO; invalid destination category id", -1);
			}

			if (!String.IsNullOrWhiteSpace(command.DestinationForumId) && !this.idValidator.IsValid(command.DestinationForumId)) {
				result.AddError("TODO; invalid destination forum id", -1);
			}

			return result;
		}
	}
}
