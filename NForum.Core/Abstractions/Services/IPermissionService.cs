using System;

namespace NForum.Core.Abstractions.Services {

	public interface IPermissionService {
		Boolean HasAccess(User user, Forum forum);
		Boolean HasAccess(User user, Forum forum, CRUD access);
		Boolean HasAccess(User user, Forum forum, Int64 accessMask);
		Boolean HasAccess(User user, Board board);
		Boolean HasAccess(User user, Board board, CRUD access);
		Boolean HasAccess(User user, Category category);
		Boolean HasAccess(User user, Category category, CRUD access);
		Boolean CanCreateBoard(User user);
	}
}