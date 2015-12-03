using System;
using System.Collections.Generic;
using NUnit.Framework;
using BlurryRoots.Procedural;
using BlurryRoots.Procedural.Implementation;
using BlurryRoots.Storage;
using UnityEngine;
using System.Runtime.Serialization;

namespace Test.Editor {

	[TestFixture] public
	class ChainTest {

		[Test] public
		void Base64SerializationTest () {
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

		[Test]
		[ExpectedException (typeof (SerializationException))]
		public void Base64NonSerializable () {
			var g = (Mathf.Sqrt (5) + 1) / 2.0f;
			var r = new Rect (23, 42, g, g - 1f);

			var serialized = new Base64DeSerializer<Rect> (r);
		}

		[Test] public
		void Base64SerializationMassTest () {
			// TODO: can i do something to speed up deserialization
			// (ca 35MB for 421337 PrimitiveGenerators in ca 21s)
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

		[Test]
		public void XMLNonSerializableTest () {
			var v = new Vector2 (12, 13);
			var s = new XMLDeSerializer<Vector2> (v);
			var d = new XMLDeSerializer<Vector2> (s.Serialized);
			Assert.AreEqual (v, d.Data);
		}

		[Test] public
		void XMLSerializationTest () {
			var position = new Vector3 ();
			position.x = (Mathf.Sqrt (5) + 1.0f) / 2.0f;

			var serialized = new XMLDeSerializer<Vector3> (position);
			Assert.AreEqual (serialized.Data, position);
			Assert.IsNotEmpty (serialized.Serialized);

			var deserialized = new XMLDeSerializer<Vector3> (serialized.Serialized);
			Assert.AreEqual (serialized.Serialized, deserialized.Serialized);
			Assert.AreEqual (position, deserialized.Data);
		}
		

		[Test] public
		void XMLerializationMassTest () {
			var generatorCount = 421337u;
			var positions = new List<Vector3> ();

			for (var i = 0u; i < generatorCount; ++i) {
				positions.Add (new Vector3 (i, i, i));
			}

			var serialized = new XMLDeSerializer<List<Vector3>> (positions);
			Assert.AreEqual (serialized.Data, positions);
			Assert.IsNotEmpty (serialized.Serialized);

			var deserialized = new XMLDeSerializer<List<Vector3>> (serialized.Serialized);
			Assert.AreEqual (serialized.Serialized, deserialized.Serialized);
			Assert.AreEqual (new Vector3 (1337, 1337, 1337), positions[1337]);
		}

	}

}