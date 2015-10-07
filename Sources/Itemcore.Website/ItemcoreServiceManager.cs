using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Reflection;
using System.Text;
using System.Web;
using Itemcore.Service.Exceptions;
using Itemcore.Service.Model;
using Sitecore;
using Sitecore.Caching;
using Sitecore.Configuration;
using Sitecore.Data;
using Sitecore.Data.Fields;
using Sitecore.Data.Items;
using Sitecore.Data.Managers;
using Sitecore.Data.Proxies;
using Sitecore.Data.Serialization;
using Sitecore.Diagnostics;
using Sitecore.Globalization;
using Sitecore.Links;
using Sitecore.Resources;
using Sitecore.SecurityModel;
using Version = Sitecore.Data.Version;

namespace Itemcore.Service
{
	public class ItemcoreServiceManager : IItemcoreServiceManager
	{
		private static readonly ID SharedFieldID = new ID("{BE351A73-FCB0-4213-93FA-C302D8AB4F51}");

		/// <summary>
		///Serialize a Sitecore item and return the contents.
		/// </summary>
		public byte[] GetItem(string itemIdOrPath, string sitecoreDatabase)
		{
			try
			{
				using (new ProxyDisabler())
				{
					using (new SecurityDisabler())
					{
						Context.Items.Add("IsItemcoreRequest", true);
						itemIdOrPath = HttpUtility.UrlDecode(itemIdOrPath);
						if (string.IsNullOrEmpty(itemIdOrPath))
						{
							throw new ArgumentNullException("Item ID or path not specified.");
						}
						var database = GetDatabase(sitecoreDatabase);
						itemIdOrPath = FixWildcardPath(itemIdOrPath);
						var item = database.GetItem(itemIdOrPath);
						if (item == null)
						{
							throw new Exception(string.Format("Item '{0}' not found.", itemIdOrPath));
						}

						if (HasSiblingWithSameName(item))
						{
							throw new InvalidOperationException(
								string.Format(
									"Duplicate item named '{0}' found for item '{1}'.\nDuplicate item names are not currently supported by Itemcore.",
									item.Name, itemIdOrPath));
						}
						var serializedItemPath = GetSerializedItemPath(new ItemReference(item));
						if (File.Exists(serializedItemPath))
						{
							File.Delete(serializedItemPath);
						}

						Manager.DumpItem(serializedItemPath, item);

						if (!File.Exists(serializedItemPath))
						{
							throw new ApplicationException("File not found. '" + serializedItemPath + "'");
						}

						return File.ReadAllBytes(serializedItemPath);
					}
				}
			}
			catch (Exception ex)
			{
				throw this.CreateServiceException(ex);
			}
		}

		/// <summary>
		/// Returns the path to an existing item in the sitecore database.
		/// </summary>
		public string GetItemPath(Guid itemId, string sitecoreDatabase)
		{
			try
			{
				using (new ProxyDisabler())
				{
					using (new SecurityDisabler())
					{
						Context.Items.Add("IsItemcoreRequest", true);
						var item = GetDatabase(sitecoreDatabase).GetItem(new ID(itemId));
						if (item == null)
						{
							return null;
						}
						return item.Paths.Path;
					}
				}
			}
			catch (Exception ex)
			{
				throw this.CreateServiceException(ex);
			}
		}

		/// <summary>
		/// Gets the relative path to the icon for the item.
		/// </summary>
		public string GetItemIcon(string itemIdOrPath, string sitecoreDatabase)
		{
			try
			{
				itemIdOrPath = HttpUtility.UrlDecode(itemIdOrPath);
				if (string.IsNullOrEmpty(itemIdOrPath))
				{
					throw new ArgumentNullException("Item ID or path not specified.");
				}
				using (new ProxyDisabler())
				{
					using (new SecurityDisabler())
					{
						Context.Items.Add("IsItemcoreRequest", true);
						var database = GetDatabase(sitecoreDatabase);
						itemIdOrPath = FixWildcardPath(itemIdOrPath);
						var item = database.GetItem(itemIdOrPath);
						if (item == null)
						{
							throw new Exception(string.Format("Item '{0}' not found.", itemIdOrPath));
						}

						return Images.GetThemedImageSource(item.Appearance.Icon);
					}
				}
			}
			catch (Exception ex)
			{
				throw this.CreateServiceException(ex);
			}
		}

