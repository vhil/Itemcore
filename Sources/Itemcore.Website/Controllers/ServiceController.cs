using System;
using System.Web.Mvc;
using Itemcore.Service.Model;

namespace Itemcore.Service.Controllers
{
	public class ServiceController : BaseJsonController
	{
		private IItemcoreServiceManager serviceManager;

		protected IItemcoreServiceManager ServiceManager
		{
			get { return serviceManager ?? (serviceManager = new ItemcoreServiceManager()); }
		}

		public JsonResult GetItem(string itemIdOrPath, string database)
		{
			try
			{
				return this.BuildJsonResult(this.ServiceManager.GetItem(itemIdOrPath, database));
			}
			catch (Exception ex)
			{
				return this.BuildJsonResult(null, ex: ex);
			}
		}

		public JsonResult GetItemPath(Guid itemId, string database)
		{
			try
			{
				return this.BuildJsonResult(this.ServiceManager.GetItemPath(itemId, database));
			}
			catch (Exception ex)
			{
				return this.BuildJsonResult(null, ex: ex);
			}
		}

		public JsonResult GetItemIcon(string itemIdOrPath, string database)
		{
			try
			{
				return this.BuildJsonResult(this.ServiceManager.GetItemIcon(itemIdOrPath, database));
			}
			catch (Exception ex)
			{
				return this.BuildJsonResult(null, ex: ex);
			}
		}

		public JsonResult GetChildren(string itemIdOrPath, string database)
		{
			try
			{
				return this.BuildJsonResult(this.ServiceManager.GetChildren(itemIdOrPath, database));
			}
			catch (Exception ex)
			{
				return this.BuildJsonResult(null, ex: ex);
			}
		}

		public JsonResult LoadItem(string itemPath, string database, byte[] definition)
		{
			try
			{
				return this.BuildJsonResult(this.ServiceManager.LoadItem(itemPath, database, definition));
			}
			catch (Exception ex)
			{
				return this.BuildJsonResult(null, ex: ex);
			}
		}

		public JsonResult LoadItemField(
			string itemIdOrPath,
			string database,
			string languageName,
			int? version,
			string fieldIdOrName,
			string fieldValue)
		{
			try
			{
				return this.BuildJsonResult(this.ServiceManager.LoadItemField(
				   itemIdOrPath,
				   database,
				   languageName,
				   version,
				   fieldIdOrName,
				   fieldValue));
			}
			catch (Exception ex)
			{
				return this.BuildJsonResult(null, ex: ex);
			}
		}

		public JsonResult DeleteItem(string itemPath, string database, DeleteActions action)
		{
			try
			{
				return this.BuildJsonResult(this.ServiceManager.DeleteItem(itemPath, database, action));
			}
			catch (Exception ex)
			{
				return this.BuildJsonResult(null, ex: ex);
			}
		}

		public JsonResult ItemExists(string itemIdOrPath, string database)
		{
			try
			{
				return this.BuildJsonResult(this.ServiceManager.ItemExists(itemIdOrPath, database));
			}
			catch (Exception ex)
			{
				return this.BuildJsonResult(null, ex: ex);
			}
		}

		public JsonResult ClearCache()
		{
			try
			{
				return this.BuildJsonResult(this.ServiceManager.ClearCache());
			}
			catch (Exception ex)
			{
				return this.BuildJsonResult(null, ex: ex);
			}
		}

		public JsonResult ItemcoreVersion()
		{
			try
			{
				return this.BuildJsonResult(this.ServiceManager.GetItemcoreVersion());
			}
			catch (Exception ex)
			{
				return this.BuildJsonResult(null, ex: ex);
			}
		}
	}
}
