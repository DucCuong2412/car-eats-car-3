using System;
using System.IO;
using System.Xml;
using System.Xml.Serialization;

namespace xmlClassTemplate
{
	public class XML
	{
		public static string Serialize(object details, Type type)
		{
			XmlSerializer xmlSerializer = new XmlSerializer(type);
			MemoryStream memoryStream = new MemoryStream();
			xmlSerializer.Serialize(memoryStream, details);
			StreamReader streamReader = new StreamReader(memoryStream);
			memoryStream.Position = 0L;
			string result = streamReader.ReadToEnd();
			memoryStream.Flush();
			memoryStream = null;
			streamReader = null;
			return result;
		}

		public static string Serialize<T>(object details)
		{
			return Serialize(details, typeof(T));
		}

		public static T Deserialize<T>(string details)
		{
			XmlSerializer xmlSerializer = new XmlSerializer(typeof(T));
			XmlReader xmlReader = XmlReader.Create(new StringReader(details));
			return (T)xmlSerializer.Deserialize(xmlReader);
		}

		public static object Deserialize(string details, Type type)
		{
			XmlSerializer xmlSerializer = new XmlSerializer(type);
			XmlReader xmlReader = XmlReader.Create(new StringReader(details));
			return xmlSerializer.Deserialize(xmlReader);
		}
	}
}
