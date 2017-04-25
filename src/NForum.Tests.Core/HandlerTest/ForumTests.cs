using CreativeMinds.CQS.Validators;
using NForum.Core.Dtos;
using NForum.CQS.Commands.Forums;
using NForum.CQS.Validators.Forums;
using NForum.Datastores;
using NForum.Infrastructure;
using NSubstitute;
using NUnit.Framework;
using System;
using System.Collections.Generic;

namespace NForum.Tests.Core.HandlerTests {

	[TestFixture]
	public class ForumTests {

		/*
			Tests needed?

			- Create forum (with wrong category id)
			- Create forum as child (with wrong category id)
			 
			 */

		[Test(Author = "Steen F. Tøttrup", Description = "Make sure that Create method gets called on the datastore, when a valid CreateForumCommand is provided")]
		public void CreateForum() {
			var parentCategory = Substitute.For<Domain.Category>("category", 1, "desc");
			parentCategory.Id.Returns("1");

			var inputParameter = new Domain.Forum(parentCategory, "name", 1, "description");
			var command = new CreateForumCommand { CategoryId = parentCategory.Id, Name = inputParameter.Name, SortOrder = inputParameter.SortOrder, Description = inputParameter.Description };
			var dto = Substitute.For<IForumDto>();

			var categoryDatastore = Substitute.For<ICategoryDatastore>();
			var datalayerCategory = Substitute.For<ICategoryDto>();
			datalayerCategory.Id.Returns("1");
			categoryDatastore.ReadById(parentCategory.Id).Returns(datalayerCategory);

			var datastore = Substitute.For<IForumDatastore>();
			datastore.Create(inputParameter).Returns<IForumDto>(dto);
			var taskDatastore = Substitute.For<ITaskDatastore>();

			CreateForumCommandHandler handler = new CreateForumCommandHandler(categoryDatastore, datastore, taskDatastore);
			GenericValidationCommandHandlerDecorator<CreateForumCommand> val =
					new GenericValidationCommandHandlerDecorator<CreateForumCommand>(
						handler,
						new List<IValidator<NForum.CQS.Commands.Forums.CreateForumCommand>> { new NForum.CQS.Validators.Forums.CreateForumValidator(TestUtils.GetInt32IdValidator()) }
					);

			val.Execute(command);

			datastore.ReceivedWithAnyArgs(1).Create(inputParameter);
		}

		[Test(Author = "Steen F. Tøttrup", Description = "Make sure that Update method gets called on the datastore, when a valid UpdateForumCommand is provided")]
		public void CreateForumAsForumChild() {
			var parentCategory = Substitute.For<Domain.Category>("category", 1, "desc");
			parentCategory.Id.Returns("1");

			var parentForum = Substitute.For<Domain.Forum>(parentCategory, "category", 1, "desc");
			parentForum.Id.Returns("1");

			var inputParameter = new Domain.Forum(parentForum, "name", 1, "description");
			var command = new CreateForumCommand { ParentForumId = parentForum.Id, Name = inputParameter.Name, SortOrder = inputParameter.SortOrder, Description = inputParameter.Description };
			var dto = Substitute.For<IForumDto>();

			var categoryDatastore = Substitute.For<ICategoryDatastore>();
			var datalayerCategory = Substitute.For<ICategoryDto>();
			datalayerCategory.Id.Returns("1");
			categoryDatastore.ReadById(parentCategory.Id).Returns(datalayerCategory);

			var datalayerForum = Substitute.For<IForumDto>();
			datalayerForum.Id.Returns("1");

			var datastore = Substitute.For<IForumDatastore>();
			datastore.CreateAsForumChild(inputParameter).Returns<IForumDto>(dto);
			var taskDatastore = Substitute.For<ITaskDatastore>();

			CreateForumCommandHandler handler = new CreateForumCommandHandler(categoryDatastore, datastore, taskDatastore);
			GenericValidationCommandHandlerDecorator<CreateForumCommand> val =
					new GenericValidationCommandHandlerDecorator<CreateForumCommand>(
						handler,
						new List<IValidator<NForum.CQS.Commands.Forums.CreateForumCommand>> { new NForum.CQS.Validators.Forums.CreateForumValidator(TestUtils.GetInt32IdValidator()) }
					);

			val.Execute(command);

			datastore.ReceivedWithAnyArgs(1).CreateAsForumChild(inputParameter);
		}

		[Test(Author = "Steen F. Tøttrup", Description = "Make sure that Update method gets called on the datastore, when a valid UpdateCategoryCommand is provided")]
		public void UpdateForum() {
			var parentCategory = Substitute.For<Domain.Category>("category", 1, "desc");
			parentCategory.Id.Returns("1");

			var inputParameter = new Domain.Forum(parentCategory, "name", 1, "description");
			var command = new UpdateForumCommand { Id = "1", Name = inputParameter.Name, SortOrder = inputParameter.SortOrder, Description = inputParameter.Description };
			var dto = Substitute.For<IForumDto>();

			var datastore = Substitute.For<IForumDatastore>();
			datastore.Update(inputParameter).Returns<IForumDto>(dto);
			var taskDatastore = Substitute.For<ITaskDatastore>();

			UpdateForumCommandHandler handler = new UpdateForumCommandHandler(datastore, taskDatastore);
			GenericValidationCommandHandlerDecorator<UpdateForumCommand> val =
					new GenericValidationCommandHandlerDecorator<UpdateForumCommand>(
						handler,
						new List<IValidator<UpdateForumCommand>> { new UpdateForumValidator(TestUtils.GetInt32IdValidator()) }
					);

			val.Execute(command);

			datastore.ReceivedWithAnyArgs(1).Update(inputParameter);
		}
	}
}
