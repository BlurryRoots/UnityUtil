﻿using System.IO;
using System.Xml.Serialization;
using System.Runtime.Serialization.Formatters.Binary;

namespace BlurryRoots.Storage {

	/// <summary>
	/// Serialized / Deserialized on creation.
	/// </summary>
	/// <typeparam name="TDataType">The type you want to (de)serialze.</typeparam>
	public class Base64DeSerializer<TDataType> {

		/// <summary>
		/// String representing the serialized data.
		/// </summary>
		public string Serialized {
			get;
			private set;
		}

		/// <summary>
		/// Data corresponding to the serialized string.
		/// </summary>
		public TDataType Data {
			get;
			private set;
		}

		/// <summary>
		/// Creates new Base64DeSerializer from data. Serialized the data on 
		/// construction.
		/// </summary>
		/// <param name="data">Data to be serialized.</param>
		public Base64DeSerializer (TDataType data) {
			this.Serialized = Base64DeSerializer<TDataType>
				.SerializeToBase64String (data);
			this.Data = data;
		}

		/// <summary>
		/// Creates a new Base64DeSerializer from a string representing base64
		/// serialized data. Gets deserialzed on construction.
		/// </summary>
		/// <param name="serialized">Base64 serialized data.</param>
		public Base64DeSerializer (string serialized) {
			this.Serialized = serialized;
			this.Data = Base64DeSerializer<TDataType>
				.DesializeFromBase64String<TDataType> (this.Serialized);
		}

		/// <summary>
		/// Helper method do deserialized from string.
		/// </summary>
		/// <typeparam name="TData">Data type used to interpret data.</typeparam>
		/// <param name="s">Serialized string.</param>
		/// <returns>Deserialized data.</returns>
		private static TData DesializeFromBase64String<TData> (string s) {
			byte[] b = System.Convert.FromBase64String (s);

			using (var stream = new MemoryStream (b)) {
				var formatter = new BinaryFormatter ();
				stream.Seek (0, SeekOrigin.Begin);

				return (TData)formatter.Deserialize (stream);
			}
		}

		/// <summary>
		/// Helper method used to serialized data to base64 encoded string.
		/// </summary>
		/// <typeparam name="TData">Data type of data to be serialized.</typeparam>
		/// <param name="data">Data to be serizaled.</param>
		/// <returns>Base64 encoded serialized data.</returns>
		private static string SerializeToBase64String<TData> (TData data) {
			using (var stream = new MemoryStream ()) {
				var formatter = new BinaryFormatter ();
				formatter.Serialize (stream, data);
				stream.Flush ();
				stream.Position = 0;

				return System.Convert.ToBase64String (stream.ToArray ());
			}
		}

	}

}