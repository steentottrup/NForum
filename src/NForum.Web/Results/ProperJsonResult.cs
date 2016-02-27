using System;
using System.Web;
using System.Web.Mvc;

namespace NForum.Web.Results {

	public class ProperJsonResult : JsonResult {

		public override void ExecuteResult(ControllerContext context) {
			if (context == null) {
				throw new ArgumentNullException("context");
			}
			if (JsonRequestBehavior == JsonRequestBehavior.DenyGet && String.Equals(context.HttpContext.Request.HttpMethod, "GET", StringComparison.OrdinalIgnoreCase)) {
				throw new InvalidOperationException("GET is not allowed");
			}

			HttpResponseBase response = context.HttpContext.Response;

			if (!String.IsNullOrEmpty(ContentType)) {
				response.ContentType = ContentType;
			}
			else {
				response.ContentType = "application/json";
			}
			if (ContentEncoding != null) {
				response.ContentEncoding = ContentEncoding;
			}
			if (Data != null) {
				response.Write(HtmlExtensions.ToJSON(this.Data));
			}
		}
	}
}
