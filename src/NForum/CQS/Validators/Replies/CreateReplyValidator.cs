using CreativeMinds.CQS.Validators;
using NForum.CQS.Commands.Replies;
using System;

namespace NForum.CQS.Validators.Replies  {

	public class CreateReplyValidator : IValidator<CreateReplyCommand> {

		public ValidationResult Validate(CreateReplyCommand command) {
			ValidationResult result = new ValidationResult();

			if (String.IsNullOrWhiteSpace(command.TopicId) && String.IsNullOrWhiteSpace(command.ParentReplyId)) {
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
