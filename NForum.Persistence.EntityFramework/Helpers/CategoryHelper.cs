//using NForum.Core;
//using System;

//using DboCategory = NForum.Persistence.EntityFramework.DatabaseObjects.Category;

//namespace NForum.Persistence.EntityFramework.Helpers {

//	public static class CategoryHelper {

//		public static Category FromDbo(DboCategory category) {
//			return new Category {
//				Id = category.Id,
//				Name = category.Name,
//				SortOrder = category.SortOrder,
//				Description = category.Description,
//				CustomProperties = category.CustomProperties,
//				BoardId = category.BoardId
//			};
//		}

//	}
//}