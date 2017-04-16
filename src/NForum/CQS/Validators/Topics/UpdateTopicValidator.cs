using CreativeMinds.CQS.Validators;
using NForum.CQS.Commands.Topics;
using System;

namespace NForum.CQS.Validators.Topics {

	public class UpdateTopicValidator : IValidator<UpdateTopicCommand> {
		protected readonly IIdValidator idValidator;

		public UpdateTopicValidator(IIdValidator idValidator) {
			this.idValidator = idValidator;
		}

		public ValidationResult Validate(UpdateTopicCommand command) {
			ValidationResult result = new ValidationResult();

			if (String.IsNullOrWhiteSpace(command.Id)) {
				result.AddError("TODO; missing id", -1);
			}
			else {
				if (!this.idValidator.IsValid(command.Id)) {
					result.AddError("TODO; invalid id", -1);
				}
			}

			if (String.IsNullOrWhiteSpace(command.Subject)) {
				result.AddError("TODO; missing subject", -1);
			}

			// TODO: Configurable ????
			if (String.IsNullOrWhiteSpace(command.Content)) {
				result.AddError("TODO; missing content", -1);
			}

			return result;
		}
	}
}
