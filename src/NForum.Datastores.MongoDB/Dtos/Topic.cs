using NForum.Core.Dtos;
using System;
using NForum.Core.Refs;
using System.Collections.Generic;
using NForum.Domain;

namespace NForum.Datastores.MongoDB.Dtos {

	public class Topic : ITopicDto {

		public String Content { get; set; }

		public DateTime Created { get; set; }

		public IAuthorRef CreatedBy { get; set; }

		public IDictionary<String, Object> CustomProperties { get; set; }

		public IForumRef Forum { get; set; }

		public String Id { get; set; }

		public DateTime LastEdited { get; set; }

		public IAuthorRef LastEditedBy { get; set; }

		public TopicState State { get; set; }

		public String Subject { get; set; }

		public TopicType Type { get; set; }
	}
}
