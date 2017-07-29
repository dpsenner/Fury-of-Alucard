using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Fury_of_Alucard.Algorithms.Dijkstra
{
	public class DijkstraEdge<T>
	{
		public DijkstraNode<T> A { get; private set; }
		public DijkstraNode<T> B { get; private set; }
		public double Distance { get; private set; }

		public DijkstraEdge(DijkstraNode<T> a, DijkstraNode<T> b, double distance)
		{
			A = a;
			B = b;
			Distance = distance;
		}
	}
}
