using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Net.Sockets;
using System.Net;
using System.Threading;

namespace Remoting
{
	public class RemoteServerSocketAcceptor: IDisposable
	{
		private TcpListener Listener;

		private bool Accepting = true;

		public event Action<RemoteServerSocket> Accepted;

		public RemoteServerSocketAcceptor(IPAddress address, int port)
		{
			Listener = new TcpListener(address, port);
			Listener.Start();

			ThreadPool.QueueUserWorkItem(Accept);
		}

		private void Accept(object state)
		{
			while (Accepting)
			{
				TcpClient client = Listener.AcceptTcpClient();
				if(Accepted != null)
					Accepted.BeginInvoke(new RemoteServerSocket(client), null, null);
			}
		}

		#region IDisposable Members

		public void Dispose()
		{
			Accepting = false;
		}

		#endregion
	}
}
