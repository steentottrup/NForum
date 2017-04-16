using System;

namespace NForum.Infrastructure {

	public class BuilderBoardConfiguration : IBoardConfiguration {

		public Boolean AllowAnonymousVisitors {
			get {
				return true;
			}
		}

		public String GetAdminGroupName() {
			// TODO:
			return "Administrator";
		}
	}
}
