using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using System;

namespace NForum.Datastores.MongoDB.Dbos {

	public class ForumRef {
		[BsonElement(FieldNames.Id)]
		public ObjectId Id { get; set; }
		[BsonElement(FieldNames.Name)]
		public String Name { get; set; }

		public static class FieldNames {
			public const String Id = "i";
			public const String Name = "n";
		}
	}
}
