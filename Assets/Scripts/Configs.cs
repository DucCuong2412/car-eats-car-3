using System.IO;
using UnityEngine;
using xmlClassTemplate;

public static class Configs
{
	public static T Load<T>(string resourceName)
	{
		return LoadRelease<T>(resourceName);
	}

	private static T LoadRelease<T>(string resourceName)
	{
		TextAsset textAsset = Resources.Load(resourceName) as TextAsset;
		return XML.Deserialize<T>(textAsset.text);
	}

	private static T LoadCached<T>(string resourceName)
	{
		string str = removeFolderNames(resourceName) + ".xml";
		string path = Application.persistentDataPath + "/" + str;
		if (File.Exists(path))
		{
			StreamReader streamReader = new StreamReader(path);
			T result = XML.Deserialize<T>(streamReader.ReadToEnd());
			streamReader.Close();
			return result;
		}
		return LoadRelease<T>(resourceName);
	}

	private static string removeFolderNames(string fullPath)
	{
		while (fullPath.Contains("/"))
		{
			int num = fullPath.IndexOf('/');
			fullPath = fullPath.Remove(0, num + 1);
		}
		return fullPath;
	}
}
