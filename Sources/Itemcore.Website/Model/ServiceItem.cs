using System;

namespace Itemcore.Service.Model
{
	[Serializable]
	public class ServiceItem
	{
		public Guid ID { get; set; }
		public Guid TemplateID { get; set; }
		public string Name { get; set; }
		public string TemplateName { get; set; }
		public string Icon { get; set; }
		public bool HasChildren { get; set; }

		public ServiceItem()
		{
			this.ID = Guid.Empty;
			this.TemplateID = Guid.Empty;
			this.Name = "";
			this.TemplateName = "";
			this.Icon = "";
			this.HasChildren = false;
		}
	}
}