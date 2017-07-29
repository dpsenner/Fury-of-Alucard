using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Media;

namespace Fury_of_Alucard.Domain
{
	public abstract class ALocation
	{
		/// <summary>
		/// the name of the city.
		/// </summary>
		public string Name { get; set; }

		public double X { get; set; }

		public double Y { get; set; }

		public Color Color { get; set; }

		public List<Point> Points { get; set; }

		public virtual bool IsHighlighted { get; set; }

		public virtual bool IsSelectable { get; set; }

		public virtual List<ACharacter> Characters { get; set; }

		public ALocation(string name)
		{
			Name = name;
			Characters = new List<ACharacter>();
		}

		public override string ToString()
		{
			return Name;
		}
	}
}
