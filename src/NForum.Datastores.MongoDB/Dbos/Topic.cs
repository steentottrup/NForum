using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using NForum.Datastores.MongoDB.Dbos.Base;
using System;

namespace NForum.Datastores.MongoDB.Dbos {

	public class Topic : ContentHolder {
		[BsonElement(FieldNames.Forum)]
		public ForumRef Forum { get; set; }

		public static class FieldNames {
			public const String Forum = "f";
		}
	}
}
