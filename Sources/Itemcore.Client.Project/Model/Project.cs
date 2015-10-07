using System;
using System.Collections.Generic;
using System.Linq;
using System.Xml.Serialization;

namespace Itemcore.Client.Project.Model
{
	[Serializable]
	[XmlRoot("project")]
	public class Project : IProject
	{
		public Project()
		{
			this.items = new List<ProjectItem>();
		}

		private Dictionary<string, IProjectItem> itemDictionary = new Dictionary<string, IProjectItem>();

		[XmlIgnore]
		public string ProjectBaseDirectory { get; set; }

		[XmlIgnore]
		public string ProjectFilePath { get; set; }

		[XmlAttribute("websiteUrl")]
		public string WebsiteBaseUrl { get; set; }

		[XmlAttribute("sitecoreAccessId")]
		public Guid SitecoreAccessId { get; set; }

		[XmlAttribute("database")]
		public string SitecoreDatabaseName { get; set; }

		[XmlIgnore]
		public IEnumerable<IProjectItem> Items { get; set; }

		public void AddItem(IProjectItem item)
		{
			if (!itemDictionary.ContainsKey(item.PhysicalPath))
			{
				items.Add(item as ProjectItem);
				itemDictionary.Add(item.PhysicalPath, item);
			}
		}

		public void RemoveItem(IProjectItem item)
		{
			if (itemDictionary.ContainsKey(item.PhysicalPath))
			{
				var foundItem = itemDictionary[item.PhysicalPath];
				items.Remove(foundItem as ProjectItem);
				itemDictionary.Remove(item.PhysicalPath);
			}

			// TODO: remove children reccursively;
		}

		[XmlArray("items")]
		[XmlArrayItem("item")]
		public List<ProjectItem> items
		{
			get { return this.Items as List<ProjectItem>; }
			set
			{
				this.Items = value.OrderBy(x=> x.PhysicalPath).ToList();
				itemDictionary = value.ToDictionary(x => x.PhysicalPath, v => v as IProjectItem);
			}
		}
	}
}
