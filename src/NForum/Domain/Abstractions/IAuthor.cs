﻿using System;

namespace NForum.Domain.Abstractions {

	public interface IAuthor {
		String GetId();
		String GetUsername();
		String GetFullname();
	}
}
