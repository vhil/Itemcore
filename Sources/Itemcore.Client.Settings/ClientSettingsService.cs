using System.IO;
using System.Linq;
using System.Reflection;
using System.Xml.Serialization;
using Itemcore.Client.Settings.Model;
using Itemcore.Logging;

namespace Itemcore.Client.Settings
{
	public class ClientSettingsService : IClientSettingsService
	{
		private readonly ILoggingService LoggingService;

		public ClientSettingsService(ILoggingService loggingService)
		{
			this.LoggingService = loggingService;
		}

		public string AppSettingsFilePath
		{
			get
			{
				var relativeLocation = new FileInfo(Assembly.GetExecutingAssembly().Location).Directory.FullName;
				return Path.Combine(relativeLocation, "ItemcoreClientSettings.xml");
			}
		}

		public IClientSettings GetClientSettings()
		{
			var filePath = this.AppSettingsFilePath;
			if (!File.Exists(filePath))
			{
				return new ClientSettings();
			}
			IClientSettings clientSettings;
			using (var fs = new FileStream(filePath, FileMode.Open, FileAccess.Read, FileShare.Read))
			{
				var serializer = new XmlSerializer(typeof (ClientSettings));
				clientSettings = serializer.Deserialize(fs) as ClientSettings ?? new ClientSettings();
			}

			this.RemoveNotExistingSolutions(clientSettings);

			return clientSettings;
		}

		private void RemoveNotExistingSolutions(IClientSettings settings)
		{
			var notExisting = settings.RecentSolutions.Where(x => !File.Exists(x.Path)).ToArray();
			foreach (var toRemove in notExisting)
			{
				settings.RecentSolutions.Remove(toRemove);
			}
		}

		public void SaveClientSettings(IClientSettings clientSettings)
		{
			this.RemoveNotExistingSolutions(clientSettings);

			var filePath = this.AppSettingsFilePath;

			using (var fs = new FileStream(filePath, FileMode.OpenOrCreate, FileAccess.Write, FileShare.Write))
			{
				var serializer = new XmlSerializer(typeof(ClientSettings));
				serializer.Serialize(fs, clientSettings);
			}
		}
	}
}
