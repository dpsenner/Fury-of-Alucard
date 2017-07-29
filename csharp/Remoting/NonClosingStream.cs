using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;

namespace Remoting
{
	public class NonClosingStream : Stream
	{
		private Stream BaseStream;

		public NonClosingStream(Stream baseStream)
		{
			BaseStream = baseStream;
		}

		public override bool CanRead
		{
			get { return BaseStream.CanRead; }
		}

		public override bool CanSeek
		{
			get { return BaseStream.CanSeek; }
		}

		public override bool CanWrite
		{
			get { return BaseStream.CanWrite; }
		}

		public override void Flush()
		{
			BaseStream.Flush();
		}

		public override long Length
		{
			get { return BaseStream.Length; }
		}

		public override long Position
		{
			get
			{
				return BaseStream.Position;
			}
			set
			{
				BaseStream.Position = Position;
			}
		}

		public override int Read(byte[] buffer, int offset, int count)
		{
			return BaseStream.Read(buffer, offset, count);
		}

		public override long Seek(long offset, SeekOrigin origin)
		{
			return BaseStream.Seek(offset, origin);
		}

		public override void SetLength(long value)
		{
			BaseStream.SetLength(value);
		}

		public override void Write(byte[] buffer, int offset, int count)
		{
			BaseStream.Write(buffer, offset, count);
		}

		public override void Close()
		{
			// do not close base stream
		}
	}
}
