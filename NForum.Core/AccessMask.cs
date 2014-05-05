using System;
using System.ComponentModel.DataAnnotations;

namespace NForum.Core {
	
	public class AccessMask {
		public Int32 Id { get; set; }
		public Int32 BoardId { get; set; }
		public String Name { get; set; }
		public Int64 Mask { get; set; }

		public Board Board { get; set; }
	}
}