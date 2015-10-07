using System.Diagnostics;
using System.Web.Mvc;

namespace Itemcore.Service.ActionFilters
{
	public class ResponseTimeMeasurementAttribute : ActionFilterAttribute
	{
		private Stopwatch stopwatch;

		public override void OnActionExecuting(ActionExecutingContext filterContext)
		{
			base.OnActionExecuting(filterContext);

			stopwatch = new Stopwatch();
			stopwatch.Start();
			filterContext.RouteData.Values.Add("stopwatch", stopwatch);
		}

		public override void OnResultExecuted(ResultExecutedContext filterContext)
		{
			base.OnResultExecuted(filterContext);

			if (this.stopwatch.IsRunning)
			{
				this.stopwatch.Stop();
			}

			this.stopwatch = null;
		}
	}
}
