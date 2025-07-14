using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Net;
using System.Text;
using System.Xml;
using System.Xml.Serialization;
using UnityEngine;

public class DebugConfig
{
	public class Config : IComparable<Config>
	{
		public string fileName = string.Empty;

		public DateTime lastModified = DateTime.MinValue;

		[NonSerialized]
		public bool cached;

		public Config()
		{
		}

		public Config(Config config)
		{
			fileName = config.fileName;
			lastModified = config.lastModified;
		}

		public Config(string fileName, DateTime lastModified)
		{
			this.fileName = fileName;
			this.lastModified = lastModified;
		}

		public override string ToString()
		{
			return lastModified.AddHours(2.0).ToString("dd.MM HH:mm");
		}

		public int CompareTo(Config obj)
		{
			if (lastModified > obj.lastModified)
			{
				return 1;
			}
			if (lastModified < obj.lastModified)
			{
				return -1;
			}
			return 0;
		}
	}

	public class AllConfigs
	{
		public List<Config> allConfigs;

		public AllConfigs()
		{
			allConfigs = new List<Config>();
		}

		public AllConfigs(params Config[] configs)
		{
			allConfigs = new List<Config>();
			allConfigs.AddRange(configs);
		}
	}

	public class ConfigPair
	{
		public string name;

		public Config server;

		public Config local;

		public ConfigPair(Config server, Config local)
		{
			name = server.fileName;
			this.server = server;
			this.local = local;
		}
	}

	private AllConfigs localConfigs;

	private AllConfigs serverConfigs;

	public Dictionary<string, ConfigPair> GetAvailableConfigs()
	{
		Dictionary<string, ConfigPair> dictionary = new Dictionary<string, ConfigPair>();
		foreach (Config allConfig in serverConfigs.allConfigs)
		{
			foreach (Config allConfig2 in localConfigs.allConfigs)
			{
				if (!(allConfig2.fileName != allConfig.fileName))
				{
					if (allConfig2.cached)
					{
						dictionary.Add(allConfig.fileName, new ConfigPair(allConfig, allConfig2));
						break;
					}
					if (allConfig.lastModified > allConfig2.lastModified)
					{
						dictionary.Add(allConfig.fileName, new ConfigPair(allConfig, allConfig2));
						break;
					}
				}
			}
		}
		return dictionary;
	}

	public bool Init()
	{
		if (GetLocalConfigs() && GetServerConfigs())
		{
			return true;
		}
		return false;
	}

	public static void ClearCache()
	{
		DirectoryInfo directoryInfo = new DirectoryInfo(Application.persistentDataPath);
		List<FileInfo> list = new List<FileInfo>();
		list.AddRange(directoryInfo.GetFiles("*.xml"));
		while (list.Count > 0)
		{
			File.Delete(list[0].FullName);
			list.RemoveAt(0);
		}
	}

	private bool GetLocalConfigs()
	{
		TextAsset textAsset = Resources.Load("DebugVersions") as TextAsset;
		if (textAsset == null)
		{
			CreateDebugVersions();
			textAsset = (Resources.Load("DebugVersions") as TextAsset);
		}
		localConfigs = Deserialize<AllConfigs>(textAsset.text);
		DirectoryInfo directoryInfo = new DirectoryInfo(Application.persistentDataPath);
		FileInfo[] files = directoryInfo.GetFiles("*.xml");
		FileInfo[] array = files;
		foreach (FileInfo fileInfo in array)
		{
			foreach (Config allConfig in localConfigs.allConfigs)
			{
				if (allConfig.fileName.Equals(fileInfo.Name))
				{
					allConfig.cached = true;
					allConfig.lastModified = fileInfo.LastWriteTimeUtc;
				}
			}
		}
		return true;
	}

	private void CreateDebugVersions()
	{
		DirectoryInfo directoryInfo = new DirectoryInfo(Application.dataPath + "/Resources/");
		List<FileInfo> list = new List<FileInfo>();
		list.AddRange(directoryInfo.GetFiles("*.xml", SearchOption.AllDirectories));
		AllConfigs allConfigs = new AllConfigs();
		foreach (FileInfo item in list)
		{
			allConfigs.allConfigs.Add(new Config(item.Name, item.LastWriteTimeUtc));
		}
		string value = Serialize<AllConfigs>(allConfigs);
		StreamWriter streamWriter = new StreamWriter(Application.dataPath + "/Resources/DebugVersions.txt");
		streamWriter.Write(value);
		streamWriter.Close();
	}

