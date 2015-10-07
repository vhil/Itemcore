using System.ComponentModel;
using System.Windows;
using Itemcore.Client.Controls;
using Itemcore.Client.Project;
using Itemcore.Client.Project.Model;
using Itemcore.Client.Views.Windows;

namespace Itemcore.Client
{
	/// <summary>
	/// Interaction logic for MainWindow.xaml
	/// </summary>
	public partial class MainWindow
	{
		protected readonly IProjectFactory projectFactory;

		public MainWindow()
			: base(ServiceLocator.Container.GetInstance<MainWindowViewModel>())
		{
			this.projectFactory = ServiceLocator.Container.GetInstance<IProjectFactory>();
			this.InitializeComponent();

			this.LoadRecentSolutionsView();
		}

		private void LoadRecentSolutionsView()
		{
			this.GrdMainPanel.Children.Clear();
			this.GrdMainPanel.Children.Add(new RecentSolutions());
		}

		#region Menu events

		private void OpenSolution(object sender, RoutedEventArgs e)
		{
			this.ViewModel.LoadSolution();
		}

		private void CreateSolution(object sender, RoutedEventArgs e)
		{
			var createSolutionUserControl = new CreateSolution();

			createSolutionUserControl.Cancelled += LoadRecentSolutionsView;
			createSolutionUserControl.Succeed += CreateSolutionUserControlOnSucceed;
			this.GrdMainPanel.Children.Clear();

			this.GrdMainPanel.Children.Add(createSolutionUserControl);
		}

		private void CreateSolutionUserControlOnSucceed(ISolution model)
		{
				
		}

		#endregion

		protected override void OnClosing(CancelEventArgs e)
		{
			this.ViewModel.SaveSettings();
			base.OnClosing(e);
		}

		private void ExitApplication(object sender, RoutedEventArgs e)
		{
			this.Close();
		}

		
	}
}
