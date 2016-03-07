using System;

namespace NForum.Database.EntityFramework.Dbos {

	public class ForumUser {
		public Guid Id { get; set; }
		public String Username { get; set; }
		public String EmailAddress { get; set; }
		public String Fullname { get; set; }
		public String ExternalId { get; set; }
		public Boolean Deleted { get; set; }
		public String Culture { get; set; }
		public String Timezone { get; set; }
		public Boolean UseFullname { get; set; }
	}
}
