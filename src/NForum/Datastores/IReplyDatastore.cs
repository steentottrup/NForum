using NForum.Core.Dtos;
using NForum.Domain;
using System;
using System.Collections.Generic;

namespace NForum.Datastores {

	public interface IReplyDatastore {
		IReplyDto Create(Reply reply);
		IReplyDto ReadById(String id);
		void MergeTopics(String destinationTopicId, IEnumerable<String> sourceTopicIds);
	}
}
