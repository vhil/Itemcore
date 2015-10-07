using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Itemcore.Client.Project.Model
{
	public interface ISolutionProject
	{
		string Name { get; set; }
		string RelativePath { get; set; }
	}
}
