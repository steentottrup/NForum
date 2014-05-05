//using NForum.Core;
//using System;

//using DboForum = NForum.Persistence.EntityFramework.DatabaseObjects.Forum;

//namespace NForum.Persistence.EntityFramework.Helpers {

//	public static class ForumHelper {

//		public static DboForum ToDbo(Forum forum) {
//			return new DboForum {
//				Id = forum.Id,
//				Name = forum.Name,
//				Description = forum.Description,
//				SortOrder = forum.SortOrder,
//				CustomProperties = forum.CustomProperties,
//				CategoryId = forum.CategoryId,
//				ParentForumId = forum.ParentForumId,
//				LatestPostId = forum.LatestPostId,
//				LatestTopicId = forum.LatestTopicId
//			};
//		}

//		public static Forum FromDbo(DboForum forum) {
//			return new Forum {
//				Id = forum.Id,
//				Name = forum.Name,
//				SortOrder = forum.SortOrder,
//				Description = forum.Description,
//				CustomProperties = forum.CustomProperties,
//				CategoryId = forum.CategoryId,
//				LatestPostId = forum.LatestPostId,
//				LatestTopicId = forum.LatestTopicId,
//				ParentForumId = forum.ParentForumId
//			};
//		}
//	}
//}