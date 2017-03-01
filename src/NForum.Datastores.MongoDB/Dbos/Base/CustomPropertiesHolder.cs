using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using System;

namespace NForum.Datastores.MongoDB.Dbos.Base {

	public abstract class CustomPropertiesHolder {
		public ObjectId Id { get; set; }
		[BsonElement(FieldNames.Properties)]
		public BsonDocument Properties { get; set; }

		public static class FieldNames {
			public const String Id = "_id;";
			public const String Properties = "p";
		}
	}
}
