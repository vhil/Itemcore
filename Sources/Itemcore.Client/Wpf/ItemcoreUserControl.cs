using System.Windows.Controls;

namespace Itemcore.Client.Wpf
{
	public class ItemcoreUserControl<TViewModel> : UserControl where TViewModel : BaseViewModel
	{
		protected readonly TViewModel ViewModel;

		protected ItemcoreUserControl(TViewModel viewViewModel)
		{
			this.ViewModel = viewViewModel;
			this.DataContext = this.ViewModel;
		}
	}
}
