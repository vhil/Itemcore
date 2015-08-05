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

		public static Container Current
		{
			get { return _instance; }
		}
	}
}
