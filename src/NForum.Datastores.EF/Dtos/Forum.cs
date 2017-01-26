using NForum.Core.Dtos;
using System;
using NForum.Core.Refs;
using System.Collections.Generic;

namespace NForum.Datastores.EF.Dtos {

	public class Forum : IForumDto {

		public ICategoryRef Category {
			get {
				throw new NotImplementedException();
			}
		}

		public IDictionary<String, Object> CustomProperties {
			get {
				throw new NotImplementedException();
			}
		}

		public String Description {
			get {
				throw new NotImplementedException();
			}
		}

		public String Id {
			get {
				throw new NotImplementedException();
			}
		}

		public String Name {
			get {
				throw new NotImplementedException();
			}
		}

		public IForumRef ParentForum {
			get {
				throw new NotImplementedException();
			}
		}

		public IEnumerable<String> Path {
			get {
				throw new NotImplementedException();
			}
		}

		public Int32 SortOrder {
			get {
				throw new NotImplementedException();
			}
		}
	}
}
