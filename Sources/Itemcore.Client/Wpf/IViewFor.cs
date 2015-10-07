namespace Itemcore.Client.Wpf
{
	public interface IViewFor<TViewModel> where TViewModel : BaseViewModel
	{
		TViewModel ViewModel { get; set; }
	}
}
