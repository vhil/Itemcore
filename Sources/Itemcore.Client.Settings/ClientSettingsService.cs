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
		private object serializationSyncRoot = new object();

		private readonly ILoggingService LoggingService;
		private IClientSettings clientSettings;

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
			lock (serializationSyncRoot)
			{
				if (clientSettings == null)
				{
					var filePath = this.AppSettingsFilePath;
					if (!File.Exists(filePath))
					{
						return new ClientSettings();
					}

					using (var fs = new FileStream(filePath, FileMode.Open, FileAccess.Read, FileShare.Read))
					{
						var serializer = new XmlSerializer(typeof(ClientSettings));
						clientSettings = serializer.Deserialize(fs) as ClientSettings;
					}

				}

				return clientSettings ?? new ClientSettings();;
			}
		}

		public void SaveClientSettings()
		{
			lock (serializationSyncRoot)
			{

				var filePath = this.AppSettingsFilePath;

				using (var fs = new FileStream(filePath, FileMode.Truncate, FileAccess.Write, FileShare.Write))
				{
					var serializer = new XmlSerializer(typeof(ClientSettings));
					serializer.Serialize(fs, clientSettings);
				}
			}
		}
	}
}
