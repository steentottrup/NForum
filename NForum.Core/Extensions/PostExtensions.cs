using System;

namespace NForum.Core {

	public static class PostExtensions {

		public static Boolean IsVisible(this Post p) {
			return p.State == PostState.None;
		}
	}
}