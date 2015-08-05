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
			this.recentSolutions = new List<RecentSolution>();
		}

		[XmlElement("lastSolutionLocation")]
		public string LastOpenedSolutionLocation { get; set; }

		[XmlIgnore]
		public ICollection<RecentSolution> RecentSolutions
		{
			get { return recentSolutions; }
		}

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

			recentSolutions.Add(newSolution);

			if (directory != null)
			{
				this.LastOpenedSolutionLocation = directory.FullName;
			}

			recentSolutions = recentSolutions.OrderByDescending(x => x.Timestamp).Take(5).ToList();

			OnPropertyChanged("RecentSolutions");
		}

		[XmlArray("recentSolutions")]
		[XmlArrayItem("solution")]
		public List<RecentSolution> recentSolutions { get; set; }

		public event PropertyChangedEventHandler PropertyChanged;

		[NotifyPropertyChangedInvocator]
		protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
		{
			var handler = PropertyChanged;
			if (handler != null) handler(this, new PropertyChangedEventArgs(propertyName));
		}
	}
}
