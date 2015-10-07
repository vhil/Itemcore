using System.Web.Mvc;
using System.Web.Routing;
using Itemcore.Service;
using WebActivatorEx;

[assembly: PostApplicationStartMethod(typeof(RouteConfig), "Initialize", Order = 200)]
namespace Itemcore.Service
{

	public class RouteConfig
	{
		public static void Initialize()
		{
			RegisterRoutes(RouteTable.Routes);
		}

		public static void RegisterRoutes(RouteCollection routes)
		{
			routes.MapRoute(
				name: "Itemcore",
				url: "-/itemcore/{controller}/{action}/{id}",
				defaults: new { controller = "Service", action = "Heartbeat", id = "required" });
		}
	}
}
