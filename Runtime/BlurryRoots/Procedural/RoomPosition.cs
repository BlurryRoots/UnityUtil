using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using BlurryRoots.Randomizer;

namespace BlurryRoots {
	namespace Procedural {

		[System.Serializable]
		public struct RoomPosition {

			public int X;
			public int Y;
			public int Z;

			public RoomPosition (RoomPosition old)
				: this (old.X, old.Y, old.Z) {
				//
			}

			public RoomPosition (int x, int y, int z) {
				this.X = x;
				this.Y = y;
				this.Z = z;
			}

			public override bool Equals (object obj) {
				if (this.GetType () != obj.GetType ()) {
					return false;
				}

				var b = (RoomPosition)obj;
				return this == b;
			}

			public override int GetHashCode () {
				// pick two primes
				// start by assigning p1 to hash
				var hash = 17;

				// incement has by its product with p2 plus the
				// has code of the component to hash
				unchecked { // allows for overflow
					hash = hash * 32 + this.X.GetHashCode ();
					hash = hash * 32 + this.Y.GetHashCode ();
					hash = hash * 32 + this.Z.GetHashCode ();
				}

				return hash;
			}

			public override string ToString () {
				return "{x:" + this.X + ", y:" + this.Y + ", z:" + this.Z + "}";
			}

			public static bool operator == (RoomPosition a, RoomPosition b) {
				return a.X == b.X
					&& a.Y == b.Y
					&& a.Z == b.Z
					;
			}

			public static bool operator != (RoomPosition a, RoomPosition b) {
				return !(a == b);
			}

			public static RoomPosition operator + (RoomPosition a, RoomPosition b) {
				var c = new RoomPosition (
					a.X + b.X,
					a.Y + b.Y,
					a.Z + b.Z
				);

				return c;
			}

			public static RoomPosition operator - (RoomPosition a, RoomPosition b) {
				var c = new RoomPosition (
					a.X - b.X,
					a.Y - b.Y,
					a.Z - b.Z
				);

				return c;
			}

			public static readonly RoomPosition Zero = new RoomPosition ();
			public static readonly RoomPosition Up = new RoomPosition (0, 1, 0);
			public static readonly RoomPosition North = new RoomPosition (0, 0, 1);
			public static readonly RoomPosition East = new RoomPosition (1, 0, 0);
			public static readonly RoomPosition South = new RoomPosition (0, 0, -1);
			public static readonly RoomPosition West = new RoomPosition (-1, 0, 0);
			public static readonly RoomPosition Down = new RoomPosition (0, -1, 0);


			public enum Directions {
				None = 0,
				Up = 1,
				North = 2,
				East = 4,
				South = 8,
				West = 16,
				Down = 32,
			}

			public static RoomPosition Neighbour (RoomPosition pos, Directions direction) {
				RoomPosition neighbour;

				switch (direction) {
					case Directions.None:
						neighbour = pos;
						break;
					case Directions.Up:
						neighbour = pos + RoomPosition.Up;
						break;
					case Directions.North:
						neighbour = pos + RoomPosition.North;
						break;
					case Directions.East:
						neighbour = pos + RoomPosition.East;
						break;
					case Directions.South:
						neighbour = pos + RoomPosition.South;
						break;
					case Directions.West:
						neighbour = pos + RoomPosition.West;
						break;
					case Directions.Down:
						neighbour = pos + RoomPosition.Down;
						break;

					default:
						throw new System.Exception ("Something went horribly wrong :/");
				}

				return neighbour;
			}

			public static Directions AreNeighbours (RoomPosition a, RoomPosition b) {
				if (Neighbour (a, Directions.Up) == b) {
					return Directions.Up;
				}				

				if (Neighbour (a, Directions.North) == b) {
					return Directions.North;
				}

				if (Neighbour (a, Directions.East) == b) {
					return Directions.East;
				}

				if (Neighbour (a, Directions.South) == b) {
					return Directions.South;
				}

				if (Neighbour (a, Directions.West) == b) {
					return Directions.West;
				}

				if (Neighbour (a, Directions.Down) == b) {
					return Directions.Down;
				}

				return Directions.None;
			}

		}

		public static class RoomBuilder {

			public static List<RoomPosition> TryFindPath (IRandomNumberGenerator rng, RoomPosition current, int minDist, RoomPosition target) {
				var horizontalTarget = new RoomPosition (target.X, 0, target.Z);
				List<RoomPosition> horizontalMap = null;
				while (null == horizontalMap) {
					horizontalMap = TryFindPathHorizontal (rng, current, minDist, horizontalTarget, new List<RoomPosition> ());
				}
								
				var last = horizontalMap[horizontalMap.Count - 1];
				if (last == target) {
					return horizontalMap;
				}

				var nextVertical = (last.Y < target.Y)
					? RoomPosition.Neighbour (last, RoomPosition.Directions.Up)
					: RoomPosition.Neighbour (last, RoomPosition.Directions.Down)
					;
				var distLeft = minDist - horizontalMap.Count;
				List<RoomPosition> verticalMap = null;
				while (null == verticalMap) {
					verticalMap = TryFindPathVertical (rng, nextVertical, distLeft, target, new List<RoomPosition> (horizontalMap));
				}

				return verticalMap;
			}

			public static List<RoomPosition> TryFindPathVertical (IRandomNumberGenerator rng, RoomPosition current, int minDist, RoomPosition target, List<RoomPosition> map) {
				if (map.Contains (current)) {
					return null;
				}

				map.Add (current);

				if (0 >= minDist && (current == target)) {
					return map;
				}

				RoomPosition next;

				// do this on a flat plane
				var dir = rng.Range (1, 4);
				switch (dir) {
					case 1:
						next = RoomPosition.Neighbour (current, RoomPosition.Directions.Up);
						break;
					case 2:
						next = RoomPosition.Neighbour (current, RoomPosition.Directions.East);
						break;
					case 3:
						next = RoomPosition.Neighbour (current, RoomPosition.Directions.Down);
						break;
					case 4:
						next = RoomPosition.Neighbour (current, RoomPosition.Directions.West);
						break;
					default:
						throw new System.Exception ("This is spartaaaaa!");
				}

				return TryFindPathVertical (rng, next, minDist - 1, target, map);
			}

			public static List<RoomPosition> TryFindPathHorizontal (IRandomNumberGenerator rng, RoomPosition current, int minDist, RoomPosition target, List<RoomPosition> map) {
				if (map.Contains (current)) {
					return null;
				}

				map.Add (current);
				
				if (0 >= minDist && (current == target)) {
					return map;
				}

				// do this on a flat plane
				RoomPosition next;
				var dir = rng.Range (1, 4);
				switch (dir) {
					case 1:
						next = RoomPosition.Neighbour (current, RoomPosition.Directions.North);
						break;
					case 2:
						next = RoomPosition.Neighbour (current, RoomPosition.Directions.East);
						break;
					case 3:
						next = RoomPosition.Neighbour (current, RoomPosition.Directions.South);
						break;
					case 4:
						next = RoomPosition.Neighbour (current, RoomPosition.Directions.West);
						break;
					default:
						throw new System.Exception ("This is spartaaaaa!");
				}

				return TryFindPathHorizontal (rng, next, minDist - 1, target, map);
			}

		}

	}
}

