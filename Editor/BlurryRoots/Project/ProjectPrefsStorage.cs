using System.Collections.Generic;

namespace BlurryRoots {
	namespace Editor {

		/// <summary>
		/// Storage backend for project preferences.
		/// </summary>
		public class ProjectPrefsStorage {

			/// <summary>
			/// Contains all default Values for storage functions.
			/// </summary>
			public static class Defaults {
				/// <summary>
				/// Default value for bool storage.
				/// </summary>
				public const bool Bool = default (bool);
				/// <summary>
				/// Default value for int storage.
				/// </summary>
				public const int Int = default (int);
				/// <summary>
				/// Default value for float storage.
				/// </summary>
				public const float Float = default (float);
				/// <summary>
				/// Default value for string storage.
				/// </summary>
				public const string String = default (string);
			}

			public int Count {
				get { return this.entries.Count; }
			}
								
			/// <summary>
			/// Removes all keys and values from the preferences. Use with caution.
			/// </summary>
			public void DeleteAll () {
				this.entries.Clear ();
			}

			/// <summary>
			/// Removes key and its corresponding value from the preferences.
			/// </summary>
			/// <param name="key">Key to remove.</param>
			public void DeleteKey (string key) {
				if (this.entries.ContainsKey (key)) {
					this.entries.Remove (key);
				}
			}

			/// <summary>
			/// Returns an array with copies of all entries currently contained in the storage.
			/// </summary>
			/// <returns>Array of all entries.</returns>
			public ProjectPrefsEntry[] GetAllEntries () {
				var count = this.entries.Values.Count;
				var entryArr = new ProjectPrefsEntry[count];
				var i = 0;

				foreach (var val in this.entries.Values) {
					entryArr[i++] = new ProjectPrefsEntry (val);
				}

				return entryArr;
			}

			/// <summary>
			/// Returns the value corresponding to key in the preference file if it exists.
			/// </summary>
			/// <param name="key">Key to retrive value for.</param>
			/// <param name="defaultValue">Default value to fallback if key is not contained in storage.</param>
			/// <returns></returns>
			public bool GetBool (string key, bool defaultValue = Defaults.Bool) {
				var val = defaultValue;
				var type = ProjectPrefsEntry.ValueType.Bool;

				if (this.entries.ContainsKey (key)
				 && type == this.entries[key].Type) {
					val = this.entries[key].Bool;
				}

				return val;
			}

			/// <summary>
			/// Returns the value corresponding to key in the preference file if it exists.
			/// </summary>
			/// <param name="key">Key to retrive value for.</param>
			/// <param name="defaultValue">Default value to fallback if key is not contained in storage.</param>
			/// <returns></returns>
			public int GetInt (string key, int defaultValue = Defaults.Int) {
				var val = defaultValue;
				var type = ProjectPrefsEntry.ValueType.Int;

				if (this.entries.ContainsKey (key)
				 && type == this.entries[key].Type) {
					val = this.entries[key].Int;
				}

				return val;
			}

			/// <summary>
			/// Returns the value corresponding to key in the preference file if it exists.
			/// </summary>
			/// <param name="key">Key to retrive value for.</param>
			/// <param name="defaultValue">Default value to fallback if key is not contained in storage.</param>
			/// <returns></returns>
			public float GetFloat (string key, float defaultValue = Defaults.Float) {
				var val = defaultValue;
				var type = ProjectPrefsEntry.ValueType.Float;

				if (this.entries.ContainsKey (key)
				 && type == this.entries[key].Type) {
					 val = this.entries[key].Float;
				}

				return val;
			}

			/// <summary>
			/// Returns the value corresponding to key in the preference file if it exists.
			/// </summary>
			/// <param name="key">Key to retrive value for.</param>
			/// <param name="defaultValue">Default value to fallback if key is not contained in storage.</param>
			/// <returns></returns>
			public string GetString (string key, string defaultValue = Defaults.String) {
				var val = defaultValue;
				var type = ProjectPrefsEntry.ValueType.String;

				if (this.entries.ContainsKey (key)
				 && type == this.entries[key].Type) {
					val = this.entries[key].String;
				}

				return val;
			}

			/// <summary>
			/// Returns true if key exists in the preferences.
			/// </summary>
			/// <param name="key">Key to check for.</param>
			/// <returns>True if it exists. False otherwise.</returns>
			public bool HasKey (string key) {
				return this.entries.ContainsKey (key);
			}

			/// <summary>
			/// Sets the value of the preference identified by key.
			/// </summary>
			/// <param name="key">Key to set.</param>
			/// <param name="val">Value to be associated with the key.</param>
			/// <returns>Value which has just been stored.</returns>
			public bool SetBool (string key, bool val) {
				var entry = new ProjectPrefsEntry (key) {
					Bool = val,
					Type = ProjectPrefsEntry.ValueType.Bool
				};

				this.SetOrCreate (key, entry);

				return val;
			}

