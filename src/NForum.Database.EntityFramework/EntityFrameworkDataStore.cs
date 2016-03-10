using NForum.Core;
using NForum.Core.Abstractions.Data;
using NForum.Database.EntityFramework.Repositories;
using System;
using System.Linq;
using System.Collections.Generic;
using System.Data.Entity;

namespace NForum.Database.EntityFramework {

	public class EntityFrameworkDataStore : IDataStore {
		protected readonly IRepository<Dbos.Category> categoryRepository;
		protected readonly IRepository<Dbos.Forum> forumRepository;
		protected readonly IRepository<Dbos.Topic> topicRepository;
		protected readonly IRepository<Dbos.Reply> replyRepository;
		protected readonly IRepository<Dbos.ForumUser> forumUserRepository;

		public EntityFrameworkDataStore(IRepository<Dbos.Category> categoryRepository,
										IRepository<Dbos.Forum> forumRepository,
										IRepository<Dbos.Topic> topicRepository,
										IRepository<Dbos.Reply> replyRepository,
										IRepository<Dbos.ForumUser> forumUserRepository) {
			this.categoryRepository = categoryRepository;
			this.forumRepository = forumRepository;
			this.topicRepository = topicRepository;
			this.replyRepository = replyRepository;
			this.forumUserRepository = forumUserRepository;
		}

		public Category CreateCategory(String name, Int32 sortOrder, String description) {
			if (String.IsNullOrWhiteSpace(name)) {
				throw new ArgumentNullException(nameof(name));
			}
			Dbos.Category model = this.categoryRepository.Create(new Dbos.Category {
				Name = name,
				SortOrder = sortOrder,
				Description = description
			});

			return model.ToModel();
		}

		public Forum CreateForum(String categoryId, String forumId, String name, Int32 sortOrder, String description) {
			Guid id;
			if (!Guid.TryParse(categoryId, out id)) {
				throw new ArgumentException(nameof(categoryId));
			}

			if (String.IsNullOrWhiteSpace(name)) {
				throw new ArgumentNullException(nameof(name));
			}

			Dbos.Forum parentForum = null;
			if (!String.IsNullOrWhiteSpace(forumId)) {
				Guid parentForumId;
				if (!Guid.TryParse(forumId, out parentForumId)) {
					throw new ArgumentException(nameof(forumId));
				}

				parentForum = this.forumRepository.FindById(parentForumId);
			}

			Dbos.Category parent = this.categoryRepository.FindById(id);
			if (parent == null) {
				throw new ArgumentException(nameof(categoryId));
			}

			return this.CreateForum(parent, parentForum, name, sortOrder, description);
		}

		private Forum CreateForum(Dbos.Category category, Dbos.Forum parentForum, String name, Int32 sortOrder, String description) {
			Dbos.Forum newForum = new Dbos.Forum {
				CategoryId = category.Id,
				Name = name,
				SortOrder = sortOrder,
				Description = description,
				Level = 0
			};
			if (parentForum != null) {
				newForum.ParentForumId = parentForum.Id;
				newForum.Level = parentForum.Level + 1;
			}

			Dbos.Forum model = this.forumRepository.Create(newForum);
			return model.ToModel();
		}

		public Boolean DeleteCategory(String categoryId) {
			// TODO: All sub-forums? All topics, replies, attachments etc??
			Guid id;
			if (!Guid.TryParse(categoryId, out id)) {
				throw new ArgumentException(nameof(categoryId));
			}
			this.categoryRepository.DeleteById(id);
			// TODO: ?!?!
			return true;
		}

		public bool DeleteForum(String forumId) {
			// TODO: All sub-forums? All topics, replies, attachments etc??
			Guid id;
			if (!Guid.TryParse(forumId, out id)) {
				throw new ArgumentException(nameof(forumId));
			}
			this.forumRepository.DeleteById(id);
			// TODO: ?!?!
			return true;
		}

