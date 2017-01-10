using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using System;

namespace NForum.Datastores.MongoDB.Dbos {

	public class Category {
		public ObjectId Id { get; set; }
		[BsonElement(FieldNames.Name)]
		public String Name { get; set; }
		[BsonElement(FieldNames.SortOrder)]
		public Int32 SortOrder { get; set; }
		[BsonElement(FieldNames.Description)]
		public String Description { get; set; }

		public static class FieldNames {
			public const String Id = "_id";
			public const String Name = "n";
			public const String SortOrder = "s";
			public const String Description = "d";
		}
	}
}
