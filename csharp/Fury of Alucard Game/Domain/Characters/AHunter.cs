using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Fury_of_Alucard.Domain.Characters
{
	public abstract class AHunter : ACharacter
	{
		/// <summary>
		/// represents the current amount of bites this character has sustained.
		/// </summary>
		public int Bites { get; set; }

		/// <summary>
		/// the minimum amount of bites a character must have.
		/// </summary>
		public int MinBites { get; private set; }

		public AHunter(int totalBlood, int minBites, string name)
			: base(totalBlood, name)
		{
			MinBites = minBites;
			Bites = minBites;
		}
	}
}
