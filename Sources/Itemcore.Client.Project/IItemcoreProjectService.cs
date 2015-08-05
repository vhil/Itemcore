using Itemcore.Client.Project.Model;

namespace Itemcore.Client.Project
{
	public interface IItemcoreProjectService
	{
		IItemcoreSolution LoadSolution(string filePath);
		IItemcoreProject LoadProject(string filePath);
		IItemcoreSolution CreateSolution(string name, string baseDirectory);
		IItemcoreSolution CreateDefaultSolution(string name, string baseDirectory);
		IItemcoreProject CreateProject(IItemcoreSolution solution, string name);
		void SaveSolution(IItemcoreSolution solution);
		void SaveProject(IItemcoreProject project);
	}
}
