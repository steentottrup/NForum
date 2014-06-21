using System;

namespace NForum.Core {

	public static class PostExtensions {

		/// <summary>
		/// Method for checking if a post has "gone public".
		/// </summary>
		/// <param name="p">The post in question.</param>
		/// <param name="oldPost">The post with the old state.</param>
		/// <returns>True if the PostState on the post has gone from Deletd/Quarantined to None, else false.</returns>
		public static Boolean GonePublic(this Post p, Post oldPost) {
			return !oldPost.IsVisible() && p.IsVisible();
		}

		public static Boolean IsVisible(this Post p) {
			return p.State != PostState.Deleted && p.State != PostState.Quarantined;
		}
	}
}