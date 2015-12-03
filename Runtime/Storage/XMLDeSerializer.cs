using System;
using System.Xml.Serialization;
using System.IO;

namespace BlurryRoots.Storage {

	/// <summary>
	/// Serialized / Deserialized on creation.
	/// </summary>
	/// <typeparam name="TDataType">The type you want to (de)serialze.</typeparam>
	public
	class XMLDeSerializer<TDataType> {

		/// <summary>
		/// String representing the serialized data.
		/// </summary>
		public
		string Serialized {
			get;
			private set;
		}

		/// <summary>
		/// Data corresponding to the serialized string.
		/// </summary>
		public
		TDataType Data {
			get;
			private set;
		}

		/// <summary>
		/// Creates a new XMLDeSerializer from a string representing XML
		/// serialized data. Gets deserialzed on construction.
		/// </summary>
		/// <param name="serialized">XML serialized data.</param>
		public
		XMLDeSerializer (TDataType data) {
			this.Serialized = XMLDeSerializer<TDataType>
				.SerializeToXMLString (data);
			this.Data = data;
		}

		/// <summary>
		/// Creates new XMLDeSerializer from data. Serialized the data on 
		/// construction.
		/// </summary>
		/// <param name="data">Data to be serialized.</param>
		public
		XMLDeSerializer (string serialized) {
			this.Serialized = serialized;
			this.Data = XMLDeSerializer<TDataType>
				.DserializeFromXMLString<TDataType> (this.Serialized);
		}

		/// <summary>
		/// Helper method do deserialized from string.
		/// </summary>
		/// <typeparam name="TData">Data type used to interpret data.</typeparam>
		/// <param name="serialized">Serialized string.</param>
		/// <returns>Deserialized data.</returns>
		private static
		TData DserializeFromXMLString<TData> (string serialized) {
			var xmlSerializer = new XmlSerializer (typeof (TData));

			using (var textReader = new StringReader (serialized)) {
				return (TData)xmlSerializer.Deserialize (textReader);
			}
		}

		/// <summary>
		/// Helper method used to serialized data to base64 encoded string.
		/// </summary>
		/// <typeparam name="TData">Data type of data to be serialized.</typeparam>
		/// <param name="data">Data to be serizaled.</param>
		/// <returns>Base64 encoded serialized data.</returns>
		private static
		string SerializeToXMLString<TData> (TData data) {
			var xmlSerializer = new XmlSerializer (data.GetType ());

			using (var textWriter = new StringWriter ()) {
				xmlSerializer.Serialize (textWriter, data);

				return textWriter.ToString ();
			}
		}

	}

}