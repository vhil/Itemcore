using System.Windows;
using Itemcore.Client.Project;
using Itemcore.Client.Settings;
using Itemcore.Client.Views.UserControls;
using Itemcore.Client.Views.Windows;
using Itemcore.Logging;
using Itemcore.Logging.Log4Net;
using Reactive.Bindings;
using SimpleInjector;

namespace Itemcore.Client
{
	/// <summary>
	/// Interaction logic for App.xaml
	/// </summary>
	public partial class App : Application
	{
		protected override void OnStartup(StartupEventArgs e)
		{
			base.OnStartup(e);

			// Initialize UIDispatcherScheduler
			UIDispatcherScheduler.Initialize();

			var container = InitializeContainer();

			ServiceLocator.SetContainer(container);

			var log = ServiceLocator.Container.GetInstance<ILoggingService>();
			log.Info("Application Start.", this);
		}

		private static Container InitializeContainer()
		{
			var container = new Container();

			container.Register(
					typeof(ILoggingService),
					() => new LoggingService(new ILoggingProvider[] { new Log4NetLoggingProvider() }),
					Lifestyle.Singleton);

			container.Register<IClientSettingsService, ClientSettingsService>(Lifestyle.Singleton);
			container.Register<IProjectFactory, ProjectFactory>(Lifestyle.Singleton);

			container.Register<MainWindowViewModel>();
			container.Register<RecentSolutionsViewModel>();
			container.Register<CreateSolutionViewModel>();

			container.Verify();

			return container;
		}
	}
}
