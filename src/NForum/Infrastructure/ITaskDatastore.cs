using System;

namespace NForum.Infrastructure {

	public interface ITaskDatastore {
		void SetTaskStatus(Guid id, String elementId, String type /* status ???*/);
		Tuple<String, String> GetTaskStatus(Guid id/* status ???*/);
	}
}
