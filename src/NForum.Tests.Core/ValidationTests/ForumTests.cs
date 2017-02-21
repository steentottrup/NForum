using CreativeMinds.CQS.Validators;
using FluentAssertions;
using NUnit.Framework;
using System;
using System.Linq;

namespace NForum.Tests.Core.ValidationTests {

	[TestFixture]
	public class ForumTests {

		[Category("Validations")]
		[Test(Author = "Steen F. Tøttrup", Description = "Test that the validation fails when the name is empty")]
		public void CreateForumWithEmptyName() {
			NForum.CQS.Commands.Forums.CreateForumCommand create = new CQS.Commands.Forums.CreateForumCommand {
				CategoryId = "1",
				Name = String.Empty
			};

			NForum.CQS.Validators.Forums.CreateForumValidator validator = new CQS.Validators.Forums.CreateForumValidator();

			ValidationResult result = validator.Validate(create);
			result.IsValid.Should().Be(false, "An empty name is not allowed");

			create.Name = "    ";
			result = validator.Validate(create);
			result.IsValid.Should().Be(false, "An empty name is not allowed");

			create.Name = "\t";
			result = validator.Validate(create);
			result.IsValid.Should().Be(false, "An empty name is not allowed");

			create.Name = null;
			result = validator.Validate(create);
			result.IsValid.Should().Be(false, "An empty name is not allowed");
		}

		[Category("Validations")]
		[Test(Author = "Steen F. Tøttrup", Description = "Test that the validation fails when the ids are empty")]
		public void CreateForumWithEmptyIds() {
			NForum.CQS.Commands.Forums.CreateForumCommand create = new CQS.Commands.Forums.CreateForumCommand {
				CategoryId = String.Empty,
				ParentForumId = String.Empty,
				Name = "hep"
			};

			NForum.CQS.Validators.Forums.CreateForumValidator validator = new CQS.Validators.Forums.CreateForumValidator();

			ValidationResult result = validator.Validate(create);
			result.IsValid.Should().Be(false, "Empty ids is not allowed");

			create.CategoryId = "    ";
			create.ParentForumId = "    ";
			result = validator.Validate(create);
			result.IsValid.Should().Be(false, "Empty ids is not allowed");

			create.CategoryId = "\t";
			create.ParentForumId = "\t";
			result = validator.Validate(create);
			result.IsValid.Should().Be(false, "Empty ids is not allowed");

			create.CategoryId = null;
			create.ParentForumId = null;
			result = validator.Validate(create);
			result.IsValid.Should().Be(false, "Empty ids is not allowed");
		}

		[Test(Author = "Steen F. Tøttrup", Description = "Test that the validation fails when the id and name is empty")]
		public void UpdateForumWithEmptyNameAndId() {
			NForum.CQS.Commands.Forums.UpdateForumCommand update = new CQS.Commands.Forums.UpdateForumCommand {
				Id = String.Empty,
				Name = String.Empty
			};

			NForum.CQS.Validators.Forums.UpdateForumValidator validator = new NForum.CQS.Validators.Forums.UpdateForumValidator();

			ValidationResult result = validator.Validate(update);
			result.IsValid.Should().Be(false, "An empty id/name is not allowed");
			result.Errors.Count().Should().Be(2, "An empty name and/or id is not allowed");

			update.Id = null;
			update.Name = null;
			result = validator.Validate(update);
			result.IsValid.Should().Be(false, "An empty id/name is not allowed");
			result.Errors.Count().Should().Be(2, "An empty name and/or id is not allowed");

			update.Id = "\t";
			update.Name = "\t";
			result = validator.Validate(update);
			result.IsValid.Should().Be(false, "An empty id/name is not allowed");
			result.Errors.Count().Should().Be(2, "An empty name and/or id is not allowed");

			update.Id = "something";
			update.Name = String.Empty;
			result = validator.Validate(update);
			result.IsValid.Should().Be(false, "An empty name is not allowed");

			update.Id = String.Empty;
			update.Name = "my name";
			result = validator.Validate(update);
			result.IsValid.Should().Be(false, "An empty id is not allowed");
		}

		[Test(Author = "Steen F. Tøttrup", Description = "Test that the validation fails when the id is empty")]
		public void DeleteCategoryWithEmptyId() {
			NForum.CQS.Commands.Forums.DeleteForumCommand delete = new NForum.CQS.Commands.Forums.DeleteForumCommand {
				Id = String.Empty
			};

			NForum.CQS.Validators.Forums.DeleteForumValidator validator = new NForum.CQS.Validators.Forums.DeleteForumValidator();

			ValidationResult result = validator.Validate(delete);
			result.IsValid.Should().Be(false, "An empty id is not allowed");

			delete.Id = "\t";
			result = validator.Validate(delete);
			result.IsValid.Should().Be(false, "An empty id is not allowed");

			delete.Id = null;
			result = validator.Validate(delete);
			result.IsValid.Should().Be(false, "An empty id is not allowed");
		}

		[Category("Validations")]
		[Test(Author = "Steen F. Tøttrup", Description = "Test that the validation succeeds when the name and parent forum id or category id is not empty")]
		public void CreateForumWithNameAndParentId() {
			NForum.CQS.Commands.Forums.CreateForumCommand create = new NForum.CQS.Commands.Forums.CreateForumCommand {
				Name = "Just anything",
				ParentForumId = "hep"
			};

			NForum.CQS.Validators.Forums.CreateForumValidator validator = new CQS.Validators.Forums.CreateForumValidator();

			ValidationResult result = validator.Validate(create);
			result.IsValid.Should().Be(true, "A name and parent forum or category id was provided");

			create.ParentForumId = String.Empty;
			create.CategoryId = "he";

			result = validator.Validate(create);
			result.IsValid.Should().Be(true, "A name and parent forum or category id was provided");
		}

		[Category("Validations")]
		[Test(Author = "Steen F. Tøttrup", Description = "Test that the validation succeeds when the id and name is not empty")]
		public void UpdateForumWithNameAndId() {
			NForum.CQS.Commands.Forums.UpdateForumCommand update = new CQS.Commands.Forums.UpdateForumCommand {
				Id = "e",
				Name = "meh"
			};

			NForum.CQS.Validators.Forums.UpdateForumValidator validator = new NForum.CQS.Validators.Forums.UpdateForumValidator();

			ValidationResult result = validator.Validate(update);
			result.IsValid.Should().Be(true, "A name and id was provide");
		}

		[Category("Validations")]
		[Test(Author = "Steen F. Tøttrup", Description = "Test that the validation succeeds when the id is not empty")]
		public void DeleteForumWithId() {
			NForum.CQS.Commands.Categories.DeleteCategoryCommand delete = new CQS.Commands.Categories.DeleteCategoryCommand {
				Id = "fe"
			};

			NForum.CQS.Validators.Categories.DeleteCategoryValidator validator = new CQS.Validators.Categories.DeleteCategoryValidator();

			ValidationResult result = validator.Validate(delete);
			result.IsValid.Should().Be(true, "An id was provided");
		}
	}
}
