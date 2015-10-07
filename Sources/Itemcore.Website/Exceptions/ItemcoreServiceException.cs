using System;

namespace Itemcore.Service.Exceptions
{
	[Serializable]
	public class ItemcoreServiceException : Exception
	{
		public ItemcoreServiceException() : base()
		{
		}

		public ItemcoreServiceException(string message) : base(message)
		{
		}

		public ItemcoreServiceException(string message, Exception innerException)
			: base(message, innerException)
		{
		}
	}
}
