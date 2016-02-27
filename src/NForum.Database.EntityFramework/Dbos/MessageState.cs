using System;

namespace NForum.Database.EntityFramework.Dbos {

	public enum MessageState {
		None = 0,
		Quarantined = 1,
		Deleted = 2
	}
}
