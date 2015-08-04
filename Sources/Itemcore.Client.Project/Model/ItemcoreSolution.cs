using System.Collections.Generic;

namespace Itemcore.Client.Project.Model
{
	public class ItemcoreSolution : IItemcoreSolution
	{
		public IEnumerable<IItemcoreProject> Projects { get; set; }
	}
}
