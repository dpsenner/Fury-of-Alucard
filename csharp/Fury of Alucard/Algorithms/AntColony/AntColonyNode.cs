using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Fury_of_Alucard.Algorithms.AntColony
{
	public class AntColonyNode<T>
	{
		public T Value { get; private set; }

		public double Pheromones { get; set; }

		public AntColonyNode(T value)
		{
			Value = value;
			Pheromones = 0;
		}
	}
}
