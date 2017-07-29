using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Fury_of_Alucard.Domain;
using Fury_of_Alucard.Domain.Characters;
using System.IO;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows;

namespace Fury_of_Alucard
{
	public class GameManager
	{
		public Game Game { get; private set; }

		public List<Player> Players { get; private set; }

		public GameManager()
		{
			Game = new Game();
			Players = new List<Player>();
		}

		public bool Start()
		{
			// check if dracula is played by one player only
			bool alucardPlayed = false;
			foreach (Player player in Players)
			{
				foreach (ACharacter c in player.Characters)
				{
					if (c is Alucard)
					{
						alucardPlayed = true;
						// he cannot have others
						if (player.Characters.Count != 1)
						{
							return false;
						}
					}
				}
			}
			if (!alucardPlayed)
			{
				return false;
			}
			// check if all characters will be played by someone
			foreach (ACharacter c in Game.Characters)
			{
				bool found = false;
				foreach (Player player in Players)
				{
					foreach (ACharacter c2 in player.Characters)
					{
						if (c == c2)
						{
							found = true;
						}
					}
				}
				if (!found)
				{
					return false;
				}
			}
			return true;
		}

		public void RandomizeCharacterLocations()
		{
			// reset highlighting for all locations
			foreach (ALocation loc in Game.Map.Locations)
			{
				loc.IsHighlighted = false;
				loc.IsSelectable = false;
			}

			Random r = new Random();
			foreach (ACharacter c in Game.Characters)
			{
				// remove highlighting
				c.IsHighlighted = false;

				// pick a random nearby location
				List<ALocation> reachables = Game.GetReachableCities(c);
				if (reachables.Count > 0)
				{
					Game.Map.MoveCharacter(c, reachables[r.Next(0, reachables.Count - 1)]);
				}
				else
				{
					Game.Map.MoveCharacter(c, Game.Map.Locations[r.Next(0, Game.Map.Locations.Count - 1)]);
				}
			}
		}

		public void InitializeMap()
		{
			Dictionary<string, Color> mapSegmentInfos = GetMapSegmentInfos();

			// get the pixels
			Color[][] bmp = GetImageBytes();

			// determine center point of the segment info
			foreach (ALocation location in Game.Map.Locations)
			{
				// check if this location needs to be calculated
				if (location.X != 0.0 && location.Y != 0.0)
					continue;

				// check this location is defined in the segment infos
				if (!mapSegmentInfos.ContainsKey(location.Name))
					continue;

				Console.WriteLine("Finding center point for city '{0}'", location.Name);

				// assign color
				location.Color = mapSegmentInfos[location.Name];

				// find boundary points of this location by examining the color
				List<Point> boundaryPoints = new List<Point>();
				double mass = 0.0;
				for (int y = 0; y < bmp.Length; y++)
				{
					for (int x = 0; x < bmp[y].Length; x++)
					{
						if (IsEqual(location.Color, bmp[y][x]))
						{
							// we are within the shape
							// now we have to determine if this is at the corner
							// that is the case if either one of the surrounding
							// pixels is not the color we desire
							bool isBoundary = false;
							bool hasNeighbour = false;
							if (x == 0)
								isBoundary = true;
							else if (x + 1 == bmp[y].Length)
								isBoundary = true;
							else if (y == 0)
								isBoundary = true;
							else if (y + 1 == bmp.Length)
								isBoundary = true;
							else
							{
								for (int x2 = x - 1; x2 <= x + 1; x2++)
								{
									for (int y2 = y - 1; y2 <= y + 1; y2++)
									{
										if (x2 == x)
											continue;
										if (y2 == y)
											continue;

										Color candidate = bmp[y2][x2];
										if (!IsEqual(location.Color, candidate))
										{
											isBoundary = true;
										}
										else
										{
											hasNeighbour = true;
										}
									}
								}
							}
							double relX = (double)x / (double)bmp[y].Length;
							double relY = (double)y / (double)bmp.Length;
							if (hasNeighbour)
							{
								if (isBoundary)
								{
									boundaryPoints.Add(new Point(relX, relY));
								}
								else
								{
									// calculate as center point
									location.X += relX;
									location.Y += relY;
									mass += 1;
								}
							}
						}
					}
				}
				if (mass > 0)
				{
					// relativize the location
					location.X = location.X / mass;
					location.Y = location.Y / mass;
				}
				// not found in image
				if (boundaryPoints.Count == 0)
				{
					Console.WriteLine(" ===> not found in image!");
					continue;
				}
				else
				{
					Console.WriteLine(" ===> got {0} boundary points", boundaryPoints.Count);
				}
				// sort the boundaryPoints into a polygon shape
				List<Point> polygon = new List<Point>();

				// pick a random start point
				polygon.Add(RemoveAt(boundaryPoints, 0));

				// now add the nearest point to the list until there are no more items
				while (boundaryPoints.Count > 0)
 				{
					// get current
					Point current = polygon.Last();
					// pick first one
					int candidate = 0;
					double distance = Math.Abs((current - boundaryPoints[candidate]).LengthSquared);
					// find nearest other point
					for (int i = 1; i < boundaryPoints.Count; i++)
					{
						double distance2 = Math.Abs((current - boundaryPoints[i]).LengthSquared);
						if (distance2 < distance)
						{
							distance = distance2;
							candidate = i;
						}
					}
					// put that one
					Point x = RemoveAt(boundaryPoints, candidate);
					if ((current - x).LengthSquared < 0.0005 && (current-x).LengthSquared > 0.000005)
					{
						polygon.Add(x);
					}
				}

				// remove every third pixel that is too close to his predecessor
				for (int i = 0; i < polygon.Count - 1; i++)
				{
					if ((polygon[i + 1] - polygon[i]).LengthSquared < 0.00001)
					{
						// remove polygon[i + 1]
						polygon.RemoveAt(i + 1);
						i--;
					}
				}

				// add the point collection to the location
				location.Points = polygon;
			}
		}

