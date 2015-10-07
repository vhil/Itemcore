using System;
using System.Collections.Generic;
using System.Xml.Serialization;

namespace Itemcore.Client.Project.Model
{
	[Serializable]
	[XmlRoot("solution")]
	public class Solution : ISolution
	{
		public Solution()
		{
			this.projects = new List<SolutionProject>();
		}

		[XmlIgnore]
		public string SolutionFilePath { get; set; }

		[XmlIgnore]
		public string BaseDirectory { get; set; }

		[XmlAttribute("name")]
		public string Name { get; set; }
		
		[XmlIgnore]
		public IEnumerable<ISolutionProject> Projects { get; set; }

		[XmlArray("projects")]
		[XmlArrayItem("project")]
		public List<SolutionProject> projects
		{
			get { return this.Projects as List<SolutionProject>; }
			set { this.Projects = value; }
		}

		public void AddProject(ISolutionProject project)
		{
			this.projects.Add(project as SolutionProject);
		}
	}
}
