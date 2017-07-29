using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Remoting;

namespace Fury_of_Alucard.Remoting
{
	public class ClientActionHandler : ARemoteActionHandler, IDisposable
	{
		private RemoteSocket transport;
		private RemoteMessagePipe messagePipe;
		private RemoteProcessor processor;

		public ClientActionHandler(string serverhost = "localhost")
		{
			transport = new RemoteClientSocket(serverhost, Config.PORT);
			messagePipe = new RemoteMessagePipe(transport);
			processor = new RemoteProcessor(messagePipe, this);
		}

		public override void Handle(RemoteMessagePipe sender, string method, params object[] args)
		{
			switch (method)
			{
				case "Echo":
					if(HandleEcho != null)
						HandleEcho();
					break;
				case "MoveCharacter":
					if (HandleMoveCharacter != null)
						HandleMoveCharacter((string)args[0], (string)args[1], (double)args[2], (double)args[3]);
					break;
			}
		}

		private string ToString(object[] args)
		{
			StringBuilder result = new StringBuilder();
			bool first = true;
			foreach (object arg in args)
			{
				if (!first)
					result.Append(", ");
				first = false;
				result.AppendFormat("{0}", arg);
			}
			return result.ToString();
		}

		public void Dispose()
		{
			processor.Dispose();
			messagePipe.Dispose();
			transport.Dispose();
		}

		public void DoEcho()
		{
			Console.WriteLine("DoEcho()");
			processor.Do("Echo");
		}
		public event Action HandleEcho;

		public void DoGetCharacterLocations()
		{
			processor.Do("GetCharacterLocations");
		}

		public void DoMoveCharacter(string character, string location, double xoffset, double yoffset)
		{
			processor.Do("MoveCharacter", character, location, xoffset, yoffset);
		}
		public event Action<string, string, double, double> HandleMoveCharacter;

	}
}
