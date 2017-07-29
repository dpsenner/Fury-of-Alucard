using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Remoting;
using System.Threading;
using System.Net;

namespace Fury_of_Alucard_Server
{
	class Program
	{
		private static readonly ServerActionHandler ServerActionHandler = new ServerActionHandler();

		static void Main(string[] args)
		{
			using (RemoteServerSocketAcceptor acceptor = new RemoteServerSocketAcceptor(IPAddress.Any, Config.PORT))
			{
				acceptor.Accepted += new Action<RemoteServerSocket>(acceptor_Accepted);
				while (true)
				{
					Thread.Sleep(1000);
				}
			}
		}

		static void acceptor_Accepted(RemoteServerSocket clientSocket)
		{
			ThreadPool.QueueUserWorkItem((object state) =>
			{
				if (!ServerActionHandler.HasFreeSlots())
				{
					// drop this new connection
					Console.WriteLine("Got too many clients, refused connection request from: {0}", clientSocket);
					return;
				}
				// got a new connection, begin to receive messages
				using (RemoteMessagePipe clientPipe = new RemoteMessagePipe(clientSocket))
				{
					bool alive = true;
					clientPipe.ExceptionCaught += (RemoteMessagePipe pipe, Exception ex) =>
					{
						alive = false;
						ServerActionHandler.Unregister(clientPipe);
					};
					ServerActionHandler.Register(clientPipe);
					using (RemoteProcessor processor = new RemoteProcessor(clientPipe, ServerActionHandler))
					{
						while (alive)
						{
							// dispatch messages
							Thread.Sleep(1000);
						}
					}
				}
			});
		}
	}
}