		public Category FindCategoryById(String categoryId) {
			Guid id;
			if (!Guid.TryParse(categoryId, out id)) {
				throw new ArgumentException(nameof(categoryId));
			}
			Dbos.Category category = this.categoryRepository.FindById(id);
			return category.ToModel();
		}

		public Forum FindForumById(String forumId) {
			Guid id;
			if (!Guid.TryParse(forumId, out id)) {
				throw new ArgumentException(nameof(forumId));
			}
			Dbos.Forum forum = this.forumRepository.FindById(id);
			return forum.ToModel();
		}

		public Category UpdateCategory(String categoryId, String name, Int32 sortOrder, String description) {
			Guid id;
			if (!Guid.TryParse(categoryId, out id)) {
				throw new ArgumentException(nameof(categoryId));
			}
			Dbos.Category category = this.categoryRepository.FindById(id);

			category.Name = name;
			category.SortOrder = sortOrder;
			category.Description = description;

			category = this.categoryRepository.Update(category);
			return category.ToModel();
		}

		public Forum UpdateForum(String forumId, String name, Int32 sortOrder, String description) {
			Guid id;
			if (!Guid.TryParse(forumId, out id)) {
				throw new ArgumentException(nameof(forumId));
			}
			if (String.IsNullOrWhiteSpace(name)) {
				throw new ArgumentNullException(nameof(name));
			}
			Dbos.Forum forum = this.forumRepository.FindById(id);

			forum.Name = name;
			forum.SortOrder = sortOrder;
			forum.Description = description;

			forum = this.forumRepository.Update(forum);
			return forum.ToModel();
		}

		public IEnumerable<Category> FindCategoriesPlus2Levels() {
			IEnumerable<Dbos.Category> categories = this.categoryRepository.FindAll()
				.Include(c => c.Forums)
				.ToList();
			// TODO: Limit on level!!

			List<Category> output = new List<Category>();
			categories.ToList().ForEach(c => {
				Category cat = c.ToModel();
				PopulatedLevel0(c, cat);
				output.Add(cat);
			});

			return output;
		}

		private static void PopulatedLevel0(Dbos.Category c, Category cat) {
			List<Forum> forums = new List<Forum>();

			c.Forums.Where(f => f.Level == 0).ToList().ForEach(f => {
				Forum forum = f.ToModel();
				forum.Category = cat;

				PopulateLevel1(cat, f, forum);

				forums.Add(forum);
			});

			cat.Forums = forums;
		}

		private static void PopulateLevel1(Category cat, Dbos.Forum f, Forum forum) {
			List<Forum> subForums = new List<Forum>();

			f.SubForums.Where(fc => fc.Level == 1 && fc.ParentForumId == f.Id).ToList().ForEach(fc => {
				Forum child = fc.ToModel();
				child.Category = cat;
				child.ParentForum = forum;
				subForums.Add(child);
			});

			forum.SubForums = subForums;
		}

		public Forum FindForumPlus2Levels(String forumId) {
			Guid id;
			if (!Guid.TryParse(forumId, out id)) {
				throw new ArgumentException(nameof(forumId));
			}
			IEnumerable<Dbos.Forum> forums = this.forumRepository.FindAll()
				.Include(f => f.Category)
				.Where(f => f.CategoryId == f.CategoryId)
				.ToList();

			Dbos.Forum forum = forums.SingleOrDefault(f => f.Id == id);
			if (forum == null) {
				// TODO:
				throw new ApplicationException();
			}

			Category cat = forum.Category.ToModel();
			Forum output = forum.ToModel();
			output.Category = cat;
			Forum dest = output;
			Dbos.Forum parentForum = forum.ParentForum;
			while (parentForum != null) {
				dest.ParentForum = parentForum.ToModel();
				dest.ParentForum.Category = cat;

				dest = dest.ParentForum;
				parentForum = parentForum.ParentForum;
			}

			output.Category = forum.Category.ToModel();
			List<Forum> subForums = new List<Forum>();
			forum.SubForums.ToList().ForEach(f => {
				Forum subForum = f.ToModel();
				subForum.ParentForum = output;
				subForum.Category = cat;

				List<Forum> children = new List<Forum>();
				f.SubForums.ToList().ForEach(fc => {
					Forum child = fc.ToModel();
					child.ParentForum = subForum;
					child.Category = cat;

					children.Add(child);
				});

				subForum.SubForums = children;
				subForums.Add(subForum);
			});
			output.SubForums = subForums;

			return output;
		}

