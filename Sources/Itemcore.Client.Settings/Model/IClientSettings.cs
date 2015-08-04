using System.Collections.Generic;

namespace Itemcore.Client.Settings.Model
{
	public interface IClientSettings
	{
		string LastOpenedProjectLocation { get; set; }
		IEnumerable<string> RecentSolutions { get; set; } 
	}
}
