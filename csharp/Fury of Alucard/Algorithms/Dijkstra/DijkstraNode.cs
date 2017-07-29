using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Fury_of_Alucard.Algorithms.Dijkstra
{
	public class DijkstraNode<T>
	{
		public T Value { get; private set; }

		public DijkstraNode(T value)
		{
			Value = value;
		}

		public override string ToString()
		{
			if(Value != null)
				return Value.ToString();
			return base.ToString();
		}
	}
}
