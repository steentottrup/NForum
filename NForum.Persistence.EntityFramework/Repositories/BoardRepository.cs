//using NForum.Core;
//using NForum.Core.Abstractions.Data;
//using System;
//using System.Collections.Generic;
//using System.Data.Entity;
//using System.Linq;
//using System.Linq.Expressions;

//namespace NForum.Persistence.EntityFramework.Repositories {

//	public class BoardRepository : RepositoryBase<Board>, IBoardRepository {
//		private readonly UnitOfWork uow;

//		public BoardRepository(UnitOfWork uow)
//			: base(uow) {
//			this.uow = uow;
//		}

//		public Board ByName(String name) {
//			return this.uow.Set<Board>().FirstOrDefault(b => b.Name == name);
//		}

//		public Board ByForum(Forum forum) {
//			return this.uow.Set<Board>().Include(b => b.Categories).FirstOrDefault(b => b.Categories.Any(c => c.Id == forum.CategoryId) == true);
//		}

//		public Board ByCategory(Category category) {
//			return this.uow.Set<Board>().FirstOrDefault(b => b.Id == category.BoardId);
//		}
//	}
//}