using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;
using System;
using System.Web;
using System.Web.Mvc;

namespace NForum.Web {

	public static class HtmlExtensions {

		public static IHtmlString ToJSON(this HtmlHelper html, Object input) {
			return html.Raw(ToJSON(input));
		}

		public static String ToJSON(Object input) {
			JsonSerializerSettings camelCaseFormatter = new JsonSerializerSettings();
			camelCaseFormatter.ContractResolver = new CamelCasePropertyNamesContractResolver();
			return JsonConvert.SerializeObject(input, camelCaseFormatter);
		}
	}
}
