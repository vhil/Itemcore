using System;
using System.Diagnostics;
using System.Web.Mvc;
using Itemcore.Service.ActionFilters;

namespace Itemcore.Service.Controllers
{
	[ResponseTimeMeasurement]
	[SitecoreAccessId]
	public abstract class BaseJsonController : Controller
	{
		protected JsonResult BuildJsonResult(object data, JsonRequestBehavior behavior = JsonRequestBehavior.AllowGet, Exception ex = null)
		{
			var error = RouteData.Values["error"] as Exception;

			if (error != null)
			{
				RouteData.Values["error"] = null;
				throw error;
			}

			long elapsedMilliseconds = 0;
			var stopwatch = RouteData.Values["stopwatch"] as Stopwatch;

			if (stopwatch != null)
			{
				stopwatch.Stop();
				elapsedMilliseconds = stopwatch.ElapsedMilliseconds;
			}

			Response.ContentType = "application/json";

			return new JsonResult
			{
				Data = new
				{
					data,
					success = ex == null && data != null,
					error = ex != null 
						? ex.ToString() 
						: data == null 
							? new ArgumentNullException("The service did not return a valid data. The data is null").ToString()
							: "",
					responseTimeMs = elapsedMilliseconds,
				},
				JsonRequestBehavior = behavior
			};
		}
	}
}
