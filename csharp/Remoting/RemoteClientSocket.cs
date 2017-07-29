using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Net.Sockets;

namespace Remoting
{
	public class RemoteClientSocket : RemoteSocket
	{
		private TcpClient client;

		public RemoteClientSocket(string host, int port)
		{
			client = new TcpClient(host, port);
		}

		public override NetworkStream GetStream()
		{
			return client.GetStream();
		}

		#region IDisposable Members

		public override void Dispose()
		{
			client.Close();
		}

		#endregion

		public override string ToString()
		{
			return client.Client.LocalEndPoint.ToString();
		}
	}
}
