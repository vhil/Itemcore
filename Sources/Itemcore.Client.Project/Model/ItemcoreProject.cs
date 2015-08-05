using System;
using System.Collections.Generic;
using System.Linq;
using System.Xml.Serialization;

namespace Itemcore.Client.Project.Model
{
	[Serializable]
	[XmlRoot("project")]
	public class ItemcoreProject : IItemcoreProject
	{
		private Dictionary<string, IItemcoreItem> itemDictionary = new Dictionary<string, IItemcoreItem>();

		[XmlIgnore]
		public string ProjectBaseDirectory { get; set; }

		[XmlIgnore]
		public string ProjectFilePath { get; set; }

		[XmlAttribute("websiteUrl")]
		public string WebsiteBaseUrl { get; set; }

		[XmlAttribute("sitecoreAccessId")]
		public Guid SitecoreConnectorAccessId { get; set; }

		[XmlAttribute("database")]
		public string SitecoreDatabaseName { get; set; }

		[XmlIgnore]
		public IEnumerable<IItemcoreItem> Items { get; set; }

		public void AddItem(IItemcoreItem item)
		{
			if (!itemDictionary.ContainsKey(item.PhysicalPath))
			{
				items.Add(item as ItemcoreItem);
				itemDictionary.Add(item.PhysicalPath, item);
			}
		}

		public void RemoveItem(IItemcoreItem item)
		{
			if (itemDictionary.ContainsKey(item.PhysicalPath))
			{
				var foundItem = itemDictionary[item.PhysicalPath];
				items.Remove(foundItem as ItemcoreItem);
				itemDictionary.Remove(item.PhysicalPath);
			}

			// TODO: remove children reccursively;
		}

		[XmlArray("items")]
		[XmlArrayItem("item")]
		public List<ItemcoreItem> items
		{
			get { return this.Items as List<ItemcoreItem>; }
			set
			{
				this.Items = value.OrderBy(x=> x.PhysicalPath).ToList();
				itemDictionary = value.ToDictionary(x => x.PhysicalPath, v => v as IItemcoreItem);
			}
		}
	}
}
