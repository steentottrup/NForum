using CreativeMinds.CQS.Validators;
using FluentAssertions;
using NUnit.Framework;
using System;
using System.Linq;

namespace NForum.Tests.Core.ValidationTests {

	[TestFixture]
	public class CategoryTests {

		[Test(Author = "Steen F. Tøttrup", Description = "Test that the validation fails when the name is empty")]
		public void CreateCategoryWithEmptyName() {
			NForum.CQS.Commands.Categories.CreateCategoryCommand create = new CQS.Commands.Categories.CreateCategoryCommand {
				Name = String.Empty
			};

			NForum.CQS.Validators.Categories.CreateCategoryValidator validator = new CQS.Validators.Categories.CreateCategoryValidator();

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

		[Test(Author = "Steen F. Tøttrup", Description = "Test that the validation fails when the id and name is empty")]
		public void UpdateCategoryWithEmptyNameAndId() {
			NForum.CQS.Commands.Categories.UpdateCategoryCommand update = new CQS.Commands.Categories.UpdateCategoryCommand {
				Id = String.Empty,
				Name = String.Empty
			};

			NForum.CQS.Validators.Categories.UpdateCategoryValidator validator = new CQS.Validators.Categories.UpdateCategoryValidator();

			ValidationResult result = validator.Validate(update);
			result.IsValid.Should().Be(false, "An empty id/name is not allowed");
			result.Errors.Count().Should().Be(2, "An empty name and/or id is not allowed");

			update.Id = null;
			update.Name = null;
			result = validator.Validate(update);
			result.IsValid.Should().Be(false, "An empty id/name is not allowed");

			update.Id = "\t";
			update.Name = "\t";
			result = validator.Validate(update);
			result.IsValid.Should().Be(false, "An empty id/name is not allowed");

			update.Id = "something";
			update.Name = String.Empty;
			result = validator.Validate(update);
			result.IsValid.Should().Be(false, "An empty name is not allowed");

			update.Id = String.Empty;
			update.Name = "my name";
			result = validator.Validate(update);
			result.IsValid.Should().Be(false, "An empty name is not allowed");
		}

		[Test(Author = "Steen F. Tøttrup", Description = "Test that the validation fails when the id is empty")]
		public void DeleteCategoryWithEmptyId() {
			NForum.CQS.Commands.Categories.DeleteCategoryCommand delete = new CQS.Commands.Categories.DeleteCategoryCommand {
				Id = String.Empty
			};

			NForum.CQS.Validators.Categories.DeleteCategoryValidator validator = new CQS.Validators.Categories.DeleteCategoryValidator();

			ValidationResult result = validator.Validate(delete);
			result.IsValid.Should().Be(false, "An empty id is not allowed");

			delete.Id = "\t";
			result = validator.Validate(delete);
			result.IsValid.Should().Be(false, "An empty id is not allowed");

			delete.Id = null;
			result = validator.Validate(delete);
			result.IsValid.Should().Be(false, "An empty id is not allowed");
		}

		[Test(Author = "Steen F. Tøttrup", Description = "Test that the validation succeeds when the name is not empty")]
		public void CreateCategoryWithName() {
			NForum.CQS.Commands.Categories.CreateCategoryCommand create = new CQS.Commands.Categories.CreateCategoryCommand {
				Name = "Just enything"
			};

			NForum.CQS.Validators.Categories.CreateCategoryValidator validator = new CQS.Validators.Categories.CreateCategoryValidator();

			ValidationResult result = validator.Validate(create);
			result.IsValid.Should().Be(true, "A name was provided");
		}

		[Test(Author = "Steen F. Tøttrup", Description = "Test that the validation fails when the id and name is empty")]
		public void UpdateCategoryWithNameAndId() {
			NForum.CQS.Commands.Categories.UpdateCategoryCommand update = new CQS.Commands.Categories.UpdateCategoryCommand {
				Id = "e",
				Name = "meh"
			};

			NForum.CQS.Validators.Categories.UpdateCategoryValidator validator = new CQS.Validators.Categories.UpdateCategoryValidator();

			ValidationResult result = validator.Validate(update);
			result.IsValid.Should().Be(true, "A name and id was provide");
		}

		[Test(Author = "Steen F. Tøttrup", Description = "Test that the validation fails when the id is empty")]
		public void DeleteCategoryWithId() {
			NForum.CQS.Commands.Categories.DeleteCategoryCommand delete = new CQS.Commands.Categories.DeleteCategoryCommand {
				Id = "fe"
			};

			NForum.CQS.Validators.Categories.DeleteCategoryValidator validator = new CQS.Validators.Categories.DeleteCategoryValidator();

			ValidationResult result = validator.Validate(delete);
			result.IsValid.Should().Be(true, "An id was provided");
		}
	}
}
