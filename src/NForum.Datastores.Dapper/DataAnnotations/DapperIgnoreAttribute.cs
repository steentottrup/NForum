using System;

namespace NForum.Datastores.Dapper.DatAnnotations {

	[AttributeUsage(AttributeTargets.Property)]
	public class DapperIgnoreAttribute : Attribute { }
}
