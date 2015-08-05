using System;
using System.Collections.Generic;

namespace Itemcore.Client.Project.Model
{
	public interface IItemcoreProject
	{
		string ProjectBaseDirectory { get; set; }
		string ProjectFilePath { get; set; }
		string WebsiteBaseUrl { get; set; }
		Guid SitecoreConnectorAccessId { get; set; }
		string SitecoreDatabaseName { get; set; }
		IEnumerable<IItemcoreItem> Items { get; set; }
		void AddItem(IItemcoreItem item);
		void RemoveItem(IItemcoreItem item);
	}
}
