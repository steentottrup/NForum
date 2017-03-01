using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using NForum.Datastores.MongoDB.Dbos.Base;
using NForum.Domain;
using System;

namespace NForum.Datastores.MongoDB.Dbos {

	public class Reply : ContentHolder {
		[BsonElement(FieldNames.Topic)]
		public TopicRef Topic { get; set; }
		[BsonElement(FieldNames.ReplyTo)]
		public ReplyRef ReplyTo { get; set; }
		[BsonElement(FieldNames.State)]
		public ReplyState State { get; set; }

		public static class FieldNames {
			public const String Topic = "t";
			public const String ReplyTo = "r";
			public const String State = "st";
		}
	}
}
