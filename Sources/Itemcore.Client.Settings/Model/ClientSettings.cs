using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.IO;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Xml.Serialization;
using Itemcore.Client.Settings.Annotations;

namespace Itemcore.Client.Settings.Model
{
	[Serializable]
	[XmlRoot("clientSettings")]
	public class ClientSettings : IClientSettings
	{
		public ClientSettings()
		{
			this.LastOpenedSolutionLocation = string.Empty;
		}

		[XmlElement("lastSolutionLocation")]
		public string LastOpenedSolutionLocation { get; set; }

		[XmlIgnore]
		public IEnumerable<IRecentSolution> RecentSolutions { get; set; }

		public void AddRecentSolution(string filePath)
		{
			var fileInfo = new FileInfo(filePath);
			var solutionName = fileInfo.Name.Replace(fileInfo.Extension, string.Empty);
			var directory = fileInfo.Directory;

			var path = fileInfo.FullName;

			var newSolution = new RecentSolution(solutionName, path);

			if (recentSolutions.Any(x => x.Path == path))
			{
				var existingSolution = recentSolutions.First(x => x.Path == path);
				recentSolutions.Remove(existingSolution);
			}

			recentSolutions.Insert(0, newSolution);

			if (directory != null)
			{
				this.LastOpenedSolutionLocation = directory.FullName;
			}

			this.RemoveNotExistingSolutions();

			recentSolutions = recentSolutions.OrderByDescending(x => x.Timestamp).ToList();

			this.OnRecentSlotionListChanged("RecentSolutions");
		}

		[XmlArray("recentSolutions")]
		[XmlArrayItem("solution")]
		public List<RecentSolution> recentSolutions
		{
			get { return this.RecentSolutions as List<RecentSolution>; }
			set { this.RecentSolutions = value; }
		}

		private void RemoveNotExistingSolutions()
		{
			var notExisting = recentSolutions.Where(x => !File.Exists(x.Path)).ToArray();
			foreach (var toRemove in notExisting)
			{
				recentSolutions.Remove(toRemove);
			}
		}

		public event PropertyChangedEventHandler PropertyChanged;

		[NotifyPropertyChangedInvocator]
		protected virtual void OnRecentSlotionListChanged([CallerMemberName] string propertyName = null)
		{
			var handler = PropertyChanged;
			if (handler != null) handler(this, new PropertyChangedEventArgs(propertyName));
		}
	}
}
