using System;
using System.Collections.Generic;

namespace Itemcore.Client.Project.Model
{
	public interface IProject
	{
		string ProjectBaseDirectory { get; set; }
		string ProjectFilePath { get; set; }
		string WebsiteBaseUrl { get; set; }
		Guid SitecoreAccessId { get; set; }
		string SitecoreDatabaseName { get; set; }
		IEnumerable<IProjectItem> Items { get; set; }
		void AddItem(IProjectItem item);
		void RemoveItem(IProjectItem item);
	}
}
