using CreativeMinds.CQS.Validators;
using NForum.CQS.Commands.Forums;
using System;

namespace NForum.CQS.Validators.Forums {

	public class CreateForumValidator : IValidator<CreateForumCommand> {
		protected readonly IIdValidator idValidator;

		public CreateForumValidator(IIdValidator idValidator) {
			this.idValidator = idValidator;
		}

		public ValidationResult Validate(CreateForumCommand command) {
			ValidationResult result = new ValidationResult();

			if (String.IsNullOrWhiteSpace(command.CategoryId) && String.IsNullOrWhiteSpace(command.ParentForumId)) {
				result.AddError("TODO; missing parent", -1);
			}

			if (!String.IsNullOrWhiteSpace(command.CategoryId) && !this.idValidator.IsValid(command.CategoryId)) {
				result.AddError("TODO; invalid category id", -1);
			}

			if (!String.IsNullOrEmpty(command.ParentForumId) && !this.idValidator.IsValid(command.ParentForumId)) {
				result.AddError("TODO; invalid parent forum id", -1);
			}

			if (String.IsNullOrWhiteSpace(command.Name)) {
				result.AddError("TODO; missing name", -1);
			}

			return result;
		}
	}
}
