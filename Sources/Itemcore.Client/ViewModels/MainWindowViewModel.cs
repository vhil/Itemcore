using System;
using System.ComponentModel;
using System.IO;
using Itemcore.Client.Settings;
using Itemcore.Client.Settings.Model;
using Microsoft.Win32;

namespace Itemcore.Client.ViewModels
{
	public class MainWindowViewModel : BaseViewModel
	{
		private readonly IClientSettingsService ClientSettingsService;
		private IClientSettings _clientSettings;

		public MainWindowViewModel(IClientSettingsService clientSettingsService)
		{
			this.ClientSettingsService = clientSettingsService;
			this.ClientSettings.PropertyChanged += ClientSettingsOnPropertyChanged;
		}

		private void ClientSettingsOnPropertyChanged(object sender, PropertyChangedEventArgs propertyChangedEventArgs)
		{
			if (propertyChangedEventArgs.PropertyName == "RecentSolutions")
			{

			}
		}

		public IClientSettings ClientSettings	
		{
			get { return _clientSettings ?? (_clientSettings = this.ClientSettingsService.GetClientSettings()); }
		}

		public void SaveSettings()
		{
			this.ClientSettingsService.SaveClientSettings(this.ClientSettings);
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
