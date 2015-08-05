using System;
using Itemcore.Client.Project;
using Itemcore.Client.Settings;
using Itemcore.Client.ViewModels.Windows;
using Itemcore.Logging;
using Itemcore.Logging.Log4Net;
using SimpleInjector;

namespace Itemcore.Client
{
	static class Program
	{
		[STAThread]
		static void Main()
		{
			var container = InitializeContainer();

			ServiceLocator.SetContainer(container);

			RunApplication(container);
		}

		private static Container InitializeContainer()
		{
			var container = new Container();

			container.Register(
					typeof(ILoggingService),
					() => new LoggingService(new ILoggingProvider[] {new Log4NetLoggingProvider() }),
					Lifestyle.Singleton);

			container.Register<IClientSettingsService, ClientSettingsService>(Lifestyle.Singleton);
			container.Register<IItemcoreProjectService, ItemcoreProjectService>(Lifestyle.Singleton);

			container.Register<MainWindow>();
			container.Register<MainWindowViewModel>();

			container.Verify();

			return container;
		}

		private static void RunApplication(Container container)
		{
			var log = container.GetInstance<ILoggingService>();
			log.Info("Application Start.", typeof(Program));

			try
			{
				var app = new App();
				var mainWindow = ServiceLocator.Current.GetInstance<MainWindow>();
				app.Run(mainWindow);
			}
			catch (Exception ex)
			{
				log.Error("Application error.", typeof(Program), ex);
			}
		}
	}
}
