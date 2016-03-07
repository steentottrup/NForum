using NForum.Core.Abstractions;
using System;

namespace NForum.Tests.Common {

	public class FakeSettings : ISettings {
		public Int32 RepliesPerPage {
			get {
				return 20;
			}
		}

		public Int32 TopicsPerPage {
			get {
				return 20;
			}
		}
	}
}
