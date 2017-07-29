using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Fury_of_Alucard.Domain
{
	public abstract class APath
	{
		public ALocation From { get; set; }
		public ALocation To { get; set; }

		public APath(ALocation from, ALocation to)
			: base()
		{
			From = from;
			To = to;
		}
	}
}
