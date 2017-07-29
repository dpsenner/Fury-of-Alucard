using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Net.Sockets;

namespace Remoting
{
	public abstract class RemoteSocket : IDisposable
	{
		public abstract NetworkStream GetStream();

		#region IDisposable Members

		public abstract void Dispose();

		#endregion
	}
}
