using System.Windows;
using Itemcore.Client.ViewModels;
using Itemcore.Logging;

namespace Itemcore.Client.Windows
{
	public abstract class ItemcoreWindow<TViewModel> : Window where TViewModel : BaseViewModel
	{
		protected readonly TViewModel ViewModel;
		protected readonly ILoggingService LoggingService;

		protected ItemcoreWindow(ILoggingService loggingService, TViewModel viewViewModel)
		{
			this.ViewModel = viewViewModel;
			this.LoggingService = loggingService;
		}
	}
}