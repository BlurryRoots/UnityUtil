using System;
using System.Collections.Generic;
using NUnit.Framework;
using BlurryRoots.Randomizer;
using BlurryRoots.Procedural;

namespace Test.Editor {

	[TestFixture]
	public class RoomPositionTest {

		static readonly int Seed = 1337;
		static readonly int ExpectedHorizontalDistance = 3;
		static readonly int ExpectedVerticalDistanceFromBelow = 2;
		static readonly int ExpectedVerticalDistanceFromTop = 2;
		static readonly RoomPosition Start = new RoomPosition (0, 0, 0);
		static readonly RoomPosition Target = new RoomPosition (1, 1, 1);

		[Test]
		public void Neigbours () {
			Assert.AreEqual (RoomPosition.Directions.None, RoomPosition.AreNeighbours (Start, Target));

			var targetDown = RoomPosition.Neighbour (Target, RoomPosition.Directions.Down);
			Assert.AreEqual (RoomPosition.Directions.Up, RoomPosition.AreNeighbours (targetDown, Target));
		}

		[Test]
		public void TestHorizontalPath () {
			var rng = new UniformRandomNumberGenerator (Seed);
			var horizontalTarget = new RoomPosition (Target.X, 0, Target.Z);
			List<RoomPosition> path = null;
			while (null == path)
				path = RoomBuilder.TryFindPathHorizontal (rng, Start, 1, horizontalTarget, new List<RoomPosition> ());

			Assert.AreEqual (ExpectedHorizontalDistance, path.Count);
		}

		[Test]
		public void TestVerticalPathFromBelow () {
			var rng = new UniformRandomNumberGenerator (Seed);
			var start = RoomPosition.Neighbour (Target, RoomPosition.Directions.Down);

			List<RoomPosition> path = null;
			while (null == path)
				path = RoomBuilder.TryFindPathVertical (rng, start, 1, Target, new List<RoomPosition> ());

			Assert.AreEqual (ExpectedVerticalDistanceFromBelow, path.Count);
		}

		[Test]
		public void TestVerticalPathFromTop () {
			var rng = new UniformRandomNumberGenerator (Seed);
			var start = RoomPosition.Neighbour (Target, RoomPosition.Directions.Up);

			List<RoomPosition> path = null;
			while (null == path)
				path = RoomBuilder.TryFindPathVertical (rng, start, 1, Target, new List<RoomPosition> ());

			Assert.AreEqual (ExpectedVerticalDistanceFromTop, path.Count);
		}

		[Test]
		public void TestPath () {
			var rng = new UniformRandomNumberGenerator (Seed);
			// node adjecent to tarteg is already 
			var expectedCount = ExpectedHorizontalDistance + (ExpectedVerticalDistanceFromBelow - 1);

			List<RoomPosition> path = RoomBuilder.TryFindPath (rng, Start, 1, Target);
			Assert.AreEqual (expectedCount, path.Count);
		}

	}

}
