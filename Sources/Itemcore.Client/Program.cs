using System;
using Itemcore.Client.ViewModels;
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

			RunApplication(container);
		}

		private static Container InitializeContainer()
		{
			var container = new Container();

			container.Register(
					typeof(ILoggingService),
					() => new LoggingService(new ILoggingProvider[] {new Log4NetLoggingProvider() }),
					Lifestyle.Singleton);

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
				var mainWindow = container.GetInstance<MainWindow>();
				app.Run(mainWindow);
			}
			catch (Exception ex)
			{
				log.Error("Error on application start.", typeof(Program), ex);
			}
		}
	}
}
