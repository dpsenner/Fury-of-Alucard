using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Fury_of_Alucard.Algorithms.Dijkstra
{
	public class Dijkstra<T>
	{
		private List<DijkstraNode<T>> Nodes;
		private List<DijkstraEdge<T>> Edges;
		private List<DijkstraNode<T>> Basis;

		private Dictionary<DijkstraNode<T>, double> Distance;
		private Dictionary<DijkstraNode<T>, DijkstraNode<T>> Predecessor;

		public Dijkstra(List<DijkstraNode<T>> nodes, List<DijkstraEdge<T>> edges, DijkstraNode<T> start)
		{
			Nodes = nodes;
			Edges = edges;
			Basis = new List<DijkstraNode<T>>();
			Distance = new Dictionary<DijkstraNode<T>, double>();
			Predecessor = new Dictionary<DijkstraNode<T>, DijkstraNode<T>>();

			foreach (DijkstraNode<T> node in Nodes)
			{
				Predecessor.Add(node, null);
				Basis.Add(node);
				Distance.Add(node, double.MaxValue);
			}

			CalculateDistance(start);
		}

		public List<DijkstraNode<T>> GetPathTo(DijkstraNode<T> d)
		{
			List<DijkstraNode<T>> path = new List<DijkstraNode<T>>();

			path.Insert(0, d);

			while (Predecessor[d] != null)
			{
				d = Predecessor[d];
				path.Insert(0, d);
			}

			return path;
		}

		private void CalculateDistance(DijkstraNode<T> start)
		{
			Distance[start] = 0;

			while (Basis.Count > 0)
			{
				DijkstraNode<T> u = GetNodeWithSmallestDistance();
				if (u == null)
				{
					// we cannot continue
					Basis.Clear();
				}
				else
				{
					foreach (DijkstraNode<T> v in GetNeighbours(u))
					{
						double d = Distance[u] + GetDistanceBetween(u, v);
						if (d < Distance[v])
						{
							Distance[v] = d;
							Predecessor[v] = u;

						}
					}
				}
			}
		}

		private double GetDistanceBetween(DijkstraNode<T> u, DijkstraNode<T> v)
		{
			foreach (DijkstraEdge<T> e in Edges)
			{
				if (e.A.Equals(u) && e.B.Equals(v))
				{
					return e.Distance;
				}
			}

			return 0;
		}

		private IEnumerable<DijkstraNode<T>> GetNeighbours(DijkstraNode<T> u)
		{
			List<DijkstraNode<T>> neighbors = new List<DijkstraNode<T>>();

			foreach (DijkstraEdge<T> e in Edges)
			{
				if (e.A.Equals(u) && Basis.Contains(u))
				{
					neighbors.Add(e.B);
				}
			}

			return neighbors;
		}

		private DijkstraNode<T> GetNodeWithSmallestDistance()
		{
			double distance = double.MaxValue;
			DijkstraNode<T> smallest = null;

			foreach (DijkstraNode<T> n in Basis)
			{
				if (Distance[n] < distance)
				{
					distance = Distance[n];
					smallest = n;
				}
			}

			return smallest;
		}
	}
}
