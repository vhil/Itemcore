using Itemcore.Client.Project.Model;

namespace Itemcore.Client.Project
{
	public interface IItemcoreProjectService
	{
		IItemcoreSolution LoadSolution(string filePath);
	}
}
