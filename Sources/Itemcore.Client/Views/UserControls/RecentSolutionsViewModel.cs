using System.ComponentModel;
using Itemcore.Client.Settings;
using Itemcore.Client.Settings.Model;
using Itemcore.Client.Wpf;
using Reactive.Bindings;

namespace Itemcore.Client.Views.UserControls
{
	public class RecentSolutionsViewModel : BaseViewModel
	{
		public ReactiveCollection<IRecentSolution> SolutionList = new ReactiveCollection<IRecentSolution>();
		public IClientSettings ClientSettings;

		public RecentSolutionsViewModel(IClientSettingsService clientSettingsService)
		{
			this.ClientSettings = clientSettingsService.GetClientSettings();
			this.ClientSettings.PropertyChanged += ClientSettingsOnPropertyChanged;
			this.ReloadRecentSolutions();
		}

		private void ClientSettingsOnPropertyChanged(object sender, PropertyChangedEventArgs propertyChangedEventArgs)
		{
			this.ReloadRecentSolutions();
		}

		private void ReloadRecentSolutions()
		{
			this.SolutionList.ClearOnScheduler();

			foreach (var rs in this.ClientSettings.RecentSolutions)
			{
				this.SolutionList.AddOnScheduler(rs);
			}
		}
	}
}
