using System;
using NForum.CQS;
using MongoDB.Bson;

namespace NForum.Datastores.MongoDB {

	public class ObjectIdValidator : IIdValidator {

		public Boolean IsValid(String id) {
			ObjectId temp;
			return ObjectId.TryParse(id, out temp);
		}
	}
}
