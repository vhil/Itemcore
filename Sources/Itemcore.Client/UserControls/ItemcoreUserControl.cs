using System.Windows.Controls;
using Itemcore.Client.ViewModels;
using Itemcore.Logging;

namespace Itemcore.Client.UserControls
{
	public class ItemcoreUserControl<TViewModel> : UserControl where TViewModel : BaseViewModel
	{
		protected readonly TViewModel ViewModel;
		protected readonly ILoggingService LoggingService;

		protected ItemcoreUserControl(ILoggingService loggingService, TViewModel viewViewModel)
		{
			this.ViewModel = viewViewModel;
			this.LoggingService = loggingService;
		}
	}
}
