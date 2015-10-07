using System;
using System.Web;
using System.Web.Mvc;
using Itemcore.Service.Exceptions;
using Sitecore.Configuration;

namespace Itemcore.Service.ActionFilters
{
	public class SitecoreAccessIdAttribute : ActionFilterAttribute
	{
		public override void OnActionExecuting(ActionExecutingContext filterContext)
		{
			base.OnActionExecuting(filterContext);

			if (this.RequestedSitecoreAccessID != this.SitecoreAccessID)
			{
				filterContext.RouteData.Values.Add("error", new SitecoreAccessIdException("Itemcore Access is denied. Provided Sitecore Access ID does not correspond to configured Itemcore.SitecoreAccessID setting."));
			}
		}

		public string RequestedSitecoreAccessID
		{
			get { return HttpContext.Current.Request["accessID"] ?? Guid.NewGuid().ToString(); }
		}

		public string SitecoreAccessID
		{
			get
			{
				var accessId = Settings.GetSetting("Itemcore.SitecoreAccessID");
				
				if (string.IsNullOrEmpty(accessId))
				{
					throw new SitecoreAccessIdException("Itemcore Access is denied. The website doesnt have Sitecore Access Id in place. Please configure the Itemcore.SitecoreAccessID sitecore setting.");
				}

				return accessId;
			}
		}
	}
}