		/// <summary>
		/// Get a list of ChildItem's from the item specified by itemPath.
		/// </summary>
		public ServiceItem[] GetChildren(string itemIdOrPath, string sitecoreDatabase)
		{
			try
			{
				itemIdOrPath = HttpUtility.UrlDecode(itemIdOrPath);
				if (string.IsNullOrEmpty(itemIdOrPath))
				{
					throw new ArgumentNullException("Item ID or path not specified.");
				}

				var list = new List<ServiceItem>();

				using (new ProxyDisabler())
				{
					using (new SecurityDisabler())
					{
						Context.Items.Add("IsItemcoreRequest", true);
						var database = GetDatabase(sitecoreDatabase);
						var idOrPath = itemIdOrPath;
						itemIdOrPath = FixWildcardPath(itemIdOrPath);
						var item = database.GetItem(itemIdOrPath);
						
						if (item == null || idOrPath.StartsWith("/") && item.Paths.FullPath.ToLower() != idOrPath.ToLower())
						{
							throw new Exception(string.Format("Item '{0}' not found.", itemIdOrPath));
						}

						foreach (Item child in item.Children)
						{
							var childItem = new ServiceItem
							{
								Name = child.Name,
								ID = child.ID.ToGuid(),
								TemplateID = child.TemplateID.ToGuid(),
								Icon = Images.GetThemedImageSource(child.Appearance.Icon),
								TemplateName = child.TemplateName,
								HasChildren = child.Children.Count != 0
							};

							list.Add(childItem);
						}
					}
				}
				return list.ToArray();
			}
			catch (Exception ex)
			{
				throw this.CreateServiceException(ex);
			}
		}

		/// <summary>
		/// Creates a .item file and loads it into Sitecore.
		/// </summary>
		public bool LoadItem(string itemPath, string sitecoreDatabase, byte[] definition)
		{
			try
			{
				itemPath = HttpUtility.UrlDecode(itemPath);
				if (string.IsNullOrEmpty(itemPath))
				{
					throw new InvalidOperationException("Item path not specified.");
				}

				Context.Items.Add("IsItemcoreRequest", true);
				
				var flag = true;
				var sitecoreDatabase1 = GetDatabase(sitecoreDatabase);
				var updatedItem = sitecoreDatabase1.GetItem(FixWildcardPath(itemPath));
				
				if (updatedItem != null)
				{
					flag = false;
					ResetSharedFields(updatedItem);
				}

				var serializedItemPath = GetSerializedItemPath(new ItemReference(sitecoreDatabase1.Name, itemPath));
				var fileInfo = new FileInfo(serializedItemPath);
				if (!fileInfo.Exists && fileInfo.Directory != null && !fileInfo.Directory.Exists)
				{
					fileInfo.Directory.Create();
				}

				File.WriteAllBytes(serializedItemPath, definition);
				var options = new LoadOptions(sitecoreDatabase1)
				{
					ForceUpdate = true
				};

				using (new SecurityDisabler())
				{
					try
					{
						updatedItem = Manager.LoadItem(serializedItemPath, options);
						if (flag)
						{
							Item obj = sitecoreDatabase1.GetItem(FixWildcardPath(itemPath));
							if (obj != null)
							{
								if (obj.Fields["__Source"] != null)
								{
									if (obj.Fields["__Source"].HasValue)
									{
										Manager.LoadItem(serializedItemPath, options);
									}
								}
							}
						}
					}
					catch (Exception ex)
					{
						if (ex.Message.Contains("Failed to paste item:"))
						{
							CacheManager.ClearAllCaches();
							updatedItem = Manager.LoadItem(serializedItemPath, options);
						}
						else
						{
							throw;
						}
					}
				}

				return updatedItem != null;
			}
			catch (Exception ex)
			{
				throw this.CreateServiceException(ex);
			}
		}

