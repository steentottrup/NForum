//using NForum.Core.Abstractions;
//using System;
//using System.Collections.Generic;
//using System.ComponentModel.DataAnnotations;
//using System.Xml.Linq;

//namespace NForum.Core {

//	public class Board : INamedElement, ISortableElement, ICustomPropertyHolder, ICloneable {
//		public Int32 Id { get; set; }
//		public String Name { get; set; }
//		public String Description { get; set; }
//		public Int32 SortOrder { get; set; }

//		public Int32 TopicsPerPage { get; set; }
//		public Int32 PostsPerPage { get; set; }

//		/// <summary>
//		/// Data container for custom properties. Do NOT read or write this property directly, use the extension methods.
//		/// </summary>
//		public String CustomProperties { get; set; }
//		/// <summary>
//		/// Xml container for custom properties. Do NOT use this property directly, use the extension methods.
//		/// </summary>
//		public XDocument CustomData { get; set; }

//		/// <summary>
//		/// Are the user allowed to pick a theme.
//		/// </summary>
//		public Boolean UserTheme { get; set; }
//		/// <summary>
//		/// The base theme of the board.
//		/// </summary>
//		public String Theme { get; set; }
//		/// <summary>
//		/// The timezone of the board.
//		/// </summary>
//		public String Timezone { get; set; }
//		/// <summary>
//		/// The region/language of the board.
//		/// </summary>
//		public String Culture { get; set; }
//		/// <summary>
//		/// A list of group Ids for the groups a new user will be assigned.
//		/// </summary>
//		public String NewUserGroups { get; set; }

//		public virtual ICollection<Category> Categories { get; set; }

//		public Object Clone() {
//			return new Board {
//				Id = this.Id,
//				Name = this.Name,
//				Description = this.Description,
//				SortOrder = this.SortOrder,
//				TopicsPerPage = this.TopicsPerPage,
//				PostsPerPage = this.PostsPerPage,
//				CustomProperties = this.CustomProperties
//			};
//		}
//	}
//}