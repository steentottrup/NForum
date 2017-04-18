using System;

namespace NForum.Domain.Abstractions {

	public class MissingEditorException : Exception {
		public MissingEditorException() : base("You need to set the editor before changing properties") {
		}
	}
}