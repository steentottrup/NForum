using System;

namespace NForum.Core.Events {

	public enum EventPriority {
		Highest = 0,
		Medium = 128,
		/// <summary>
		/// This is the lowest priority, subscribers with this low a priority should NOT change the payload!
		/// </summary>
		Lowest = 255
	}
}