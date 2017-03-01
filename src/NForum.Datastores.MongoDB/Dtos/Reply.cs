using NForum.Core.Dtos;
using System;
using NForum.Core.Refs;
using System.Collections.Generic;

namespace NForum.Datastores.MongoDB.Dtos {

	public class Reply : IReplyDto {
		public String Content { get; set; }

		public DateTime Created { get; set; }

		public IAuthorRef CreatedBy { get; set; }

		public IDictionary<String, Object> CustomProperties { get; set; }

		public String Id { get; set; }

		public DateTime LastEdited { get; set; }

		public IAuthorRef LastEditedBy { get; set; }

		public IReplyRef ReplyTo { get; set; }

		public String Subject { get; set; }

		public ITopicRef Topic { get; set; }
	}
}
