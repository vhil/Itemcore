using Itemcore.Client.Project;
using Itemcore.Client.Settings;
using Itemcore.Client.Settings.Model;
using Itemcore.Client.Wpf;
using Microsoft.Win32;

namespace Itemcore.Client.Views.Windows
{
	public class MainWindowViewModel : BaseViewModel
	{
		private readonly IClientSettingsService ClientSettingsService;
		private readonly IProjectFactory projectFactory;
		private IClientSettings _clientSettings;

		public MainWindowViewModel(IClientSettingsService clientSettingsService, IProjectFactory projectFactory)
		{
			this.ClientSettingsService = clientSettingsService;
			this.projectFactory = projectFactory;
		}

		public IClientSettings ClientSettings	
		{
			get { return _clientSettings ?? (_clientSettings = this.ClientSettingsService.GetClientSettings()); }
		}

		public void SaveSettings()
		{
			this.ClientSettingsService.SaveClientSettings();
		}

		public void LoadSolution()
		{
			var dialog = new OpenFileDialog();

			var show = dialog.ShowDialog();

			if (show.HasValue && show.Value)
			{
				var filePath = dialog.FileName;
				this.ClientSettings.AddRecentSolution(filePath);
			}
		}
	}
}
