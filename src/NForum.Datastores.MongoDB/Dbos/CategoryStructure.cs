﻿using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using System;

namespace NForum.Datastores.MongoDB.Dbos {

	public class CategoryStructure : Category {
		[BsonElement(FieldNames.Forums)]
		public ForumStructure[] Forums { get; set; }

		public static class FieldNames {
			public const String Forums = "f";
		}
	}
}
