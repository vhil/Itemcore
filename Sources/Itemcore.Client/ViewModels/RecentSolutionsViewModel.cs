using Itemcore.Client.Settings.Model;

namespace Itemcore.Client.ViewModels
{
	public class RecentSolutionsViewModel : BaseViewModel
	{
		public readonly IClientSettings ClientSettings;

		public RecentSolutionsViewModel(IClientSettings clientSettings)
		{
			this.ClientSettings = clientSettings;
		}
	}
}
