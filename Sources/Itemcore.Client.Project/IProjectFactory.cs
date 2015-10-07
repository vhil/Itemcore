using Itemcore.Client.Project.Model;

namespace Itemcore.Client.Project
{
	public interface IProjectFactory
	{
		ISolution LoadSolution(string filePath);
		IProject LoadProject(string filePath);
		ISolution CreateSolution(string name, string baseDirectory);
		ISolution CreateDefaultSolution(string name, string baseDirectory);
		IProject CreateProject(ISolution solution, string name);
		void SaveSolution(ISolution solution);
		void SaveProject(IProject project);
	}
}
