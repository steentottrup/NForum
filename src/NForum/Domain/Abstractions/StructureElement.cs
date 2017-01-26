using NForum.Core.Dtos;
using System;

namespace NForum.Domain.Abstractions {

	public abstract class StructureElement : CustomPropertiesHolder {

		protected StructureElement(String name, Int32 sortOrder, String desciption) : base() {
			this.Name = name;
			this.SortOrder = sortOrder;
			this.Description = desciption;
		}

		protected StructureElement(IStrutureElementDto data) : base(data) {
			this.SortOrder = data.SortOrder;
			this.Name = data.Name;
			this.Description = data.Description;
		}

		public virtual Int32 SortOrder { get; set; }
		public virtual String Name { get; set; }
		public virtual String Description { get; set; }
	}
}
