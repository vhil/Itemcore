using System;
using System.Xml.Serialization;

namespace Itemcore.Client.Project.Model
{
	[Serializable]
	[XmlRoot("item")]
	public class ItemcoreItem : IItemcoreItem
	{
		[XmlAttribute("id")]
		public Guid ID { get; set; }

		[XmlAttribute("path")]
		public string PhysicalPath { get; set; }

		[XmlAttribute("icon")]
		public string Icon { get; set; }

		[XmlAttribute("syncChildren")]
		public bool SynchronizeChildren { get; set; }
	}
}
