using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Fury_of_Alucard.Domain.Paths
{
	public class Ship : APath
	{
		public Ship(ALocation from, ALocation to)
			: base(from, to)
		{
		}
	}
}
