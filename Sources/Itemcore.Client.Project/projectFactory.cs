using System.IO;
using System.Xml.Serialization;
using Itemcore.Client.Project.Model;

namespace Itemcore.Client.Project
{
	public class ProjectFactory : IProjectFactory
	{
		public ISolution LoadSolution(string filePath)
		{
			if (!File.Exists(filePath))
			{
				return null;
			}

			ISolution solution;

			using (var fs = new FileStream(filePath, FileMode.Open, FileAccess.Read, FileShare.Read))
			{
				var serializer = new XmlSerializer(typeof(Solution));
				solution = serializer.Deserialize(fs) as ISolution;
			}

			return solution;
		}

		public IProject LoadProject(string filePath)
		{
			if (!File.Exists(filePath))
			{
				return null;
			}

			IProject project;

			using (var fs = new FileStream(filePath, FileMode.Open, FileAccess.Read, FileShare.Read))
			{
				var serializer = new XmlSerializer(typeof(Solution));
				project = serializer.Deserialize(fs) as IProject;
			}

			return project;
		}

		public ISolution CreateSolution(string name, string baseDirectory)
		{
			// Create baseDirectory if not exists
			if (!Directory.Exists(baseDirectory))
			{
				Directory.CreateDirectory(baseDirectory);
			}

			var solutionFilePath = Path.Combine(baseDirectory, name + ".icsln");

			var solution = new Solution
			{
				Name = name,
				BaseDirectory = baseDirectory,
				SolutionFilePath = solutionFilePath
			};

			this.SaveSolution(solution);

			return solution;
		}

		public ISolution CreateDefaultSolution(string name, string baseDirectory)
		{
			var solution = this.CreateSolution(name, baseDirectory);

			var masterProjectName = "Itemcore." + name + ".Master";

			var project = this.CreateProject(solution, masterProjectName);

			project.SitecoreDatabaseName = "master";

			this.SaveProject(project);
			this.SaveSolution(solution);

			return solution;
		}

		public IProject CreateProject(ISolution solution, string name)
		{
			var solutionProject = new SolutionProject
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

			var project = new Model.Project
			{
				ProjectBaseDirectory = projectBaseDirectory,
				ProjectFilePath = projectFilePath
			};

			this.SaveProject(project);
			this.SaveSolution(solution);

			return project;
		}

		public void SaveSolution(ISolution solution)
		{
			using (var fs = new FileStream(solution.SolutionFilePath, FileMode.Truncate, FileAccess.Write, FileShare.Write))
			{
				var serializer = new XmlSerializer(typeof(Solution));
				serializer.Serialize(fs, solution);
			}
		}

		public void SaveProject(IProject project)
		{
			using (var fs = new FileStream(project.ProjectFilePath, FileMode.Truncate, FileAccess.Write, FileShare.Write))
			{
				var serializer = new XmlSerializer(typeof(Model.Project));
				serializer.Serialize(fs, project);
			}
		}
	}
}
