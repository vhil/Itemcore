using System;
using System.Collections.Generic;

namespace Itemcore.Client.Settings.Model
{
	[Serializable]
	public class ClientSettings : IClientSettings
	{
		public string LastOpenedProjectLocation { get; set; }
		public IEnumerable<string> RecentSolutions { get; set; }
	}
}
