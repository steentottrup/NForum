using System;

namespace NForum.Core {

	public static class ForumConfigurationExtensions {

		public static DateTime InstallationDate(this ForumConfiguration conf) {
			return conf.GetCustomPropertyDateTime(ForumConfiguration.PropertyNames.InstallationDate);
		}

		public static String SenderEmailAddress(this ForumConfiguration conf) {
			return conf.GetCustomPropertyString(ForumConfiguration.PropertyNames.SenderEmailAddress);
		}

		public static String SenderName(this ForumConfiguration conf) {
			return conf.GetCustomPropertyString(ForumConfiguration.PropertyNames.SenderName);
		}

		public static String Version(this ForumConfiguration conf) {
			return conf.GetCustomPropertyString(ForumConfiguration.PropertyNames.Version);
		}

		public static String Theme(this ForumConfiguration conf) {
			return conf.GetCustomPropertyString(ForumConfiguration.PropertyNames.Theme);
		}

		public static void SetTheme(this ForumConfiguration conf, String theme) {
			conf.SetCustomProperty(ForumConfiguration.PropertyNames.Theme, theme);
		}

		public static Int32 PostsPerPage(this ForumConfiguration conf) {
			return conf.GetCustomPropertyInt32(ForumConfiguration.PropertyNames.PostsPerPage);
		}

		public static void SetPostsPerPage(this ForumConfiguration conf, Int32 postsPerPage) {
			conf.SetCustomProperty(ForumConfiguration.PropertyNames.PostsPerPage, postsPerPage);
		}

		public static Int32 TopicsPerPage(this ForumConfiguration conf) {
			return conf.GetCustomPropertyInt32(ForumConfiguration.PropertyNames.TopicsPerPage);
		}

		public static void SetTopicsPerPage(this ForumConfiguration conf, Int32 topicsPerPage) {
			conf.SetCustomProperty(ForumConfiguration.PropertyNames.TopicsPerPage, topicsPerPage);
		}
	}
}