			/// <summary>
			/// Sets the value of the preference identified by key.
			/// </summary>
			/// <param name="key">Key to set.</param>
			/// <param name="val">Value to be associated with the key.</param>
			/// <returns>Value which has just been stored.</returns>
			public float SetFloat (string key, float val) {
				var entry = new ProjectPrefsEntry (key) {
					Float = val,
					Type = ProjectPrefsEntry.ValueType.Float
				};

				this.SetOrCreate (key, entry);

				return val;
			}

			/// <summary>
			/// Sets the value of the preference identified by key.
			/// </summary>
			/// <param name="key">Key to set.</param>
			/// <param name="val">Value to be associated with the key.</param>
			/// <returns>Value which has just been stored.</returns>
			public int SetInt (string key, int val) {
				var entry = new ProjectPrefsEntry (key) {
					Int = val,
					Type = ProjectPrefsEntry.ValueType.Int
				};

				this.SetOrCreate (key, entry);

				return val;
			}

			/// <summary>
			/// Sets the value of the preference identified by key.
			/// </summary>
			/// <param name="key">Key to set.</param>
			/// <param name="val">Value to be associated with the key.</param>
			/// <returns>Value which has just been stored.</returns>
			public string SetString (string key, string val) {
				var entry = new ProjectPrefsEntry (key) {
					String = val,
					Type = ProjectPrefsEntry.ValueType.String
				};

				this.SetOrCreate (key, entry);

				return val;				
			}

			/// <summary>
			/// Creates a new ProjectPrefsStorage.
			/// </summary>
			public ProjectPrefsStorage () {
				this.entries = new Dictionary<string, ProjectPrefsEntry> ();
			}

			/// <summary>
			/// Sets or creates and sets a value in given dictionary.
			/// </summary>
			/// <typeparam name="TValue">Dictionary value type.</typeparam>
			/// <param name="key">Key to associate value with.</param>
			/// <param name="entry">Entry to set.</param>
			private void SetOrCreate (string key, ProjectPrefsEntry entry) {
				if (!this.entries.ContainsKey (key)) {
					this.entries.Add (key, entry);
				}
				else {
					this.entries[key] = entry;
				}
			}

			/// <summary>
			/// Removes given key from storage if it exists.
			/// </summary>
			/// <typeparam name="TValue">Type of storage value type.</typeparam>
			/// <param name="dict">Dictionary to remove value from.</param>
			/// <param name="key">Key to remove.</param>
			private static void RemoveIfContainedBy<TValue> (IDictionary<string, TValue> dict, string key) {
				if (dict.ContainsKey (key)) {
					dict.Remove (key);
				}
			}

			/// <summary>
			/// Storage for entries.
			/// </summary>
			private IDictionary<string, ProjectPrefsEntry> entries;

		}

		/// <summary>
		/// Key value pair representing an entry in the project storage.
		/// </summary>
		/// <typeparam name="TValue">Type of the value stored.</typeparam>
		public class ProjectPrefsEntry {

			/// <summary>
			/// Key value is associated with.
			/// </summary>
			public string Key;

			/// <summary>
			/// Boolean value associated with Key.
			/// </summary>
			public bool Bool;
			/// <summary>
			/// Integer value associated with Key.
			/// </summary>
			public int Int;
			/// <summary>
			/// Float value associated with Key.
			/// </summary>
			public float Float;
			/// <summary>
			/// String value associated with Key.
			/// </summary>
			public string String;

			/// <summary>
			/// Type of value.
			/// </summary>
			public enum ValueType {
				Bool,
				Int,
				Float,
				String
			};

			/// <summary>
			/// Type of value.
			/// </summary>
			public ValueType Type;

			/// <summary>
			/// Create a new ProjectPrefsEntry.
			/// </summary>
			/// <param name="key">Key for lookup.</param>
			public ProjectPrefsEntry (string key) {
				this.Key = key;

				this.Bool = ProjectPrefsStorage.Defaults.Bool;
				this.Int = ProjectPrefsStorage.Defaults.Int;
				this.Float = ProjectPrefsStorage.Defaults.Float;
				this.String = ProjectPrefsStorage.Defaults.String;

				this.Type = ValueType.Bool;
			}

			/// <summary>
			/// Creates a copy.
			/// </summary>
			/// <param name="old">Object to copy.</param>
			public ProjectPrefsEntry (ProjectPrefsEntry old) {
				this.Key = old.Key;

				this.Bool = old.Bool;
				this.Int = old.Int;
				this.Float = old.Float;
				this.String = old.String;
				this.Type = old.Type;
			}

		}

	}
}
