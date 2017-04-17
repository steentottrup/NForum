using CreativeMinds.CQS.Validators;
using NForum.Core.Dtos;
using NForum.CQS.Commands.Forums;
using NForum.CQS.Commands.Topics;
using NForum.CQS.Validators.Forums;
using NForum.CQS.Validators.Topics;
using NForum.Datastores;
using NForum.Domain.Abstractions;
using NForum.Infrastructure;
using NSubstitute;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Security.Principal;

namespace NForum.Tests.Core.HandlerTests {

	[TestFixture]
	public class TopicTests {

		/*
			Tests needed?

			- Create forum (with wrong category id)
			- Create forum as child (with wrong category id)
			 
			 */

		[Test(Author = "Steen F. Tøttrup", Description = "Make sure that Create method gets called on the datastore, when a valid CreateTopicCommand is provided")]
		public void CreateTopic() {
			var parentCategory = Substitute.For<Domain.Category>("category", 1, "desc");
			parentCategory.Id.Returns("1");

			var parentForum = Substitute.For<Domain.Forum>(parentCategory, "forum", 1, "desc");
			parentForum.Id.Returns("1");

			var inputParameter = new Domain.Topic(parentForum, "topic subject", "bla bla bla ", Domain.TopicType.Regular);
			var command = new CreateTopicCommand { ForumId = parentForum.Id, Content = inputParameter.Content, State = inputParameter.State, Subject = inputParameter.Subject, Type = inputParameter.Type };
			var dto = Substitute.For<ITopicDto>();

			var categoryDatastore = Substitute.For<ICategoryDatastore>();
			var datalayerCategory = Substitute.For<ICategoryDto>();
			datalayerCategory.Id.Returns("1");
			var forumDatastore = Substitute.For<IForumDatastore>();
			var datalayerForum = Substitute.For<IForumDto>();
			datalayerForum.Id.Returns("1");
			categoryDatastore.ReadById(parentCategory.Id).Returns(datalayerCategory);
			forumDatastore.ReadById(parentForum.Id).Returns(datalayerForum);

			var datastore = Substitute.For<ITopicDatastore>();
			datastore.Create(inputParameter).Returns<ITopicDto>(dto);

			var taskDatastore = Substitute.For<ITaskDatastore>();
			var user = Substitute.For<IPrincipal>();

			CreateTopicCommandHandler handler = new CreateTopicCommandHandler(forumDatastore, datastore, taskDatastore, user);
			GenericValidationCommandHandlerDecorator<CreateTopicCommand> val =
					new GenericValidationCommandHandlerDecorator<CreateTopicCommand>(
						handler,
						new List<IValidator<NForum.CQS.Commands.Topics.CreateTopicCommand>> { new NForum.CQS.Validators.Topics.CreateTopicValidator(TestUtils.GetInt32IdValidator()) }
					);

			val.Execute(command);

			datastore.ReceivedWithAnyArgs(1).Create(inputParameter);
		}

		[Test(Author = "Steen F. Tøttrup", Description = "Make sure that Update method gets called on the datastore, when a valid UpdateTopicCommand is provided")]
		public void UpdateTopic() {
			var parentCategory = Substitute.For<Domain.Category>("category", 1, "desc");
			parentCategory.Id.Returns("1");
			var parentForum = Substitute.For<Domain.Forum>(parentCategory, "forum", 1, "desc");
			parentForum.Id.Returns("1");

			var inputParameter = new Domain.Topic(parentForum, "subject, topic2", "bla blalba", Domain.TopicType.Regular);
			var command = new UpdateTopicCommand { Id = "1", Subject = inputParameter.Subject, Content = inputParameter.Content, State = inputParameter.State, Type = inputParameter.Type };
			var dto = Substitute.For<ITopicDto>();

			var datastore = Substitute.For<ITopicDatastore>();
			datastore.Update(inputParameter).Returns<ITopicDto>(dto);

			var userProvider = Substitute.For<IUserProvider>();
			var user = Substitute.For<IPrincipal>();
			var author = Substitute.For<IAuthenticatedUser>();
			userProvider.Get(user).Returns<IAuthor>(author);

			UpdateTopicCommandHandler handler = new UpdateTopicCommandHandler(datastore, user, userProvider);
			GenericValidationCommandHandlerDecorator<UpdateTopicCommand> val =
					new GenericValidationCommandHandlerDecorator<UpdateTopicCommand>(
						handler,
						new List<IValidator<UpdateTopicCommand>> { new UpdateTopicValidator(TestUtils.GetInt32IdValidator()) }
					);

			val.Execute(command);

			datastore.ReceivedWithAnyArgs(1).Update(inputParameter);
		}
	}
}
