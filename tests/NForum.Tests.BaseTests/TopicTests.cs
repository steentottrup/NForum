using FluentAssertions;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using NForum.Core;
using NForum.Core.Abstractions.Services;
using NForum.Tests.Common;
using System;
using System.Collections.Generic;
using System.Linq;

namespace NForum.Tests.BaseTests {

	[TestClass]
	public class TopicTests {

		[ClassInitialize]
		public static void SetUp(TestContext context) {
			Initializer.Initialize();
		}

		[ClassCleanup]
		public static void TearDown() { }

		[TestMethod]
		public void CreateNewTopic() {
			ICategoryService categoryService = Initializer.CategoryService;
			IForumService forumService = Initializer.ForumService;
			ITopicService topicService = Initializer.TopicService;

			Category category = categoryService.Create("category", 1, String.Empty);
			Forum forum = forumService.Create(category.Id, "forum", 1, String.Empty);

			String subject = "The first subject";
			String text = "The text";

			Topic topic = topicService.Create(forum.Id, subject, text);

			topic.Subject.Should().Be(subject);
			topic.Text.Should().Be(text);
			topic.Type.Should().Be(TopicType.Regular);
			topic.State.Should().Be(TopicState.None);
		}

		[TestMethod]
		public void CreateNewTopicWithException() {
			ICategoryService categoryService = Initializer.CategoryService;
			IForumService forumService = Initializer.ForumService;
			ITopicService topicService = Initializer.TopicService;

			Category category = categoryService.Create("category", 1, String.Empty);
			Forum forum = forumService.Create(category.Id, "forum", 1, String.Empty);

			String subject = String.Empty;
			String text = "The text";
			Action action = () => topicService.Create(forum.Id, subject, text);

			action.ShouldThrow<ArgumentNullException>();
		}

		[TestMethod]
		public void TypeSortOrder() {
			ICategoryService categoryService = Initializer.CategoryService;
			IForumService forumService = Initializer.ForumService;
			ITopicService topicService = Initializer.TopicService;
			IUIService uiService = Initializer.UIService;

			Category category = categoryService.Create("category", 1, String.Empty);
			Forum forum = forumService.Create(category.Id, "forum", 1, String.Empty);

			String stickySubject = "I'm sticky";
			Topic sticky = topicService.Create(forum.Id, stickySubject, "bla", TopicType.Sticky);
			String announcementSubject = "I'm announcement";
			Topic announcement = topicService.Create(forum.Id, announcementSubject, "bla", TopicType.Announcement);
			String regularSubject = "I'm regular";
			Topic regular = topicService.Create(forum.Id, regularSubject, "bla");

			IEnumerable<Topic> topics = uiService.FindByForum(forum.Id, 0, 20);
			topics.Count().Should().Be(3, "3 topics were created");
			topics.First().Type.Should().Be(TopicType.Announcement, "Announcements should always be first");
			topics.Skip(1).First().Type.Should().Be(TopicType.Sticky, "Stickies should always be second");
			topics.Last().Type.Should().Be(TopicType.Regular, "Regulars should always be last");
		}

		[TestMethod]
		public void PageCountTest() {
			ICategoryService categoryService = Initializer.CategoryService;
			IForumService forumService = Initializer.ForumService;
			ITopicService topicService = Initializer.TopicService;
			IUIService uiService = Initializer.UIService;

			Category category = categoryService.Create("category", 1, String.Empty);
			Forum forum = forumService.Create(category.Id, "forum", 1, String.Empty);

			Topic announcement1 = topicService.Create(forum.Id, "announcement", "bla", TopicType.Announcement);
			uiService.GetNumberOfForumPages(forum.Id, 5).Should().Be(1);
			Topic announcement2 = topicService.Create(forum.Id, "announcement", "bla", TopicType.Announcement);
			uiService.GetNumberOfForumPages(forum.Id, 5).Should().Be(1);
			Topic announcement3 = topicService.Create(forum.Id, "announcement", "bla", TopicType.Announcement);
			uiService.GetNumberOfForumPages(forum.Id, 5).Should().Be(1);

			Topic sticky1 = topicService.Create(forum.Id, "sticky", "bla", TopicType.Sticky);
			uiService.GetNumberOfForumPages(forum.Id, 5).Should().Be(1);
			Topic sticky2 = topicService.Create(forum.Id, "sticky", "bla", TopicType.Sticky);
			uiService.GetNumberOfForumPages(forum.Id, 5).Should().Be(1);
			Topic sticky3 = topicService.Create(forum.Id, "sticky", "bla", TopicType.Sticky);
			uiService.GetNumberOfForumPages(forum.Id, 5).Should().Be(2);

			Topic regular1 = topicService.Create(forum.Id, "regular", "bla");
			uiService.GetNumberOfForumPages(forum.Id, 5).Should().Be(2);
			Topic regular2 = topicService.Create(forum.Id, "regular", "bla");
			uiService.GetNumberOfForumPages(forum.Id, 5).Should().Be(3);

		}

