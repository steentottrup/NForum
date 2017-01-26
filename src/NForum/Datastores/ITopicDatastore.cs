using NForum.Core.Dtos;
using NForum.Domain;
using System;

namespace NForum.Datastores {

	public interface ITopicDatastore {
		ITopicDto Create(Topic topic);
		ITopicDto Update(Topic topic);
		void DeleteById(String id);
	}
}
