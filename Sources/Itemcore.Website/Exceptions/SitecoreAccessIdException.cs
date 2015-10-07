using System;

namespace Itemcore.Service.Exceptions
{
	[Serializable]
	public class SitecoreAccessIdException : Exception
	{
		public SitecoreAccessIdException()
		{
		}

		public SitecoreAccessIdException(string message)
			:base(message)
		{
		}

		public SitecoreAccessIdException(string message, Exception innerException)
			: base(message, innerException)
		{
		}
	}
}
