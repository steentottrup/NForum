using System;
using System.ComponentModel.DataAnnotations;

namespace NForum.Core {

	public class SolutionSettings {
		public Int32 Id { get; set; }
		public String Key { get; set; }
		public String Value { get; set; }
	}
}