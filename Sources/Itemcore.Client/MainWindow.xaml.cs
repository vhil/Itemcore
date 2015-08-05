using System.ComponentModel;
using System.Windows;
using Itemcore.Client.UserControls;
using Itemcore.Client.ViewModels;
using Itemcore.Logging;

namespace Itemcore.Client
{
	/// <summary>
	/// Interaction logic for MainWindow.xaml
	/// </summary>
	public partial class MainWindow
	{
		public MainWindow(ILoggingService loggingService, MainWindowViewModel viewModel)
			: base(loggingService, viewModel)
		{
			InitializeComponent();

			this.GrdMainPanel.Children.Add(new RecentSolutions(this.LoggingService, new RecentSolutionsViewModel(viewModel.ClientSettings)));
		}

		protected override void OnClosing(CancelEventArgs e)
		{
			this.ViewModel.SaveSettings();
			base.OnClosing(e);
		}

		private void OpenSolution(object sender, RoutedEventArgs e)
		{
			this.ViewModel.LoadSolution();
		}

		private void ExitApplication(object sender, RoutedEventArgs e)
		{
			this.ViewModel.SaveSettings();
			this.Close();
		}
	}
}