		public Category FindCategoryPlus2Levels(String categoryId) {
			Guid id;
			if (!Guid.TryParse(categoryId, out id)) {
				throw new ArgumentException(nameof(categoryId));
			}

			Dbos.Category category = this.categoryRepository.FindAll()
				.Include(c => c.Forums)
				.SingleOrDefault(c => c.Id == id);

			Category output = category.ToModel();

			PopulatedLevel0(category, output);

			return output;
		}

		public IEnumerable<Category> FindAllCategories() {
			return this.categoryRepository.FindAll()
				.ToList()
				.Select(c => c.ToModel())
				.OrderBy(c => c.SortOrder);
		}

		public IEnumerable<Topic> FindByForum(String forumId, Int32 pageIndex, Int32 pageSize, Boolean includeExtra) {
			Guid id;
			if (!Guid.TryParse(forumId, out id)) {
				throw new ArgumentException(nameof(forumId));
			}

			IEnumerable<Dbos.Topic> anns = this.topicRepository.FindAll()
				.Include(t => t.Message)
				.Include(t => t.Replies)
				.Where(t => t.ForumId == id)
				// Not really likely that any of these exists, but...
				.Where(t => t.State == TopicState.Locked || t.State == TopicState.Moved || t.State == TopicState.None)
				.Where(t => t.Type == TopicType.Announcement)
				.OrderByDescending(t => t.LatestReplyId.HasValue == true ? t.LatestReply.Message.Created : t.Message.Created)
				.ToList();

			IEnumerable<Topic> announcements = anns
				.Select(t => t.ToModel());
			IEnumerable<Topic> stickies = new List<Topic>();
			IEnumerable<Topic> regulars = new List<Topic>();

			Int32 announcementCount = announcements.Count();
			// The actual amount of topics per page that we need to fetch, the rest is "hard-coded" as announcements.
			Int32 maxOtherTypesPerPage = pageSize - announcementCount;

			// Do we have room for anything but the announcements?
			if (maxOtherTypesPerPage > 0) {
				// Yeah, let's fetch stickies first, then regular topics!
				IEnumerable<Dbos.Topic> tempStickies = this.topicRepository.FindAll()
					.Include(t => t.Message)
					.Include(t => t.Replies)
					.Where(t => t.ForumId == id)
					.Where(t => t.State == TopicState.Locked || t.State == TopicState.Moved || t.State == TopicState.None)
					.Where(t => t.Type == TopicType.Sticky);

				Int32 totalStickies = tempStickies.Count();

				stickies = tempStickies
					.OrderByDescending(t => t.LatestReplyId.HasValue == true ? t.LatestReply.Message.Created : t.Message.Created)
					.Skip(maxOtherTypesPerPage * pageIndex)
					.Take(maxOtherTypesPerPage + (includeExtra ? 1 : 0))
					.ToList()
					.Select(t => t.ToModel());

				// Do we have room for anything but the announcements and stickies?
				if (stickies.Count() < maxOtherTypesPerPage) {
					// Yeah, let's fetch some regular topics then!
					Int32 skip = 0;
					// Are we getting any stickies, or are we past those?
					if (stickies.Count() == 0) {
						// We're past, so only regulars! Let's figure out how many we need to skip!
						skip = (pageIndex * maxOtherTypesPerPage) - totalStickies;
					}
					// And how many are we taking?
					Int32 take = maxOtherTypesPerPage - stickies.Count();
					// Do it!
					regulars = this.topicRepository.FindAll()
						.Include(t => t.LatestReply)
						.Include(t => t.LatestReply.Message)
						.Include(t => t.Message)
						// TODO: Really? That's potentially A LOT of data!
						.Include(t => t.Replies)
						.Where(t => t.ForumId == id)
						.Where(t => t.State == TopicState.Locked || t.State == TopicState.Moved || t.State == TopicState.None)
						.Where(t => t.Type == TopicType.Regular)
						.OrderByDescending(t => t.LatestReplyId.HasValue == true ? t.LatestReply.Message.Created : t.Message.Created)
						.Skip(skip)
						.Take(take + (includeExtra ? 1 : 0))
						.ToList()
						.Select(t => t.ToModel());
				}
			}

			return announcements.Union(
					stickies.Union(
							regulars
						)
				);
		}

