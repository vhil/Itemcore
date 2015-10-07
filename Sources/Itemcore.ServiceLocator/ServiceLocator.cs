using SimpleInjector;

namespace Itemcore
{
	public static class ServiceLocator
	{
		private static Container _instance;

		public static void SetContainer(Container container)
		{
			_instance = container;
		}

		public static Container Container
		{
			get { return _instance; }
		}
	}
}
