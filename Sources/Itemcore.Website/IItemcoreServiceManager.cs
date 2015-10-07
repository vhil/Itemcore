using System;
using Itemcore.Service.Model;

namespace Itemcore.Service
{
	public interface IItemcoreServiceManager
	{
		/// <summary>
		///Serialize a Sitecore item and return the contents.
		/// </summary>
		byte[] GetItem(string itemIdOrPath, string sitecoreDatabase);

		/// <summary>
		/// Returns the path to an existing item in the sitecore database.
		/// </summary>
		string GetItemPath(Guid itemId, string sitecoreDatabase);

		/// <summary>
		/// Gets the relative path to the icon for the item.
		/// </summary>
		string GetItemIcon(string itemIdOrPath, string sitecoreDatabase);

		/// <summary>
		/// Get a list of ChildItem's from the item specified by itemPath.
		/// </summary>
		ServiceItem[] GetChildren(string itemIdOrPath, string sitecoreDatabase);

		/// <summary>
		/// Creates a .item file and loads it into Sitecore.
		/// </summary>
		bool LoadItem(string itemPath, string sitecoreDatabase, byte[] definition);

		/// <summary>
		/// Update a field value for a Sitecore item.
		/// </summary>
		bool LoadItemField(string itemIdOrPath, string sitecoreDatabase, string languageName, int? version, string fieldIdOrName, string fieldValue);

		/// <summary>
		/// Deletes the item from Sitecore if not in use.
		/// </summary>
		bool DeleteItem(string itemPath, string sitecoreDatabase, DeleteActions action);

		/// <summary>
		/// Returns true if an item exists
		/// </summary>
		bool ItemExists(string itemIdOrPath, string sitecoreDatabase);

		/// <summary>
		/// Clears all Sitecore caches.
		/// </summary>
		bool ClearCache();

		/// <summary>
		/// Returns the current version
		/// </summary>
		/// <returns></returns>
		VersionInfo GetItemcoreVersion();
	}
}