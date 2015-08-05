using System;
using System.Xml.Serialization;

namespace Itemcore.Client.Settings.Model
{
	[Serializable]
	[XmlRoot("solution")]
	public class RecentSolution : IRecentSolution
	{
		public RecentSolution()
		{
		}

		public RecentSolution(string name, string path)
		{
			this.Path = path;
			this.SolutionName = name;
			this.Timestamp = DateTime.Now;
		}

		[XmlAttribute("timestamp")]
		public DateTime Timestamp { get; set; }

		[XmlAttribute("path")]
		public string Path { get; set; }

		[XmlAttribute("name")]
		public string SolutionName { get; set; }
	}
}
