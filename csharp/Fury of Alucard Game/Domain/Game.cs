using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Fury_of_Alucard.Domain.Characters;
using Fury_of_Alucard.Domain.Characters.Hunters;
using Fury_of_Alucard.Domain.Paths;
using Fury_of_Alucard.Common;

namespace Fury_of_Alucard.Domain
{
	/// <summary>
	/// the game class contains the information of a game.
	/// </summary>
	public class Game
	{
		public Map Map { get; private set; }

		public List<ACharacter> Characters { get; private set; }

		public int Turn { get; set; }

		public TimeToken Time { get; set; }

		public Game()
		{
			Map = new Map();

			// populate characters
			Characters = new List<ACharacter>();
			Characters.Add(TypeFactory.CreateAutoNotifierInstance<Alucard>());
			Characters.Add(TypeFactory.CreateAutoNotifierInstance<AbrahamVanHelsing>());
			Characters.Add(TypeFactory.CreateAutoNotifierInstance<JohnSeward>());
			Characters.Add(TypeFactory.CreateAutoNotifierInstance<LordGodalming>());
			Characters.Add(TypeFactory.CreateAutoNotifierInstance<MinaHarker>());
		}

		public List<ALocation> GetReachableCities(ACharacter c)
		{
			List<ALocation> result = new List<ALocation>();
			if (c.Position == null)
			{
				foreach (ALocation loc in Map.Locations)
				{
					if (!IsAllowedToGoTo(c, loc))
					{
						// cannot go there
					}
					else if (!result.Contains(loc))
					{
						result.Add(loc);
					}
				}
			}
			else
			{
				foreach (APath path in Map.Streets)
				{
					if (path.From == c.Position)
					{
						if (!IsAllowedToGoWith(c, path))
						{
							// cannot go there
						}
						else if (!IsAllowedToGoTo(c, path.To))
						{
							// cannot go there
						}
						else if (!result.Contains(path.To))
						{
							result.Add(path.To);
						}
					}
				}
			}
			return result;
		}

		private bool IsAllowedToGoWith(ACharacter c, APath path)
		{
			if ((c as Alucard) != null)
			{
				if ((path as ARail) != null)
				{
					// cannot move to st joseph
					return false;
				}
			}
			return true;
		}

		private bool IsAllowedToGoTo(ACharacter c, ALocation aLocation)
		{
			if ((c as Alucard) != null)
			{
				if (aLocation == Map.StJosephStMary)
				{
					// cannot move to st joseph
					return false;
				}
			}
			return true;
		}
	}
}
