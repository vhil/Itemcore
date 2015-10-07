namespace Itemcore.Client.Wpf
{
	public class DialogUserControl<TViewModel, TModel> : ItemcoreUserControl<TViewModel> where TViewModel : BaseViewModel
	{
		protected DialogUserControl(TViewModel viewViewModel) 
			: base(viewViewModel)
		{
		}

		public event DialogCancel Cancelled;
		public event Success Succeed;

		public delegate void DialogCancel();
		public delegate void Success(TModel model);

		protected virtual void OnCancelled()
		{
			var handler = Cancelled;
			if (handler != null) handler();	
		}

		protected virtual void OnSucceed(TModel model)
		{
			var handler = Succeed;
			if (handler != null) handler(model);
		}
	}
}
