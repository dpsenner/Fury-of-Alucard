using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Remoting
{
	public class RemoteProcessor : IDisposable
	{
		private RemoteMessagePipe Pipe;

		private ARemoteActionHandler Handler;

		public event Action<RemoteProcessor, Exception> ExceptionCaught;

		public RemoteProcessor(RemoteMessagePipe pipe, ARemoteActionHandler handler)
		{
			Handler = handler;
			Pipe = pipe;
			pipe.MessageReceived += pipe_MessageReceived;
			pipe.ExceptionCaught += pipe_ExceptionCaught;
		}

		void pipe_ExceptionCaught(RemoteMessagePipe pipe, Exception ex)
		{
			Console.WriteLine("{0} is down, exception caught: {1}", pipe, ex);
			if (ExceptionCaught != null)
				ExceptionCaught(this, new Exception("See inner exception.", ex));
		}

		void pipe_MessageReceived(RemoteMessagePipe pipe, RemoteMessage msg)
		{
			// got a message, parse message and invoke method on handler
			try
			{
				Handler.Handle(pipe, msg.Action, msg.Args);
			}
			catch (Exception ex)
			{
				if (ExceptionCaught != null)
					ExceptionCaught(this, new Exception("See inner exception.", ex));
			}
		}

		public void Do(string action, params object[] args)
		{
			RemoteMessage msg = new RemoteMessage(action, args);
			Pipe.SendMessage(msg);
		}

		public override string ToString()
		{
			return Pipe.ToString();
		}

		#region IDisposable Members

		public void Dispose()
		{
			Pipe.MessageReceived -= pipe_MessageReceived;
			Pipe.ExceptionCaught -= pipe_ExceptionCaught;
			Pipe.Dispose();
		}

		#endregion
	}
}
