using NForum.Web.Results;
using System;
using System.Web.Mvc;

namespace NForum.Web.Controllers.App {

	public abstract class BaseAppController : Controller {

		protected new JsonResult Json(Object data) {
			return this.Json(data, JsonRequestBehavior.AllowGet);
		}

		protected new JsonResult Json(Object data, JsonRequestBehavior behavior) {
			return new ProperJsonResult {
				Data = data,
				JsonRequestBehavior = behavior
			};
		}
	}
}
