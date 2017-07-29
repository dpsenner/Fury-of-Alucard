using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Fury_of_Alucard.Domain.Locations
{
	public abstract class ACity : ALocation
	{
		public LocationToken Location { get; set; }

		public ACity(string name, LocationToken token)
			: base(name)
		{
			Location = token;
		}
	}
}
