using System.Collections.Generic;
using System.ComponentModel;

namespace Itemcore.Client.Settings.Model
{
	public interface IClientSettings : INotifyPropertyChanged
	{
		string LastOpenedSolutionLocation { get; set; }
		IEnumerable<IRecentSolution> RecentSolutions { get; }
		void AddRecentSolution(string filePath);
	}
}
