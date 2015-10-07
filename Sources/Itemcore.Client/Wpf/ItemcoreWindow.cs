using System.Windows;
using Itemcore.Logging;

namespace Itemcore.Client.Wpf
{
	public abstract class ItemcoreWindow<TViewModel> : Window where TViewModel : BaseViewModel
	{
		protected readonly TViewModel ViewModel;
		protected readonly ILoggingService LoggingService;

		protected ItemcoreWindow(TViewModel viewViewModel)
		{
			this.ViewModel = viewViewModel;
			this.LoggingService = ServiceLocator.Container.GetInstance<ILoggingService>();
			this.DataContext = this.ViewModel;
		}
	}
}