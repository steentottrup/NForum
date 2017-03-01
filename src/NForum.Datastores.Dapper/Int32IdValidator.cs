using NForum.CQS;
using System;

namespace NForum.Datastores.Dapper {

	public class Int32IdValidator : IIdValidator {

		public Boolean IsValid(String id) {
			Int32 temp;
			return Int32.TryParse(id, out temp);
		}
	}
}
