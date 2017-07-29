using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Fury_of_Alucard.Domain
{
	public class Player
	{
		/// <summary>
		/// the name of this player.
		/// </summary>
		public string Name { get; set; }

		/// <summary>
		/// a list of characters played by this player.
		/// </summary>
		public List<ACharacter> Characters { get; set; }

		public Player()
		{
			Characters = new List<ACharacter>();
		}
	}
}
