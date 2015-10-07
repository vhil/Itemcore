using System.Collections.Generic;

namespace Itemcore.Client.Project.Model
{
	public interface ISolution
	{
		string BaseDirectory { get; set; }
		string SolutionFilePath { get; set; }
		string Name { get; set; }
		IEnumerable<ISolutionProject> Projects { get; set; }
		void AddProject(ISolutionProject project);
	}
}
