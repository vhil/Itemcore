using Itemcore.Client.Views.UserControls;
using Itemcore.Client.Wpf;

namespace Itemcore.Client.Controls
{
	/// <summary>
	/// Interaction logic for RecentSolutions.xaml
	/// </summary>
	public partial class RecentSolutions : IViewFor<RecentSolutionsViewModel>
	{
		public RecentSolutions()
		{
			this.ViewModel = ServiceLocator.Container.GetInstance<RecentSolutionsViewModel>();

			this.DataContext = this.ViewModel;

			InitializeComponent();

			RecentSolutionsListBox.ItemsSource = this.ViewModel.SolutionList;
		}

		public RecentSolutionsViewModel ViewModel { get; set; }
	}
}