		[TestMethod]
		public void PagingWithAnnouncementsAndStickies() {
			ICategoryService categoryService = Initializer.CategoryService;
			IForumService forumService = Initializer.ForumService;
			ITopicService topicService = Initializer.TopicService;
			IUIService uiService = Initializer.UIService;

			Category category = categoryService.Create("category", 1, String.Empty);
			Forum forum = forumService.Create(category.Id, "forum", 1, String.Empty);

			Topic announcement1 = topicService.Create(forum.Id, "announcement", "bla", TopicType.Announcement);
			Topic announcement2 = topicService.Create(forum.Id, "announcement", "bla", TopicType.Announcement);
			Topic announcement3 = topicService.Create(forum.Id, "announcement", "bla", TopicType.Announcement);

			Topic sticky1 = topicService.Create(forum.Id, "sticky", "bla", TopicType.Sticky);
			Topic sticky2 = topicService.Create(forum.Id, "sticky", "bla", TopicType.Sticky);
			Topic sticky3 = topicService.Create(forum.Id, "sticky", "bla", TopicType.Sticky);

			Topic regular1 = topicService.Create(forum.Id, "regular", "bla");
			Topic regular2 = topicService.Create(forum.Id, "regular", "bla");

			IEnumerable<Topic> topics = uiService.FindByForum(forum.Id, 0, 5, true);
			topics.Count().Should().Be(6, "we should get the request +1 because more topics than will fit on one page");
			topics.First().Type.Should().Be(TopicType.Announcement);
			topics.Skip(1).First().Type.Should().Be(TopicType.Announcement);
			topics.Skip(2).First().Type.Should().Be(TopicType.Announcement);

			topics.Skip(3).First().Type.Should().Be(TopicType.Sticky);
			topics.Skip(4).First().Type.Should().Be(TopicType.Sticky);
			// We're not testing the last one, it should never be shown, it's just a "marker" to indicate that more exists (next page)

			topics = uiService.FindByForum(forum.Id, 1, 5, true);
			topics.Count().Should().Be(6, "we should get the 3 announcements and 1 sticky and 1 regular from the 2nd page (we have 8 in all, and 5 per page) and plus 1 to indicate \"more\" pages");
			topics.First().Type.Should().Be(TopicType.Announcement);
			topics.Skip(1).First().Type.Should().Be(TopicType.Announcement);
			topics.Skip(2).First().Type.Should().Be(TopicType.Announcement);
			topics.Skip(3).First().Type.Should().Be(TopicType.Sticky);
			topics.Skip(4).First().Type.Should().Be(TopicType.Regular);

			topics = uiService.FindByForum(forum.Id, 2, 5, true);
			topics.Count().Should().Be(4, "we should get the 3 announcements and 1 regular from the 3nd page (we have 8 in all, and 5 per page)");
			topics.First().Type.Should().Be(TopicType.Announcement);
			topics.Skip(1).First().Type.Should().Be(TopicType.Announcement);
			topics.Skip(2).First().Type.Should().Be(TopicType.Announcement);
			topics.Skip(3).First().Type.Should().Be(TopicType.Regular);
		}
	}
}
