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
				var targetDistance = target - current;

				var revMap = new List<RoomPosition> ();
				revMap.Add (targetDistance);

				while (RoomPosition.Zero != targetDistance) {
					var hasNext = false;
					var considerX = 0 != targetDistance.X;
					var considerY = 0 != targetDistance.Y;
					var considerZ = 0 != targetDistance.Z;
					var min = 1;

					min += considerX ? 0 : 33;
					min += considerY ? 0 : 33;
					min += considerZ ? 0 : 33;

					var diceRoll = rng.Range (min, 100);

					if (considerX && 77 < diceRoll) {
						targetDistance.X += (int)Mathf.Sign (-targetDistance.X);
						hasNext = true;
					}
					else if (considerY && 33 < diceRoll) {
						targetDistance.Y += (int)Mathf.Sign (-targetDistance.Y);
						hasNext = true;
					}
					else if (considerZ && 0 < diceRoll) {
						targetDistance.Z += (int)Mathf.Sign (-targetDistance.Z);
						hasNext = true;
					}

					if (hasNext) {
						revMap.Add (targetDistance);
					}
				}

				revMap.Reverse ();

				return revMap;
			}

		}

	}
}

