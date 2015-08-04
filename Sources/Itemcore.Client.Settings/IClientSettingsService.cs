using Itemcore.Client.Settings.Model;

namespace Itemcore.Client.Settings
{
	public interface IClientSettingsService
	{
		IClientSettings GetClientSettings();
		void SaveClientSettings();
	}
}
