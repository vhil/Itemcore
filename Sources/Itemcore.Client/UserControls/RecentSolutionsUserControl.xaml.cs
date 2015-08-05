using System.ComponentModel;
using Itemcore.Client.ViewModels;
using Itemcore.Client.ViewModels.UserControls;
using Itemcore.Logging;

namespace Itemcore.Client.UserControls
{
	/// <summary>
	/// Interaction logic for RecentSolutions.xaml
	/// </summary>
	public partial class RecentSolutionsUserControl
	{
		private ILoggingService loggingService;
		private RecentSolutionsViewModel recentSolutionsViewModel;

		public RecentSolutionsUserControl(ILoggingService loggingService, RecentSolutionsViewModel viewViewModel) 
			: base(loggingService, viewViewModel)
		{
			InitializeComponent();
			this.LoadRecentSolutions();
			this.ViewModel.ClientSettings.PropertyChanged += ClientSettingsOnPropertyChanged;
		}

		private void ClientSettingsOnPropertyChanged(object sender, PropertyChangedEventArgs propertyChangedEventArgs)
		{
			if (propertyChangedEventArgs.PropertyName == "RecentSolutions")
			{
				this.LoadRecentSolutions();
			}
		}

		public void LoadRecentSolutions()
		{
			lbContainer.ItemsSource = this.ViewModel.ClientSettings.RecentSolutions;
		}
	}
}
