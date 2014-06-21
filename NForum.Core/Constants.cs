using System;

namespace NForum.Core {

	public static class Constants {
		public static class FieldLengths {
			public const Int32 ForumName = 200;
			public const Int32 CategoryName = 200;
			public const Int32 BoardName = 200;
			public const Int32 GroupName = 200;
			public const Int32 AccessMaskName = 200;

			// User
			public const Int32 UserName = 200;
			public const Int32 FullName = 200;
			public const Int32 EmailAddress = 200;
			public const Int32 ProviderId = 200;
			public const Int32 TimeZone = 50;
			public const Int32 Culture = 10;

			// Attachment
			public const Int32 Filename = 200;
			public const Int32 Path = 1000;

			public const Int32 SettingsKey = 50;
		}
	}
}