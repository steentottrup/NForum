using System;

namespace NForum.Core {

	[Flags]
	public enum CRUD {
		Create,
		Read,
		Update,
		Delete
	}
}