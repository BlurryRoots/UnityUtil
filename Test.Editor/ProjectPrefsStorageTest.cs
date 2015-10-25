using System;
using System.Collections.Generic;
using NUnit.Framework;
using BlurryRoots.Editor;

namespace Test.Editor {

	[TestFixture]
	public class ProjectPrefsStoargeTest {

		const string Key = "Hanswurst";
		const string Key2 = "890234890234€µKäsestraße\\счастли́вый^/.txt";

		[Test]
		public void SetGetBool () {
			var storage = new ProjectPrefsStorage ();
			var expected = true;

			storage.SetBool (Key, expected);
			{
				var actual = storage.GetBool (Key);
				Assert.AreEqual (expected, actual);
			}

			storage.SetBool (Key, expected && false);
			{
				var actual = storage.GetBool (Key);
				Assert.AreEqual (expected && false, actual);
			}
		}

		[Test]
		public void SetGetInt () {
			var storage = new ProjectPrefsStorage ();
			var expected = 133742;

			storage.SetInt (Key, expected);
			{
				var actual = storage.GetInt (Key);
				Assert.AreEqual (expected, actual);
			}

			storage.SetInt (Key, expected + 1);
			{
				var actual = storage.GetInt (Key);
				Assert.AreEqual (expected + 1, actual);
			}
		}

		[Test]
		public void SetGetFloat () {
			var storage = new ProjectPrefsStorage ();
			var expected = ((float)Math.Sqrt (5f) + 1f) / 2f;

			storage.SetFloat (Key, expected);
			{
				var actual = storage.GetFloat (Key);
				Assert.AreEqual (expected, actual);
			}

			storage.SetFloat (Key, expected + 1);
			{
				var actual = storage.GetFloat (Key);
				Assert.AreEqual (expected + 1, actual);
			}
		}

		[Test]
		public void SetGetString () {
			var storage = new ProjectPrefsStorage ();
			var expected = "счастли́вый";

			storage.SetString (Key, expected);
			{
				var actual = storage.GetString (Key);
				Assert.AreEqual (expected, actual);
			}

			storage.SetString (Key, expected + 1);
			{
				var actual = storage.GetString (Key);
				Assert.AreEqual (expected + 1, actual);
			}
		}

		[Test]
		public void OddKeyBool () {
			var storage = new ProjectPrefsStorage ();

			var expectedBool = true;
			storage.SetBool (Key2, expectedBool);

			var b = storage.GetBool (Key);
			Assert.AreEqual (ProjectPrefsStorage.Defaults.Bool, b);

			var b2 = storage.GetBool (Key2);
			Assert.AreEqual (expectedBool, b2);
		}

		[Test]
		public void OddKeyFloat () {
			var storage = new ProjectPrefsStorage ();

			var expectedFloat = 3.1415f;
			storage.SetFloat (Key2, expectedFloat);

			var f = storage.GetFloat (Key);
			Assert.AreEqual (ProjectPrefsStorage.Defaults.Float, f);

			var f2 = storage.GetFloat (Key2);
			Assert.AreEqual (expectedFloat, f2);
		}

		[Test]
		public void OddKeyInt () {
			var storage = new ProjectPrefsStorage ();

			var expectedInt = 4096;
			storage.SetInt (Key2, expectedInt);

			var i = storage.GetInt (Key);
			Assert.AreEqual (ProjectPrefsStorage.Defaults.Int, i);

			var i2 = storage.GetInt (Key2);
			Assert.AreEqual (expectedInt, i2);
		}
		
		[Test]
		public void OddKeyString () {
			var storage = new ProjectPrefsStorage ();

			var expectString = "Hans im Glück!";
			storage.SetString (Key2, expectString);

			var s = storage.GetString (Key);
			Assert.AreEqual (ProjectPrefsStorage.Defaults.String, s);

			var s2 = storage.GetString (Key2);
			Assert.AreEqual (expectString, s2);
		}

	}

}