		public Topic CreateTopic(String userId, String forumId, String subject, String text, TopicType type) {
			Guid id;
			if (!Guid.TryParse(forumId, out id)) {
				throw new ArgumentException(nameof(forumId));
			}
			Guid uId;
			if (!Guid.TryParse(userId, out uId)) {
				throw new ArgumentException(nameof(userId));
			}

			if (String.IsNullOrWhiteSpace(subject)) {
				throw new ArgumentNullException(nameof(subject));
			}

			Dbos.Forum parent = this.forumRepository.FindById(id);
			if (parent == null) {
				throw new ArgumentException(nameof(forumId));
			}

			Dbos.Topic newTopic = new Dbos.Topic {
				ForumId = parent.Id,
				State = TopicState.None,
				Type = type
			};
			newTopic.Message = new Dbos.Message {
				Created = DateTime.UtcNow,
				State = Dbos.MessageState.None,
				Subject = subject,
				Text = text,
				Updated = DateTime.UtcNow,
				AuthorId = uId,
				EditorId = uId,
			};

			newTopic = this.topicRepository.Create(newTopic);

			return newTopic.ToModel();
		}

		public IEnumerable<Forum> FindAllForums() {
			return this.forumRepository.FindAll()
				.ToList()
				.Select(c => c.ToModel())
				.OrderBy(c => c.SortOrder);
		}

		public Int32 GetNumberOfForumPages(String forumId, Int32 pageSize) {
			Guid id;
			if (!Guid.TryParse(forumId, out id)) {
				throw new ArgumentException(nameof(forumId));
			}

			Int32 announcementCount = this.topicRepository.FindAll()
				.Where(t => t.ForumId == id)
				// Not really likely that any of these exists, but...
				.Where(t => t.State == TopicState.Locked || t.State == TopicState.Moved || t.State == TopicState.None)
				.Where(t => t.Type == TopicType.Announcement)
				.Count();

			Int32 otherCount = this.topicRepository.FindAll()
				.Where(t => t.ForumId == id)
				// Not really likely that any of these exists, but...
				.Where(t => t.State == TopicState.Locked || t.State == TopicState.Moved || t.State == TopicState.None)
				.Where(t => t.Type != TopicType.Announcement)
				.Count();

			// The actual amount of topics per page, the rest is "hard-coded" as announcements.
			Int32 maxOtherTypesPerPage = pageSize - announcementCount;

			Int32 pages = 1;
			if (maxOtherTypesPerPage > 0 && otherCount > 0) {
				pages = otherCount / maxOtherTypesPerPage;
				if (otherCount % maxOtherTypesPerPage != 0) {
					pages++;
				}
			}

			return pages;
		}

