using System;
using System.Xml.Serialization;

namespace Itemcore.Client.Project.Model
{
	[Serializable]
	[XmlRoot("project")]
	public class SolutionProject : ISolutionProject
	{
		[XmlAttribute("name")]
		public string Name { get; set; }

		[XmlAttribute("relativePath")]
		public string RelativePath { get; set; }
	}
}
