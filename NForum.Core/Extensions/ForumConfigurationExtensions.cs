using System;

namespace NForum.Core {

	public static class ForumConfigurationExtensions {

		public static DateTime InstallationDate(this ForumConfiguration conf) {
			return conf.GetCustomPropertyDateTime(ForumConfiguration.FieldNames.InstallationDate);
		}

		public static String Version(this ForumConfiguration conf) {
			return conf.GetCustomPropertyString(ForumConfiguration.FieldNames.Version);
		}

		public static String Theme(this ForumConfiguration conf) {
			return conf.GetCustomPropertyString(ForumConfiguration.FieldNames.Theme);
		}

		public static void SetTheme(this ForumConfiguration conf, String theme) {
			conf.SetCustomProperty(ForumConfiguration.FieldNames.Theme, theme);
		}

		public static Int32 PostsPerPage(this ForumConfiguration conf) {
			return conf.GetCustomPropertyInt32(ForumConfiguration.FieldNames.PostsPerPage);
		}

		public static void SetPostsPerPage(this ForumConfiguration conf, Int32 postsPerPage) {
			conf.SetCustomProperty(ForumConfiguration.FieldNames.PostsPerPage, postsPerPage);
		}

		public static Int32 TopicsPerPage(this ForumConfiguration conf) {
			return conf.GetCustomPropertyInt32(ForumConfiguration.FieldNames.TopicsPerPage);
		}

		public static void SetTopicsPerPage(this ForumConfiguration conf, Int32 topicsPerPage) {
			conf.SetCustomProperty(ForumConfiguration.FieldNames.TopicsPerPage, topicsPerPage);
		}
	}
}