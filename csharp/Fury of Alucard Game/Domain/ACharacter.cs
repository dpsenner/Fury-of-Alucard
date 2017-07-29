using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Fury_of_Alucard.Domain
{
	public abstract class ACharacter
	{
		/// <summary>
		/// the name of this character
		/// </summary>
		public string Name { get; private set; }

		/// <summary>
		/// sets the image path.
		/// </summary>
		public string Image { get; private set; }

		/// <summary>
		/// current amount of blood.
		/// </summary>
		public int Blood { get; set; }

		/// <summary>
		/// the total amount of blood this character can have.
		/// </summary>
		public int TotalBlood { get; private set; }

		/// <summary>
		/// marks if this character is highlighted.
		/// </summary>
		public virtual bool IsHighlighted { get; set; }

		/// <summary>
		/// the position of this characters on the map.
		/// </summary>
		public virtual ALocation Position { get; set; }

		public virtual double PositionOffsetX { get; set; }

		public virtual double PositionOffsetY { get; set; }
	
		public ACharacter(int totalBlood, string name)
		{
			Image = @"Resources\" + this.GetType().BaseType.Name + ".png";
			Name = name;
			Blood = totalBlood;
			TotalBlood = totalBlood;
		}
	}
}
