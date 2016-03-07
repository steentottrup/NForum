using System;
using System.Collections.Generic;

namespace NForum.Core.Abstractions.Services {

	public interface IReplyService {
		Reply Create(String topicId, String subject, String text);
		Reply FindById(String replyId);
		IEnumerable<Reply> FindAll();
		Reply Update(String replyId, String subject, String text);
		Boolean Delete(String replyId);
	}
}
