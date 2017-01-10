using System;

namespace NForum.Infrastructure {

	public class BuilderBoardConfiguration : IBoardConfiguration {
		public Boolean AllowAnonymousVisitors {
			get {
				return true;
			}
		}
	}
}
