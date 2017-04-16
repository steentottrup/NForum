using System;
using System.Collections.Generic;
using NForum.Core.Dtos;
using MongoDB.Bson;
using NForum.Datastores.MongoDB.Dbos;
using System.Linq;
using NForum.Domain;

namespace NForum.Datastores.MongoDB {

	public class ForumDatastore : IForumDatastore {
		protected readonly CommonDatastore datastore;

		public ForumDatastore(CommonDatastore datastore) {
			this.datastore = datastore;
		}

		public IForumDto Create(Domain.Forum forum) {
			Dbos.Category cat = this.datastore.ReadCategoryById(ObjectId.Parse(forum.CategoryId));
			if (cat == null) {
				// TODO ??
				throw new ArgumentException("Parent category not found");
			}

			Dbos.Forum f = new Dbos.Forum {
				Name = forum.Name,
				Description = forum.Description,
				SortOrder = forum.SortOrder,
				Category = new CategoryRef {
					Id = cat.Id,
					Name = cat.Name
				},
				Path = new ObjectId[] { }
			};

			if (!String.IsNullOrWhiteSpace(forum.ParentForumId)) {
				Dbos.Forum parentForum = this.datastore.ReadForumById(ObjectId.Parse(forum.ParentForumId));
				if (parentForum == null) {
					// TODO ??
					throw new ArgumentException("Parent forum not found");
				}

				f.ParentForum = new ForumRef {
					Id = parentForum.Id,
					Name = parentForum.Name
				};
				f.Path = parentForum.Path.Union(new ObjectId[] { parentForum.Id }).ToArray();
			}

			return this.datastore.CreateForum(f).ToDto();
		}

		public IForumDto CreateAsForumChild(Domain.Forum forum) {
			throw new NotImplementedException();
		}

		public void DeleteById(String id) {
			throw new NotImplementedException();
		}

		public void DeleteWithSubElementsById(String id) {
			throw new NotImplementedException();
		}

		public IForumDto MoveToCategory(String forumId, String categoryId) {
			throw new NotImplementedException();
		}

		public IForumDto MoveToForum(String forumId, String parentForumId) {
			throw new NotImplementedException();
		}

		public IEnumerable<IForumDto> ReadByCategoryId(String categoryId) {
			throw new NotImplementedException();
		}

		public IForumDto ReadById(String id) {
			ObjectId forumId;
			if (!ObjectId.TryParse(id, out forumId)) {
				throw new ArgumentException(nameof(id));
			}
			return this.datastore.ReadForumById(forumId).ToDto();
		}

		public IEnumerable<IForumDto> ReadByPath(IEnumerable<String> ids) {
			throw new NotImplementedException();
		}

		public IForumDto Update(Domain.Forum forum) {
			throw new NotImplementedException();
		}
	}
}
