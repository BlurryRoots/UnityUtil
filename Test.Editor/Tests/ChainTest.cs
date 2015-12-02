using System;
using System.Collections.Generic;
using NUnit.Framework;
using BlurryRoots.Procedural;
using BlurryRoots.Procedural.Implementation;
using BlurryRoots.Storage;
using UnityEngine;

namespace Test.Editor {

	[TestFixture]
	public class ChainTest {

		[Test]
		public void ChainSerialization () {
			var generator = new PrimitveGenerator (PrimitiveType.Sphere, 12);
			var generator2 = new PrimitveGenerator (PrimitiveType.Cube, 234702);

			var chain = new Chain<List<GameObject>> ()
				.Link (generator)
				.Link (generator2)
				;

			var serialized = new Base64DeSerializer<Chain<List<GameObject>>> (chain);
			Assert.IsNotEmpty (serialized.Serialized);
			Assert.AreEqual (chain, serialized.Data);
			var byteSize = serialized.Serialized.Length * sizeof (Char);
			Console.WriteLine ("Serialized data is " + byteSize + " bytes.");

			var deserialized = new Base64DeSerializer<Chain<List<GameObject>>> (serialized.Serialized);
			Assert.AreEqual (serialized.Serialized, deserialized.Serialized);
			Assert.AreEqual (serialized.Data.Elements.Count, deserialized.Data.Elements.Count);

			var deserializedGenerator = (PrimitveGenerator)deserialized.Data.Elements[0];
			Assert.AreEqual (generator.Amount, deserializedGenerator.Amount);
			Assert.AreEqual (generator.Type, deserializedGenerator.Type);

			var deserializedGenerator2 = (PrimitveGenerator)deserialized.Data.Elements[1];
			Assert.AreEqual (generator2.Amount, deserializedGenerator2.Amount);
			Assert.AreEqual (generator2.Type, deserializedGenerator2.Type);
		}

		[Test] // TODO: can i do something to speed up deserialization (ca 35MB for 421337 PrimitiveGenerators in ca 21s)
		public void MassTest () {
			var generatorCount = 421337u;
			var chain = new Chain<List<GameObject>> ();

			for (var i = 0u; i < generatorCount; ++i) {
				chain.Link (new PrimitveGenerator (PrimitiveType.Cylinder, i + 1u));
			}

			var serialized = new Base64DeSerializer<Chain<List<GameObject>>> (chain);
			Assert.IsNotEmpty (serialized.Serialized);
			Assert.AreEqual (chain, serialized.Data);
			var byteSize = serialized.Serialized.Length * sizeof (Char);
			Console.WriteLine ("Serialized data is " + byteSize + " bytes.");

			var deserialized = new Base64DeSerializer<Chain<List<GameObject>>> (serialized.Serialized);
			Assert.AreEqual (serialized.Serialized, deserialized.Serialized);
			Assert.AreEqual (serialized.Data.Elements.Count, deserialized.Data.Elements.Count);
		}

	}

}