using System;

namespace Itemcore.Client.Project.Model
{
	public interface IProjectItem
	{
		Guid ID { get; set; }
		string PhysicalPath { get; set; }
		string Icon { get; set; }
		bool SynchronizeChildren { get; set; }
	}
}
