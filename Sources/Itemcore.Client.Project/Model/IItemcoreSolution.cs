using System.Collections.Generic;

namespace Itemcore.Client.Project.Model
{
	public interface IItemcoreSolution
	{
		IEnumerable<IItemcoreProject> Projects { get; set; } 
	}
}
