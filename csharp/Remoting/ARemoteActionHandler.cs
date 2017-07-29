using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Remoting
{
	public abstract class ARemoteActionHandler
	{
		/// <summary>
		/// invokes a method on this handler.
		/// </summary>
		/// <param name="method"></param>
		/// <param name="args"></param>
		public abstract void Handle(RemoteMessagePipe sender, string method, params object[] args);
	}
}