		private unsafe Color[][] GetImageBytes()
		{
			using (Stream stream = ResourceUtils.GetResourceStream(@"Fury_of_Alucard.MapSegmented.png"))
			{
				System.Drawing.Bitmap bmp = new System.Drawing.Bitmap(stream);
				// use width and height
				int width = bmp.Width;
				int height = bmp.Height;
				// open the png with a decoder
				Color[][] result = new Color[height][];
				for (int y = 0; y < height; y++)
				{
					result[y] = new Color[width];
					for (int x = 0; x < width; x++)
					{
						System.Drawing.Color c = bmp.GetPixel(x, y);
						result[y][x] = Color.FromArgb(c.A, c.R, c.G, c.B);
					}
				}
				return result;
			}
		}

		private Point RemoveAt(List<Point> boundaryPoints, int p)
		{
			Point result = boundaryPoints[p];
			boundaryPoints.RemoveAt(p);
			return result;
		}

		private static void GetPixel(int stride, byte[] pixels, int x, int y, out byte b, out byte g, out byte r, out byte a)
		{
			int i = y * stride + x * 4;
			b = pixels[i];
			g = pixels[i + 1];
			r = pixels[i + 2];
			a = pixels[i + 3];
		}

		private bool IsEqual(Color a, Color b)
		{
			if (Color.Equals(a, b))
			{
				return true;
			}
			return false;
		}

		private static Dictionary<string, Color> GetMapSegmentInfos()
		{
			Dictionary<string, Color> mapSegmentInfos = new Dictionary<string, Color>();
			// determine center points of locations relative to image size
			using (Stream segmentedMapInfo = ResourceUtils.GetResourceStream(@"Fury_of_Alucard.MapSegmented.info"))
			{
				// 
				using (StreamReader sr = new StreamReader(segmentedMapInfo))
				{
					string line = null;
					do
					{
						line = sr.ReadLine();
						// skip some items
						if (string.IsNullOrWhiteSpace(line))
							continue;
						if (line.StartsWith("#"))
							continue;
						// split the line
						string[] parts = line.Split('=', ',');
						// extract all portions
						string city = parts[0];
						Color c = Color.FromArgb(255, byte.Parse(parts[1]), byte.Parse(parts[2]), byte.Parse(parts[3]));
						mapSegmentInfos.Add(city, c);
					} while (line != null);
				}
			}
			return mapSegmentInfos;
		}
	}
}
