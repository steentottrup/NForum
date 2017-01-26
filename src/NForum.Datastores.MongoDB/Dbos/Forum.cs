using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using System;

namespace NForum.Datastores.MongoDB.Dbos {

	public class Forum {
		public ObjectId Id { get; set; }
		[BsonElement(FieldNames.Name)]
		public String Name { get; set; }
		[BsonElement(FieldNames.SortOrder)]
		public Int32 SortOrder { get; set; }
		[BsonElement(FieldNames.Description)]
		public String Description { get; set; }
		[BsonElement(FieldNames.CategoryId)]
		public CategoryRef Category { get; set; }
		[BsonElement(FieldNames.ParentForumId)]
		public ForumRef ParentForum { get; set; }
		[BsonElement(FieldNames.Path)]
		public ObjectId[] Path { get; set; }

		public static class FieldNames {
			public const String Id = "_id";
			public const String Name = "n";
			public const String SortOrder = "s";
			public const String Description = "d";
			public const String CategoryId = "c";
			public const String ParentForumId = "f";
			public const String Path = "p";
		}
	}
}
