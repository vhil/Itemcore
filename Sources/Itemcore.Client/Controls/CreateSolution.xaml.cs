using System.Windows;
using Itemcore.Client.Project;
using Itemcore.Client.Views.UserControls;

namespace Itemcore.Client.Controls
{
	/// <summary>
	/// Interaction logic for CreateSolutionUserControl.xaml
	/// </summary>
	public partial class CreateSolution
	{
		protected readonly IProjectFactory projectFactory;

		public CreateSolution()
			: base(ServiceLocator.Container.GetInstance<CreateSolutionViewModel>())
		{
			this.projectFactory = ServiceLocator.Container.GetInstance<IProjectFactory>();
			InitializeComponent();
		}

		private void Cancel(object sender, RoutedEventArgs e)
		{
			this.OnCancelled();
		}

		private void Submit(object sender, RoutedEventArgs e)
		{
			var solution = this.projectFactory.CreateDefaultSolution(this.TbxSolutionName.Text, this.TbxSolutionPath.Text);
			this.OnSucceed(solution);
		}
	}
}
