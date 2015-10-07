using System;

namespace Itemcore.Service.Model
{
	[Serializable]
	public class VersionInfo
	{
		public int MajorVersion;
		public int MinorVersion;
		public int FileBuild;
		public int FileRevision;
	}
}
