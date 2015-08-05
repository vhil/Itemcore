using System;
using System.Collections.Generic;
using System.Xml.Serialization;

namespace Itemcore.Client.Project.Model
{
	[Serializable]
	[XmlRoot("solution")]
	public class ItemcoreSolution : IItemcoreSolution
	{
		[XmlIgnore]
		public string SolutionFilePath { get; set; }

		[XmlIgnore]
		public string BaseDirectory { get; set; }

		[XmlAttribute("name")]
		public string Name { get; set; }
		
		[XmlIgnore]
		public IEnumerable<IItemcoreSolutionProject> Projects { get; set; }

		[XmlArray("projects")]
		[XmlArrayItem("project")]
		public List<ItemcoreSolutionProject> projects
		{
			get { return this.Projects as List<ItemcoreSolutionProject>; }
			set { this.Projects = value; }
		}

		public void AddProject(IItemcoreSolutionProject project)
		{
			this.projects.Add(project as ItemcoreSolutionProject);
		}
	}
}
