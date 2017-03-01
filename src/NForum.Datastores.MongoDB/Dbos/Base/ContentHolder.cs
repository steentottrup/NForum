using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using System;

namespace NForum.Datastores.MongoDB.Dbos.Base {

	public abstract class ContentHolder : CustomPropertiesHolder {
		[BsonElement(FieldNames.Subject)]
		public String Subject { get; set; }
		[BsonElement(FieldNames.Content)]
		public String Content { get; set; }
		[BsonElement(FieldNames.Created)]
		public DateTime Created { get; set; }
		[BsonElement(FieldNames.LastEdited)]
		public DateTime LastEdited { get; set; }
		[BsonElement(FieldNames.CreatedBy)]
		public Object CreatedBy { get; set; }
		[BsonElement(FieldNames.LastEditedBy)]
		public Object LastEditedBy { get; set; }

		public static class FieldNames {
			public const String Subject = "s";
			public const String Content = "c";
			public const String Created = "cr";
			public const String LastEdited = "le";
			public const String LastEditedBy = "eb";
			public const String CreatedBy = "cb";
		}
	}
}
