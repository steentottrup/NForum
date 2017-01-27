using CreativeMinds.CQS.Validators;
using FluentAssertions;
using NForum.Core.Dtos;
using NForum.CQS.Commands.Categories;
using NForum.CQS.Validators.Categories;
using NForum.Datastores;
using NSubstitute;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;

namespace NForum.Tests.Core.HandlerTests {

	[TestFixture]
	public class CategoryTests {

		[Test(Author = "Steen F. Tøttrup", Description = "Make sure that Create mthod gets called on the datastore, when a valid CreateCategoryCommand is provided")]
		public void CreateCategory() {
			var inputParameter = new Domain.Category("name", 1, "description");
			var command = new CreateCategoryCommand { Name = inputParameter.Name, SortOrder = inputParameter.SortOrder, Description = inputParameter.Description };
			var dto = Substitute.For<ICategoryDto>();

			var datastore = Substitute.For<ICategoryDatastore>();
			datastore.Create(inputParameter).Returns<ICategoryDto>(dto);

			CreateCategoryCommandHandler handler = new CreateCategoryCommandHandler(datastore);
			GenericValidationCommandHandlerDecorator<CreateCategoryCommand> val =
					new GenericValidationCommandHandlerDecorator<CreateCategoryCommand>(
						handler,
						new List<IValidator<NForum.CQS.Commands.Categories.CreateCategoryCommand>> { new NForum.CQS.Validators.Categories.CreateCategoryValidator() }
					);

			val.Execute(command);

			datastore.ReceivedWithAnyArgs(1).Create(inputParameter);
		}

		[Test(Author = "Steen F. Tøttrup", Description = "Make sure that Update mthod gets called on the datastore, when a valid UpdateCategoryCommand is provided")]
		public void UpdateCategory() {
			var inputParameter = new Domain.Category("name", 1, "description");
			var command = new UpdateCategoryCommand { Id = "1", Name = inputParameter.Name, SortOrder = inputParameter.SortOrder, Description = inputParameter.Description };
			var dto = Substitute.For<ICategoryDto>();

			var datastore = Substitute.For<ICategoryDatastore>();
			datastore.Create(inputParameter).Returns<ICategoryDto>(dto);

			UpdateCategoryCommandHandler handler = new UpdateCategoryCommandHandler(datastore);
			GenericValidationCommandHandlerDecorator<UpdateCategoryCommand> val =
					new GenericValidationCommandHandlerDecorator<UpdateCategoryCommand>(
						handler,
						new List<IValidator<UpdateCategoryCommand>> { new UpdateCategoryValidator() }
					);

			val.Execute(command);

			datastore.ReceivedWithAnyArgs(1).Update(inputParameter);
		}
	}
}
