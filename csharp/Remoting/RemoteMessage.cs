using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using System.Xml;
using System.Xml.Serialization;

namespace Remoting
{
	[Serializable]
	public class RemoteMessage
	{
		public static readonly string BEGIN = "<BEGIN<";

		public static readonly string END = ">END>";

		private static readonly XmlSerializer Serializer = new XmlSerializer(typeof(RemoteMessage));

		public string Action { get; set; }

		public object[] Args { get; set; }

		public RemoteMessage()
		{
		}

		public RemoteMessage(string action, params object[] args)
			: this()
		{
			Action = action;
			Args = args;
		}

		public void Serialize(Stream stream)
		{
			using (NonClosingStream ncs = new NonClosingStream(stream))
			{
				using (StreamWriter sw = new StreamWriter(ncs))
				{
					StringBuilder sb = new StringBuilder();
					using (XmlWriter writer = XmlWriter.Create(sb))
					{
						Serializer.Serialize(writer, this);
					}
					string msg = Convert.ToBase64String(Encoding.UTF8.GetBytes(sb.ToString()));
					sw.WriteLine(msg);
					System.Threading.Thread.Sleep(10);
				}
			}
		}

		public static RemoteMessage Deserialize(Stream stream)
		{
			using (NonClosingStream ncs = new NonClosingStream(stream))
			{
				using (StreamReader sw = new StreamReader(ncs))
				{
					// got a begin
					string tmp = sw.ReadLine();
					// now we are at end, deserialize it
					using (XmlReader reader = XmlReader.Create(new StringReader(Encoding.UTF8.GetString(Convert.FromBase64String(tmp.ToString())))))
					{
						return (RemoteMessage)Serializer.Deserialize(reader);
					}
				}
			}
		}

		public override string ToString()
		{
			return string.Format("{0} (... {1})", Action, Args.Length);
		}
	}
}
