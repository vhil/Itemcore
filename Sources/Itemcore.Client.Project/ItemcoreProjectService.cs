using System.IO;
using System.Xml.Serialization;
using Itemcore.Client.Project.Model;

namespace Itemcore.Client.Project
{
	public class ItemcoreProjectService : IItemcoreProjectService
	{
		public IItemcoreSolution LoadSolution(string filePath)
		{
			if (!File.Exists(filePath))
			{
				return null;
			}

			IItemcoreSolution solution;

			using (var fs = new FileStream(filePath, FileMode.Open, FileAccess.Read, FileShare.Read))
			{
				var serializer = new XmlSerializer(typeof(ItemcoreSolution));
				solution = serializer.Deserialize(fs) as IItemcoreSolution;
			}

			return solution;
		}

		public IItemcoreProject LoadProject(string filePath)
		{
			if (!File.Exists(filePath))
			{
				return null;
			}

			IItemcoreProject project;

			using (var fs = new FileStream(filePath, FileMode.Open, FileAccess.Read, FileShare.Read))
			{
				var serializer = new XmlSerializer(typeof(ItemcoreSolution));
				project = serializer.Deserialize(fs) as IItemcoreProject;
			}

			return project;
		}

		public IItemcoreSolution CreateSolution(string name, string baseDirectory)
		{
			// Create baseDirectory if not exists
			if (!Directory.Exists(baseDirectory))
			{
				Directory.CreateDirectory(baseDirectory);
			}

			var solutionFilePath = Path.Combine(baseDirectory, name + ".icsln");

			var solution = new ItemcoreSolution
			{
				Name = name,
				BaseDirectory = baseDirectory,
				SolutionFilePath = solutionFilePath
			};

			this.SaveSolution(solution);

			return solution;
		}

		public IItemcoreSolution CreateDefaultSolution(string name, string baseDirectory)
		{
			var solution = this.CreateSolution(name, baseDirectory);

			var masterProjectName = "Itemcore." + name + ".Master";

			var project = this.CreateProject(solution, masterProjectName);

			project.SitecoreDatabaseName = "master";

			this.SaveProject(project);
			this.SaveSolution(solution);

			return solution;
		}

		public IItemcoreProject CreateProject(IItemcoreSolution solution, string name)
		{
			var solutionProject = new ItemcoreSolutionProject
			{
				Name = name,
				RelativePath = name + ".icproj",
			};

			solution.AddProject(solutionProject);

			var projectBaseDirectory = Path.Combine(solution.BaseDirectory, name);

			// Create baseDirectory if not exists
			if (!Directory.Exists(projectBaseDirectory))
			{
				Directory.CreateDirectory(projectBaseDirectory);
			}

			var projectFilePath = Path.Combine(projectBaseDirectory, solutionProject.RelativePath);

			var project = new ItemcoreProject
			{
				ProjectBaseDirectory = projectBaseDirectory,
				ProjectFilePath = projectFilePath
			};

			this.SaveProject(project);

			return project;
		}

		public void SaveSolution(IItemcoreSolution solution)
		{
			using (var fs = new FileStream(solution.SolutionFilePath, FileMode.OpenOrCreate, FileAccess.Write, FileShare.Write))
			{
				var serializer = new XmlSerializer(typeof(ItemcoreSolution));
				serializer.Serialize(fs, solution);
			}
		}

		public void SaveProject(IItemcoreProject project)
		{
			using (var fs = new FileStream(project.ProjectFilePath, FileMode.OpenOrCreate, FileAccess.Write, FileShare.Write))
			{
				var serializer = new XmlSerializer(typeof(ItemcoreProject));
				serializer.Serialize(fs, project);
			}
		}
	}
}
