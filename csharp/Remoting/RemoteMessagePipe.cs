using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Net.Sockets;
using System.Threading;

namespace Remoting
{
	/// <summary>
	/// exchanges messages between two endpoints by using the specified socket.
	/// </summary>
	public class RemoteMessagePipe : IDisposable
	{
		private RemoteSocket TransportSocket { get; set; }

		private NetworkStream Transport { get; set; }

		public event Action<RemoteMessagePipe, RemoteMessage> MessageReceived;

		public event Action<RemoteMessagePipe, Exception> ExceptionCaught;

		private Queue<RemoteMessage> MessagesToSend = new Queue<RemoteMessage>();

		public RemoteMessagePipe(RemoteSocket transport)
			: this(transport.GetStream())
		{
			TransportSocket = transport;
		}

		public RemoteMessagePipe(NetworkStream transport)
		{
			Transport = transport;
			ThreadPool.QueueUserWorkItem((object state) =>
			{
				try
				{
					while (true)
					{
						// read message from stream
						RemoteMessage msg = ReadFromStream(Transport);
						if (MessageReceived != null)
							MessageReceived(this, msg);
					}
				}
				catch (Exception ex)
				{
					// may happen, ignore it
					if (ExceptionCaught != null)
						ExceptionCaught(this, ex);
				}
			});
		}

		public void SendMessage(RemoteMessage msg, bool autoflush = true)
		{
			MessagesToSend.Enqueue(msg);
			if (autoflush)
				FlushMessages();
		}

		public void FlushMessages()
		{
			while (MessagesToSend.Count > 0)
			{
				WriteToStream(Transport, MessagesToSend.Dequeue());
			}
		}

		private void WriteToStream(NetworkStream stream, RemoteMessage msg)
		{
			msg.Serialize(stream);
			stream.Flush();
		}

		private RemoteMessage ReadFromStream(NetworkStream stream)
		{
			return RemoteMessage.Deserialize(stream);
		}

		#region IDisposable Members

		public void Dispose()
		{
			Transport.Dispose();
		}

		#endregion

		public override string ToString()
		{
			return TransportSocket + " (" + GetType().Name + "#" + GetHashCode() + ")";
		}
	}
}
