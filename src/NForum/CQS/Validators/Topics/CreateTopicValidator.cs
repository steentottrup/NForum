using CreativeMinds.CQS.Validators;
using NForum.CQS.Commands.Topics;
using System;

namespace NForum.CQS.Validators.Topics {

	public class CreateTopicValidator : IValidator<CreateTopicCommand> {

		public ValidationResult Validate(CreateTopicCommand command) {
			ValidationResult result = new ValidationResult();

			if (String.IsNullOrWhiteSpace(command.ForumId)) {
				result.AddError("TODO; missing parent", -1);
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