	private bool GetServerConfigs()
	{
		if (Application.internetReachability == NetworkReachability.NotReachable)
		{
			UnityEngine.Debug.LogError("Network is not reachable");
			serverConfigs = new AllConfigs();
			return false;
		}
		WebClient webClient = new WebClient();
		webClient.Credentials = new NetworkCredential(ConfigSettings.Instance.username, ConfigSettings.Instance.password);
		byte[] buffer = webClient.DownloadData(ConfigSettings.Instance.httpAddress + "DebugVersions.txt");
		MemoryStream memoryStream = new MemoryStream(buffer);
		memoryStream.Position = 0L;
		StreamReader streamReader = new StreamReader(memoryStream, Encoding.UTF8);
		serverConfigs = Deserialize<AllConfigs>(streamReader.ReadToEnd());
		streamReader.Close();
		memoryStream.Close();
		return true;
	}

	public IEnumerator UpdateFile(string filename, Action<bool> callback)
	{
		if (Application.internetReachability == NetworkReachability.NotReachable)
		{
			callback(obj: false);
			yield break;
		}
		WWW www = new WWW(ConfigSettings.Instance.httpAddress + filename);
		while (!www.isDone)
		{
			yield return null;
		}
		StreamWriter writer = new StreamWriter(Application.persistentDataPath + "/" + filename);
		writer.Write(www.text);
		writer.Close();
		foreach (Config allConfig in localConfigs.allConfigs)
		{
			if (!(allConfig.fileName != filename))
			{
				FileInfo fileInfo = new FileInfo(Application.persistentDataPath + "/" + filename);
				if (fileInfo.Exists)
				{
					allConfig.lastModified = fileInfo.LastWriteTimeUtc;
					allConfig.cached = true;
				}
				break;
			}
		}
		callback?.Invoke(obj: true);
	}

	public bool HasLocalVersion(string fileName)
	{
		return File.Exists(Application.persistentDataPath + "/" + fileName);
	}

	public bool HasNewVersion(string fileName)
	{
		DateTime t = LastModifiedOnServer(fileName);
		DateTime t2 = LastModifiedLocal(fileName);
		return t > t2;
	}

	public bool HasNewVersion(string fileName, out DateTime localDate, out DateTime serverDate)
	{
		serverDate = LastModifiedOnServer(fileName);
		localDate = LastModifiedLocal(fileName);
		return serverDate > localDate;
	}

	public DateTime LastModifiedOnServer(string filename)
	{
		FtpWebRequest ftpWebRequest = (FtpWebRequest)WebRequest.Create(ConfigSettings.Instance.httpAddress + filename);
		ftpWebRequest.Credentials = new NetworkCredential(ConfigSettings.Instance.username, ConfigSettings.Instance.password);
		ftpWebRequest.Method = "MDTM";
		FtpWebResponse ftpWebResponse = (FtpWebResponse)ftpWebRequest.GetResponse();
		return ftpWebResponse.LastModified;
	}

	public DateTime LastModifiedLocal(string fileName)
	{
		FileInfo fileInfo = new FileInfo(Application.persistentDataPath + "/" + fileName);
		return fileInfo.LastWriteTimeUtc;
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

	private static string Serialize<T>(object details)
	{
		return Serialize(details, typeof(T));
	}

	private static T Deserialize<T>(string details)
	{
		XmlSerializer xmlSerializer = new XmlSerializer(typeof(T));
		XmlReader xmlReader = XmlReader.Create(new StringReader(details));
		return (T)xmlSerializer.Deserialize(xmlReader);
	}

	private static object Deserialize(string details, Type type)
	{
		XmlSerializer xmlSerializer = new XmlSerializer(type);
		XmlReader xmlReader = XmlReader.Create(new StringReader(details));
		return xmlSerializer.Deserialize(xmlReader);
	}
}
