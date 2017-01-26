using NForum.Core.Dtos;
using System;
using NForum.Core.Refs;
using System.Collections.Generic;

namespace NForum.Datastores.MongoDB.Dtos {

	public class Reply : IReplyDto {
		public String Content {
			get {
				throw new NotImplementedException();
			}
		}

		public DateTime Created {
			get {
				throw new NotImplementedException();
			}
		}

		public IAuthorRef CreatedBy {
			get {
				throw new NotImplementedException();
			}
		}

		public IDictionary<String, Object> CustomProperties {
			get {
				throw new NotImplementedException();
			}
		}

		public String Id {
			get {
				throw new NotImplementedException();
			}
		}

		public DateTime LastEdited {
			get {
				throw new NotImplementedException();
			}
		}

		public IAuthorRef LastEditedBy {
			get {
				throw new NotImplementedException();
			}
		}

		public IReplyRef ReplyTo {
			get {
				throw new NotImplementedException();
			}
		}

		public String Subject {
			get {
				throw new NotImplementedException();
			}
		}

		public ITopicRef Topic {
			get {
				throw new NotImplementedException();
			}
		}
	}
}