		/// <summary>
		/// Update a field value for a Sitecore item.
		/// </summary>
		public bool LoadItemField(string itemIdOrPath, string sitecoreDatabase, string languageName, int? version, string fieldIdOrName, string fieldValue)
		{
			try
			{
				itemIdOrPath = HttpUtility.UrlDecode(itemIdOrPath);
				if (string.IsNullOrEmpty(itemIdOrPath))
				{
					throw new InvalidOperationException("Item path not specified.");
				}

				Context.Items.Add("IsItemcoreRequest", true);
				var language = (Language)null;
				if (languageName != null)
				{
					language = LanguageManager.GetLanguage(languageName);
					if (language == null)
					{
						throw new ArgumentNullException(string.Format("Language '{0}' not found.", languageName));
					}
				}

				var database = GetDatabase(sitecoreDatabase);
				using (new ProxyDisabler())
				{
					using (new SecurityDisabler())
					{
						itemIdOrPath = FixWildcardPath(itemIdOrPath);
						var item = !(language == null) ? database.GetItem(itemIdOrPath, language) : database.GetItem(itemIdOrPath);
						if (item == null)
						{
							throw new ArgumentNullException(string.Format("Item '{0}' not found for language '{1}'.", itemIdOrPath, languageName));
						}

						if (version.HasValue)
						{
							item = item.Versions[new Version(version.Value)];
						}

						var field = item.Fields[fieldIdOrName];
						if (field == null)
						{
							throw new ArgumentNullException(string.Format("Field '{0}' not found on item '{1}'.", fieldIdOrName, itemIdOrPath));
						}

						using (new EditContext(item, false, false))
						{
							item.Fields[field.ID].Value = fieldValue;
						}

						Log.Audit(this, "Itemcore deployed '{0}' field for item: {1}", fieldIdOrName, AuditFormatter.FormatItem(item));
					}
				}
				return true;
			}
			catch (Exception ex)
			{
				throw this.CreateServiceException(ex);
			}
		}


		/// <summary>
		/// Deletes the item from Sitecore if not in use.
		/// </summary>
		public bool DeleteItem(string itemPath, string sitecoreDatabase, DeleteActions action)
		{
			try
			{
				Context.Items.Add("IsItemcoreRequest", true);
				var database = GetDatabase(sitecoreDatabase);

				itemPath = FixWildcardPath(itemPath);
				itemPath = HttpUtility.UrlDecode(itemPath);

				if (string.IsNullOrEmpty(itemPath))
				{
					throw new ArgumentNullException("Item ID or path not specified.");
				}

				using (new ProxyDisabler())
				{
					using (new SecurityDisabler())
					{
						var obj = database.GetItem(itemPath);
						IsOkToDelete(obj);
						if (obj != null)
						{
							switch (action)
							{
								case DeleteActions.SitecoreRecycle:
									if (!Settings.RecycleBinActive)
										throw new InvalidOperationException("Sitecore RecycleBin is not active.");
									obj.Recycle();
									break;
								case DeleteActions.Delete:
									obj.Delete();
									break;
							}
						}
					}
				}
				return true;
			}
			catch (Exception ex)
			{
				throw this.CreateServiceException(ex);
			}
		}

		/// <summary>
		/// Returns true if an item exists
		/// </summary>
		public bool ItemExists(string itemIdOrPath, string sitecoreDatabase)
		{
			try
			{
				using (new ProxyDisabler())
				{
					using (new SecurityDisabler())
					{
						Context.Items.Add("IsItemcoreRequest", true);
						itemIdOrPath = HttpUtility.UrlDecode(itemIdOrPath);
						if (string.IsNullOrEmpty(itemIdOrPath))
						{
							throw new ArgumentNullException("Item ID or path not specified.");
						}

						var database = GetDatabase(sitecoreDatabase);
						itemIdOrPath = FixWildcardPath(itemIdOrPath);
						return database.GetItem(itemIdOrPath) != null;
					}
				}
			}
			catch (Exception ex)
			{
				throw this.CreateServiceException(ex);
			}
		}

		/// <summary>
		/// Clears all Sitecore caches.
		/// </summary>
		public bool ClearCache()
		{
			try
			{
				Context.Items.Add("IsItemcoreRequest", true);
				CacheManager.ClearAllCaches();
				return true;
			}
			catch
			{
				return false;
			}
		}

