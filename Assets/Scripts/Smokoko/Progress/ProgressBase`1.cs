using System;
using System.IO;
using System.Reflection;
using System.Xml;
using System.Xml.Serialization;
using UnityEngine;

namespace Smokoko.Progress
{
	[Serializable]
	public class ProgressBase<T> : MonoBehaviour where T : ProgressBase<T>
	{
		private static T p_instance;

		protected static T instance
		{
			get
			{
				if ((UnityEngine.Object)p_instance == (UnityEngine.Object)null)
				{
					GameObject gameObject = new GameObject("_progress");
					p_instance = gameObject.AddComponent<T>();
					UnityEngine.Object.DontDestroyOnLoad(gameObject);
					LoadAllFields(p_instance);
				}
				return p_instance;
			}
		}

		public static T GetInstance()
		{
			return instance;
		}

		private void OnLevelWasLoaded(int level)
		{
			Save();
		}

		private void OnApplicationQuit()
		{
			Save();
			PlayerPrefs.Save();
			GameCenterWrapper.SaveGameSave();
		}

		private void OnApplicationPause(bool pauseStatus)
		{
			Save();
		}

		private static void LoadAllFields(T _instance)
		{
			BindingFlags bindingAttr = BindingFlags.Instance | BindingFlags.Public | BindingFlags.NonPublic;
			FieldInfo[] fields = _instance.GetType().GetFields(bindingAttr);
			FieldInfo[] array = fields;
			foreach (FieldInfo fieldInfo in array)
			{
				if (fieldInfo.FieldType.IsArray)
				{
					LoadArrayField(fieldInfo);
				}
				else
				{
					LoadField(fieldInfo);
				}
			}
		}

		private static void LoadField(FieldInfo fieldInfo)
		{
			string key = fieldInfo.FieldType.ToString() + "+" + fieldInfo.Name;
			if (PlayerPrefs.HasKey(key))
			{
				object value = Deserialize(PlayerPrefs.GetString(key), fieldInfo.FieldType);
				fieldInfo.SetValue(instance, value);
			}
			else
			{
				CreateDefaultField(fieldInfo);
			}
		}

		private static void LoadArrayField(FieldInfo fieldInfo)
		{
			string key = fieldInfo.FieldType.ToString() + "+" + fieldInfo.Name;
			if (PlayerPrefs.HasKey(key))
			{
				Array array = Deserialize(PlayerPrefs.GetString(key), fieldInfo.FieldType) as Array;
				Array array2 = fieldInfo.GetValue(instance) as Array;
				CreateDefaultField(fieldInfo, Mathf.Max(array.Length, array2.Length));
				for (int i = array.GetLowerBound(0); i <= Mathf.Min(array.GetUpperBound(0), array2.GetUpperBound(0)); i++)
				{
					array2.SetValue(array.GetValue(i), i);
				}
				fieldInfo.SetValue(instance, array2);
			}
			else
			{
				CreateDefaultField(fieldInfo);
			}
		}

		private static void CreateDefaultField(FieldInfo fieldInfo, int arrayLength = -1)
		{
			if (fieldInfo.FieldType.IsArray)
			{
				if (arrayLength < 0)
				{
					arrayLength = (fieldInfo.GetValue(instance) as Array).Length;
				}
				Array array = Array.CreateInstance(fieldInfo.FieldType.GetElementType(), arrayLength);
				for (int i = array.GetLowerBound(0); i <= array.GetUpperBound(0); i++)
				{
					array.SetValue(Activator.CreateInstance(fieldInfo.FieldType.GetElementType()), i);
				}
				fieldInfo.SetValue(instance, array);
			}
			else
			{
				fieldInfo.SetValue(instance, Activator.CreateInstance(fieldInfo.FieldType));
			}
		}

		protected void SaveField(FieldInfo fieldInfo)
		{
			string key = fieldInfo.FieldType.ToString() + "+" + fieldInfo.Name;
			string value = Serialize(fieldInfo.GetValue(instance), fieldInfo.FieldType);
			PlayerPrefs.SetString(key, value);
		}

		protected static void SaveField(string fieldName)
		{
			BindingFlags bindingAttr = BindingFlags.Instance | BindingFlags.Public | BindingFlags.NonPublic;
			T instance = ProgressBase<T>.instance;
			FieldInfo[] fields = instance.GetType().GetFields(bindingAttr);
			FieldInfo[] array = fields;
			int num = 0;
			FieldInfo fieldInfo;
			while (true)
			{
				if (num < array.Length)
				{
					fieldInfo = array[num];
					if (fieldInfo.Name == fieldName)
					{
						break;
					}
					num++;
					continue;
				}
				return;
			}
			T instance2 = ProgressBase<T>.instance;
			instance2.SaveField(fieldInfo);
		}

		protected void ClearAllFields()
		{
			BindingFlags bindingAttr = BindingFlags.Instance | BindingFlags.Public | BindingFlags.NonPublic;
			T instance = ProgressBase<T>.instance;
			FieldInfo[] fields = instance.GetType().GetFields(bindingAttr);
			FieldInfo[] array = fields;
			foreach (FieldInfo fieldInfo in array)
			{
				CreateDefaultField(fieldInfo, (!fieldInfo.FieldType.IsArray) ? (-1) : (fieldInfo.GetValue(ProgressBase<T>.instance) as Array).Length);
			}
		}

		public static void Save<_type>()
		{
			BindingFlags bindingAttr = BindingFlags.Instance | BindingFlags.Public | BindingFlags.NonPublic;
			T instance = ProgressBase<T>.instance;
			FieldInfo[] fields = instance.GetType().GetFields(bindingAttr);
			FieldInfo[] array = fields;
			foreach (FieldInfo fieldInfo in array)
			{
				if (fieldInfo.FieldType == typeof(_type))
				{
					T instance2 = ProgressBase<T>.instance;
					instance2.SaveField(fieldInfo);
				}
			}
		}

		public static void Save()
		{
			BindingFlags bindingAttr = BindingFlags.Instance | BindingFlags.Public | BindingFlags.NonPublic;
			T instance = ProgressBase<T>.instance;
			FieldInfo[] fields = instance.GetType().GetFields(bindingAttr);
			FieldInfo[] array = fields;
			foreach (FieldInfo fieldInfo in array)
			{
				T instance2 = ProgressBase<T>.instance;
				instance2.SaveField(fieldInfo);
			}
		}

		private static string Serialize(object details)
		{
			return Serialize(details, details.GetType());
		}

		private static string Serialize<TT>(object details)
		{
			return Serialize(details, typeof(TT));
		}

		private static string Serialize(object details, Type type)
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

		private static TT Deserialize<TT>(string details)
		{
			return (TT)Deserialize(details, typeof(TT));
		}

		private static object Deserialize(string details, Type type)
		{
			XmlSerializer xmlSerializer = new XmlSerializer(type);
			XmlReader xmlReader = XmlReader.Create(new StringReader(details));
			return xmlSerializer.Deserialize(xmlReader);
		}
	}
}