		public Int32 GetNumberOfTopicPages(String topicId, Int32 pageSize, Boolean includeDeleted) {
			Guid id;
			if (!Guid.TryParse(topicId, out id)) {
				throw new ArgumentException(nameof(topicId));
			}
			var query = this.replyRepository.FindAll()
				.Where(r => r.TopicId == id);
			if (includeDeleted) {
				query = query.Where(r => r.State == ReplyState.Deleted || r.State == ReplyState.None);
			}
			else {
				query = query.Where(r => r.State == ReplyState.None);
			}

			// Plus one for the topic!!
			Int32 count = query.Count() + 1;

			Int32 pages = count / pageSize;
			if (count % pageSize != 0) {
				pages++;
			}

			return pages;
		}

		public IEnumerable<Reply> FindByTopic(String topicId, Int32 pageIndex, Int32 pageSize, Boolean includeDeleted) {
			Guid id;
			if (!Guid.TryParse(topicId, out id)) {
				throw new ArgumentException(nameof(topicId));
			}

			Dbos.Topic topic = this.topicRepository.FindById(id);

			Int32 skip = 0;
			Int32 take = pageSize;
			// Not on the first page?
			if (pageIndex == 0) {
				skip = pageIndex * pageSize;
			}

			IEnumerable<Reply> replies = this.replyRepository.FindAll()
				.Include(r => r.Message)
				.Where(r => r.TopicId == id)
				.Where(r => (r.State == ReplyState.None || (r.State == ReplyState.Deleted && includeDeleted)))
				.OrderBy(r => r.Message.Created)
				.Skip(skip)
				.Take(take)
				.ToList()
				.Select(r => r.ToModel());

			return replies;
		}

		public Topic UpdateTopic(String userId, String topicId, String subject, String text, TopicType type, TopicState state) {
			Guid id;
			if (!Guid.TryParse(topicId, out id)) {
				throw new ArgumentException(nameof(topicId));
			}
			Guid uId;
			if (!Guid.TryParse(userId, out uId)) {
				throw new ArgumentException(nameof(userId));
			}
			if (String.IsNullOrWhiteSpace(subject)) {
				throw new ArgumentNullException(nameof(subject));
			}
			Dbos.Topic topic = this.topicRepository.FindById(id);

			topic.Message.Subject = subject;
			topic.Message.Text = text;
			topic.Type = type;
			topic.Message.Updated = DateTime.UtcNow;
			topic.Message.EditorId = uId;
			topic.State = state;

			topic = this.topicRepository.Update(topic);
			return topic.ToModel();
		}

		public Topic FindTopicById(String topicId) {
			Guid id;
			if (!Guid.TryParse(topicId, out id)) {
				throw new ArgumentException(nameof(topicId));
			}
			Dbos.Topic topic = this.topicRepository.FindById(id);
			return topic.ToModel();
		}

		public Boolean DeleteTopic(String topicId) {
			// TODO: All replies? All attachments etc??
			Guid id;
			if (!Guid.TryParse(topicId, out id)) {
				throw new ArgumentException(nameof(topicId));
			}
			this.topicRepository.DeleteById(id);
			// TODO: ?!?!
			return true;
		}

		public ForumUser CreateForumUser(String userName, String emailAddress, String fullname, String userId, String culture, String timezone) {
			return this.forumUserRepository.Create(new Dbos.ForumUser {
				Username = userName,
				EmailAddress = emailAddress,
				Fullname = fullname,
				Culture = culture,
				Timezone = timezone,
				ExternalId = userId,
				Deleted = false,
				UseFullname = false
			}).ToModel();
		}

		public ForumUser FindForumUserById(String forumUserId) {
			Guid id;
			if (!Guid.TryParse(forumUserId, out id)) {
				throw new ArgumentException(nameof(forumUserId));
			}
			return this.forumUserRepository.FindById(id).ToModel();
		}

		public IEnumerable<ForumUser> FindAllForumUsers() {
			return this.forumUserRepository.FindAll().ToList().Select(u => u.ToModel());
		}
	}
}

