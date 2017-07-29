using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Remoting;
using Fury_of_Alucard;
using Fury_of_Alucard.Domain;

namespace Fury_of_Alucard_Server
{
	class ServerActionHandler : ARemoteActionHandler
	{
		private readonly object lockObj = new object();

		private List<RemoteMessagePipe> Pipes = new List<RemoteMessagePipe>();

		private GameManager Manager;

		public ServerActionHandler()
			: base()
		{
			// initialize manager
			Manager = new GameManager();
			Manager.InitializeMap();
			Manager.RandomizeCharacterLocations();
		}

		public override void Handle(RemoteMessagePipe sender, string method, params object[] args)
		{
			Console.WriteLine("{0}{1}", method, ToString(args));
			lock (lockObj)
			{
				// inform other clients, if one client fails, remove it
				List<RemoteMessagePipe> toUnregister = new List<RemoteMessagePipe>();
				// handle method
				switch (method)
				{
					case "GetCharacterLocations":
						foreach (ACharacter c in Manager.Game.Characters)
						{
							sender.SendMessage(new RemoteMessage("MoveCharacter", c.Name, c.Position.Name, c.PositionOffsetX, c.PositionOffsetY), false);
						}
						try
						{
							sender.FlushMessages();
						}
						catch (Exception ex)
						{
							Console.WriteLine("Caught client exception at client {0}: {1}", sender, ex);
							toUnregister.Add(sender);
						}
						break;
					case "MoveCharacter":
						foreach (ACharacter c in Manager.Game.Characters)
						{
							if (c.Name == (string)args[0])
							{
								foreach (ALocation l in Manager.Game.Map.Locations)
								{
									if (l.Name == (string)args[1])
									{
										Manager.Game.Map.MoveCharacter(c, l);
										ForwardMessageToEveryone(method, args, toUnregister);
									}
								}
							}
						}
						break;
					default:
						// by default pipe the message to everyone else
						ForwardMessageToEveryone(method, args, toUnregister);
						break;
				}
				// unregister broken pipes
				foreach (RemoteMessagePipe pipe in toUnregister)
				{
					Unregister(pipe);
				}
			}
		}

		private void ForwardMessageToEveryone(string method, object[] args, List<RemoteMessagePipe> toUnregister)
		{
			foreach (RemoteMessagePipe pipe in Pipes)
			{
				try
				{
					pipe.SendMessage(new RemoteMessage(method, args));
				}
				catch (Exception ex)
				{
					Console.WriteLine("Caught client exception at client {0}: {1}", pipe, ex);
					toUnregister.Add(pipe);
				}
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
			return "(" + result.ToString() + ")";
		}

		public void Register(RemoteMessagePipe clientPipe)
		{
			lock (lockObj)
			{
				Pipes.Add(clientPipe);
			}
		}

		public void Unregister(RemoteMessagePipe clientPipe)
		{
			lock (lockObj)
			{
				Pipes.Remove(clientPipe);
			}
		}

		public bool HasFreeSlots()
		{
			lock (lockObj)
			{
				if (Pipes.Count >= 5)
				{
					return false;
				}
				return true;
			}
		}
	}
}
