using System;
using System.Collections.Generic;
using NUnit.Framework;
using BlurryRoots.Randomizer;
using BlurryRoots.Procedural;

namespace Test.Editor {

	[TestFixture]
	public class RoomPositionTest {

		static readonly int Seed = 1337;
		static readonly int Seed2 = 133742;
		static readonly int ExpectedDistance = 6;
		static readonly RoomPosition Start = new RoomPosition (0, 0, 0);
		static readonly RoomPosition Target = new RoomPosition (-2, 2, 1);

		[Test]
		public void Neigbours () {
			var startToTargetDirection = RoomPosition.AreNeighbours (Start, Target);
			Assert.AreEqual (RoomPosition.Directions.None, startToTargetDirection);

			var targetDown = RoomPosition.Neighbour (Target, RoomPosition.Directions.Down);
			var targetDirection = RoomPosition.AreNeighbours (targetDown, Target);
			Assert.AreEqual (RoomPosition.Directions.Up, targetDirection);
		}

		[Test]
		public void TestPath () {
			var rng = new UniformRandomNumberGenerator (Seed);
			// node adjecent to tarteg is already 

			List<RoomPosition> path = RoomBuilder.TryFindPath (rng, Start, 1, Target);
			Assert.AreEqual (ExpectedDistance, path.Count);
		}

		[Test]
		public void TestEqualPaths () {
			List<RoomPosition> path1, path2;
			{
				var rng = new UniformRandomNumberGenerator (Seed);
				path1 = RoomBuilder.TryFindPath (rng, Start, 1, Target);
			}
			{
				var rng = new UniformRandomNumberGenerator (Seed2);
				path2 = RoomBuilder.TryFindPath (rng, Start, 1, Target);
			}

			// two different seeds, should yield two different paths
			Assert.AreEqual (path1.Count, path2.Count);
			Assert.AreNotEqual (path1, path2);
		}

	}

}
