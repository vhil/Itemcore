using System.Collections.Generic;

namespace Itemcore.Client.Project.Model
{
	public interface IItemcoreSolution
	{
		string BaseDirectory { get; set; }
		string SolutionFilePath { get; set; }
		string Name { get; set; }
		IEnumerable<IItemcoreSolutionProject> Projects { get; set; }
		void AddProject(IItemcoreSolutionProject project);
	}
}
