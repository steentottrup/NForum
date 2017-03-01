using NForum.Datastores.Dapper.Dbos.Abstractions;
using NForum.Domain;
using System;

namespace NForum.Datastores.Dapper.Dbos {

	public class Topic : ContentHolder {
		public Int32 ForumId { get; set; }
		public TopicState State { get; set; }
		public TopicType Type { get; set; }
	}
}
