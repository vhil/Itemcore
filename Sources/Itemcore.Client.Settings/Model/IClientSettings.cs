using System.Collections.Generic;
using System.ComponentModel;

namespace Itemcore.Client.Settings.Model
{
	public interface IClientSettings : INotifyPropertyChanged
	{
		string LastOpenedSolutionLocation { get; set; }
		ICollection<RecentSolution> RecentSolutions { get; }
		void AddRecentSolution(string filePath);
	}
}
