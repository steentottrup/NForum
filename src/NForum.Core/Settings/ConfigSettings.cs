using NForum.Core.Abstractions;
using System;

namespace NForum.Core.Settings {

	public class ConfigSettings : ISettings {

		public Int32 RepliesPerPage {
			get {
				return 20;
			}
		}

		public Int32 TopicsPerPage {
			get {
				return 25;
			}
		}
	}
}
