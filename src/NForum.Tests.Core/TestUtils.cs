using NForum.CQS;
using NSubstitute;
using System;

namespace NForum.Tests.Core {

	public static class TestUtils {
		public static IIdValidator GetInt32IdValidator() {
			return new Int32IdValidator();
		}

		private class Int32IdValidator : IIdValidator {
			public Boolean IsValid(String id) {
				Int32 temp;
				return Int32.TryParse(id, out temp);
			}
		}
	}
}
