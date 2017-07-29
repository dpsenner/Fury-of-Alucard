using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Fury_of_Alucard.Algorithms.AntColony
{
	public class AntColony<T>
	{
		private List<AntColonyNode<T>> Nodes;

		private Dictionary<AntColonyNode<T>, Dictionary<AntColonyNode<T>, double>> Distance;

		private double MinDistance = double.MaxValue;

		private double MaxDistance = double.MinValue;

		private double PheromoneValue;

		public AntColony(List<T> nodes, Func<T, T, double> DistanceFunc)
		{
			// store nodes
			Nodes = new List<AntColonyNode<T>>();
			foreach (T v in nodes)
			{
				Nodes.Add(new AntColonyNode<T>(v));
			}

			InitializeDistances(DistanceFunc);

			foreach (AntColonyNode<T> start in Nodes)
			{
				Travel(start);
			}
		}

		public List<T> GetPath()
		{
			List<T> result = new List<T>();

			// start with the first one
			AntColonyNode<T> start = Nodes.First();
			Nodes.RemoveAt(0);
			result.Add(start.Value);

			// get the next nearest
			bool working = true;
			while(working)
			{
				double min=double.MaxValue;
				int p = 0;
				for (int i = 1; i < Nodes.Count; i++)
				{
					AntColonyNode<T> candidate = Nodes[p];
					double d=candidate.Pheromones - Distance[start][candidate];
					if (d > 0)
					{
						if (d < min)
						{
							p = i;
							min = d;
						}
					}
				}
				if (p != 0)
				{
					result.Add(Nodes[p].Value);
					Nodes.RemoveAt(p);
				}
			}

			return result;
		}

		private void InitializeDistances(Func<T, T, double> DistanceFunc)
		{
			Distance = new Dictionary<AntColonyNode<T>, Dictionary<AntColonyNode<T>, double>>();
			// calculate distance between all nodes
			foreach (AntColonyNode<T> a in Nodes)
			{
				Distance.Add(a, new Dictionary<AntColonyNode<T>, double>());
				foreach (AntColonyNode<T> b in Nodes)
				{
					double d = DistanceFunc(a.Value, b.Value);
					MinDistance = Math.Min(MinDistance, d);
					MaxDistance = Math.Max(MaxDistance, d);
					Distance[a].Add(b, d);
				}
			}

			PheromoneValue = (MaxDistance - MinDistance) / 2;
		}

		private void Travel(AntColonyNode<T> start)
		{
			List<AntColonyNode<T>> visited = new List<AntColonyNode<T>>();

			// place our ant on the first node
			AntColonyNode<T> current = start;
			visited.Add(current);

			// travel to the nearest node possible
			while (visited.Count < Nodes.Count)
			{
				AntColonyNode<T> nearest = GetNearest(Distance[current]);
				visited.Add(nearest);
				current = nearest;
			}

			// now travel backwards and place pheromones, the farther we travelled, the less pheromone we place
			double dist = PheromoneValue * visited.Count;
			foreach (AntColonyNode<T> v in visited)
			{
				v.Pheromones += dist;
				dist -= Math.Max(0, PheromoneValue);
			}
		}

		private AntColonyNode<T> GetNearest(Dictionary<AntColonyNode<T>, double> dict)
		{
			AntColonyNode<T> result = null;
			double distance = double.MaxValue;
			foreach (KeyValuePair<AntColonyNode<T>, double> kvp in dict)
			{
				if ((kvp.Value - kvp.Key.Pheromones) < distance)
				{
					result = kvp.Key;
					distance = kvp.Value;
				}
			}
			return result;
		}
	}
}
