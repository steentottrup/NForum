//using NForum.Core.Abstractions;
//using NForum.Core.Abstractions.Data;
//using NForum.Core.Abstractions.Events;
//using NForum.Core.Abstractions.Providers;
//using NForum.Core.Abstractions.Services;
//using NForum.Core.Events.Payloads;
//using System;
//using System.Collections.Generic;
//using System.Linq;

//namespace NForum.Core.Services {

//	/// <summary>
//	/// The baord service takes care of any changes to the <see cref="NForum.Board">Board</see> entities.
//	/// </summary>
//	public class BoardService : IBoardService {
//		protected readonly IBoardRepository boardRepo;
//		protected readonly IUserProvider userProvider;
//		protected readonly IEventPublisher eventPublisher;
//		protected readonly ILogger logger;
//		protected readonly IPermissionService permissionService;

//		public BoardService(IUserProvider userProvider,
//							IBoardRepository boardRepo,
//							IEventPublisher eventPublisher,
//							ILogger logger,
//							IPermissionService permissionService) {

//			this.boardRepo = boardRepo;
//			this.userProvider = userProvider;
//			this.eventPublisher = eventPublisher;
//			this.logger = logger;
//			this.permissionService = permissionService;
//		}

//		private Boolean CanCreateBoard(User user) {
//			if (user == null) {
//				return false;
//			}
//			return this.permissionService.CanCreateBoard(user);
//		}

//		/// <summary>
//		/// Method for creating a new board.
//		/// </summary>
//		/// <param name="name">The name of the board.</param>
//		/// <param name="description">The description of the board.</param>
//		/// <param name="sortOrder">The sort order/placement of the board.</param>
//		/// <returns></returns>
//		public Board Create(String name, String description, Int32 sortOrder) {
//			this.logger.WriteFormat("Create called on BoardService, Name: {0}, Description: {1}, Sort Order: {2}", name, description, sortOrder);
//			if (!this.CanCreateBoard(this.userProvider.CurrentUser)) {
//				this.logger.WriteFormat("User does not have permissions to create a new board, name: {0}", name);
//				throw new PermissionException("board, create");
//			}

//			Board b = new Board {
//				Name = name,
//				SortOrder = sortOrder,
//				Description = description
//			};
//			// TODO: Custom properties?

//			this.boardRepo.Create(b);
//			this.logger.WriteFormat("Board created in BoardService, Id: {0}", b.Id);
//			this.eventPublisher.Publish<BoardCreated>(new BoardCreated {
//				Board = b
//			});
//			this.logger.WriteFormat("Create events in BoardService fired, Id: {0}", b.Id);

//			return b;
//		}

//		/// <summary>
//		/// Method for reading a board by its id.
//		/// </summary>
//		/// <param name="id">Id of the board to read.</param>
//		/// <returns></returns>
//		public Board Read(Int32 id) {
//			this.logger.WriteFormat("Read called on BoardService, Id: {0}", id);
//			Board board = this.boardRepo.Read(id);
//			if (!this.permissionService.HasAccess(this.userProvider.CurrentUser, board, CRUD.Read)) {
//				this.logger.WriteFormat("User does not have permissions to read a board, name: {0}", board.Name);
//				throw new PermissionException("board, read");
//			}
//			return board;
//		}

//		/// <summary>
//		/// Method for reading a board by its name.
//		/// </summary>
//		/// <param name="name">The name of the board to read.</param>
//		/// <returns></returns>
//		public Board Read(String name) {
//			this.logger.WriteFormat("Read called on BoardService, name: {0}", name);
//			Board board = this.boardRepo.ByName(name);
//			if (!this.permissionService.HasAccess(this.userProvider.CurrentUser, board, CRUD.Read)) {
//				this.logger.WriteFormat("User does not have permissions to read a board, name: {0}", board.Name);
//				throw new PermissionException("board, read");
//			}
//			return board;
//		}

//		/// <summary>
//		/// Method used to get all boards.
//		/// </summary>
//		/// <returns>A list of all boards.</returns>
//		public IEnumerable<Board> Read() {
//			this.logger.Write("Read called on BoardService");
//			// TODO: Permissions!!
//			return this.boardRepo.ReadAll().OrderBy(b => b.SortOrder);
//		}

//		/// <summary>
//		/// Method for updating a board.
//		/// </summary>
//		/// <param name="board">The changed board.</param>
//		/// <returns>The updated board.</returns>
//		public Board Update(Board board) {
//			if (board == null) {
//				throw new ArgumentNullException("board");
//			}
//			this.logger.WriteFormat("Update called on BoardService, Id: {0}", board.Id);
//			// Let's get the board from the data-storage!
//			Board oldBoard = this.Read(board.Id);
//			if (!this.permissionService.HasAccess(this.userProvider.CurrentUser, board, CRUD.Update)) {
//				this.logger.WriteFormat("User does not have permissions to update a board, name: {0}", board.Name);
//				throw new PermissionException("board, update");
//			}
//			Board originalBoard = oldBoard.Clone() as Board;

//			if (oldBoard != null) {
//				Boolean changed = false;
//				if (board.Name != oldBoard.Name) {
//					oldBoard.Name = board.Name;
//					changed = true;
//				}
//				if (board.SortOrder != oldBoard.SortOrder) {
//					oldBoard.SortOrder = board.SortOrder;
//					changed = true;
//				}
//				if (board.Description != oldBoard.Description) {
//					oldBoard.Description = board.Description;
//					changed = true;
//				}
//				if (board.PostsPerPage != oldBoard.PostsPerPage) {
//					oldBoard.PostsPerPage = board.PostsPerPage;
//					changed = true;
//				}
//				if (board.TopicsPerPage != oldBoard.TopicsPerPage) {
//					oldBoard.TopicsPerPage = board.TopicsPerPage;
//					changed = true;
//				}
//				if (board.CustomProperties != oldBoard.CustomProperties) {
//					oldBoard.CustomProperties = board.CustomProperties;
//					changed = true;
//				}

//				if (changed) {
//					oldBoard = this.boardRepo.Update(oldBoard);
//					this.logger.WriteFormat("Board updated in BoardService, Id: {0}", board.Id);
//					this.eventPublisher.Publish<BoardUpdated>(new BoardUpdated {
//						Board = originalBoard,
//						UpdatedBoard = oldBoard
//					});
//					this.logger.WriteFormat("Update events in BoardService fired, Id: {0}", board.Id);
//				}
//				return oldBoard;
//			}
//			this.logger.WriteFormat("Update board failed, no board with the given id was found, Id: {0}", board.Id);
//			// TODO:
//			throw new ApplicationException();
//		}

//		/// <summary>
//		/// Method for deleting a board.
//		/// </summary>
//		/// <param name="board">The board to delete.</param>
//		public void Delete(Board board) {
//			if (board == null) {
//				throw new ArgumentNullException("board");
//			}
//			this.logger.WriteFormat("Delete called on BoardService, Id: {0}", board.Id);
//			if (!this.permissionService.HasAccess(this.userProvider.CurrentUser, board, CRUD.Delete)) {
//				this.logger.WriteFormat("User does not have permissions to delete a board, name: {0}", board.Name);
//				throw new PermissionException("board, delete");
//			}
//			// TODO: Delete categories, forums, topics, posts, access masks etc etc.
//			this.boardRepo.Delete(board);
//			this.eventPublisher.Publish<BoardDeleted>(new BoardDeleted {
//				Board = board
//			});
//			this.logger.WriteFormat("Delete events in BoardService fired, Id: {0}", board.Id);
//		}
//	}
//}