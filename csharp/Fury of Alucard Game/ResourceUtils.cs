using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Reflection;
using System.IO;
using System.Windows;

namespace Fury_of_Alucard
{
	public static class ResourceUtils
	{
		public static Stream GetResourceStream(string name)
		{
			return Assembly.GetExecutingAssembly().GetManifestResourceStream(name);
		}
	}
}