		/// <summary>
		/// Returns the current version
		/// </summary>
		/// <returns></returns>
		public VersionInfo GetItemcoreVersion()
		{
			try
			{
				var versionInfo = FileVersionInfo.GetVersionInfo(Assembly.GetExecutingAssembly().Location);
				return new VersionInfo
				{
					MajorVersion = versionInfo.FileMajorPart,
					MinorVersion = versionInfo.FileMinorPart,
					FileBuild = versionInfo.FileBuildPart,
					FileRevision = versionInfo.FilePrivatePart
				};
			}
			catch (Exception ex)
			{
				throw this.CreateServiceException(ex);
			}
		}

		private static Database GetDatabase(string sitecoreDatabase)
		{
			var database = (Database)null;
			try
			{
				database = Database.GetDatabase(sitecoreDatabase);
			}
			catch
			{
			}

			if (database == null)
			{
				throw new InvalidOperationException(
					string.Format("The database configuration databases/database[@id='{0}'] could not be found.", sitecoreDatabase));
			}
			return database;
		}

		private static bool HasSiblingWithSameName(Item item)
		{
			if (item.Parent != null)
			{
				foreach (Item obj in item.Parent.Children)
				{
					if (string.Compare(obj.Name, item.Name, StringComparison.InvariantCulture) == 0 && obj.ID != item.ID)
						return true;
				}
			}
			return false;
		}

		private static string GetSerializedItemPath(ItemReference itemReference)
		{
			return PathUtils.GetFilePath(itemReference.ToString()).Replace("*", "#asterisk");
		}

		private ItemcoreServiceException CreateServiceException(Exception ex)
		{
			var stringBuilder = new StringBuilder();
			for (var exception = ex; exception != null; exception = exception.InnerException)
			{
				stringBuilder.AppendLine(exception.Message);
			}

			return new ItemcoreServiceException(ex.Message + ". " + stringBuilder, ex);
		}

		private static bool IsOkToDelete(Item item)
		{
			if (item != null)
			{
				if (item.TemplateID == TemplateIDs.Template)
				{
					var referrers = Globals.LinkDatabase.GetReferrers(item);
					if (referrers.Length > 0 && !IsStandardValuesItem(item, referrers))
						throw new InvalidOperationException(string.Format("The \"{0}\" template is in use.\n\nDelete all the items that are based on this template.", (object)item.DisplayName));
				}
				foreach (Item obj in item.Children)
				{
					if (!IsOkToDelete(obj))
						return false;
				}
			}
			return true;
		}

		private static bool IsStandardValuesItem(Item item, IReadOnlyList<ItemLink> links)
		{
			if (links.Count != 1)
			{
				return false;
			}
			var itemLink = links[0];
			var standardValues = (TemplateItem) item.Template.StandardValues;
			
			if (standardValues == null)
			{
				return false;
			}

			if (!(itemLink.SourceItemID == standardValues.ID))
			{
				return itemLink.TargetItemID == standardValues.ID;
			}

			return true;
		}

		private static string FixWildcardPath(string path)
		{
			return path.Replace('*', '?');
		}

		private static void ResetSharedFields(Item updatedItem)
		{
			if (IsSitecoreVersionOrHigher(6, 5, 0, 4043))
				return;
			using (new SecurityDisabler())
			{
				using (new EditContext(updatedItem))
				{
					foreach (Field field in updatedItem.Fields)
					{
						if (field.Shared && field.HasValue && (!(field.ID == SharedFieldID) && !(field.ID == TemplateFieldIDs.Unversioned)))
							field.Reset();
					}
				}
			}
		}

		private static bool IsSitecoreVersionOrHigher(int major, int minor, int build, int version)
		{
			var versionInfo = FileVersionInfo.GetVersionInfo(Assembly.GetAssembly(typeof(Item)).Location);

			return versionInfo.FileMajorPart > major 
				|| versionInfo.FileMinorPart > minor 
				|| (versionInfo.FileBuildPart > build 
				|| versionInfo.FilePrivatePart >= version);
		}
	}
}
