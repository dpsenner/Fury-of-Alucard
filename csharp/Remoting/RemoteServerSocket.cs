using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Net.Sockets;

namespace Remoting
{
	public class RemoteServerSocket : RemoteSocket
	{
		private TcpClient Client;

		public RemoteServerSocket(TcpClient client)
		{
			Client = client;
		}

		public override NetworkStream GetStream()
		{
			return Client.GetStream();
		}

		public override void Dispose()
		{
			Client.Close();
		}

		public override string ToString()
		{
			return Client.Client.RemoteEndPoint.ToString();
		}
	}
}
