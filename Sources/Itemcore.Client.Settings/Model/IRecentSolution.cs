using System;

namespace Itemcore.Client.Settings.Model
{
	public interface IRecentSolution
	{
		DateTime Timestamp { get; set; }
		string Path { get; set; }
		string SolutionName { get; set; }
	}
}
