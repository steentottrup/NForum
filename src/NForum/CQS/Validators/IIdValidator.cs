using System;

namespace NForum.CQS {

	public interface IIdValidator {
		Boolean IsValid(String id);
	}
}
