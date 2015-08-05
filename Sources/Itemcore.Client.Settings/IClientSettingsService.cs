using Itemcore.Client.Settings.Model;

namespace Itemcore.Client.Settings
{
	public interface IClientSettingsService
	{
		string AppSettingsFilePath { get; }
		IClientSettings GetClientSettings();
		void SaveClientSettings(IClientSettings clientSettings);
	}
}
