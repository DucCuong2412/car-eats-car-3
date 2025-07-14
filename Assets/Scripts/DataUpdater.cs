using System;
using System.IO;
using System.Net;
using UnityEngine;

public class DataUpdater : MonoBehaviour
{
	public UILabel label;

	private void Start()
	{
		if (Application.internetReachability == NetworkReachability.NotReachable)
		{
			label.text = "no i-net";
		}
	}

	public void UpdateData()
	{
		Game.LoadLevel("DebugScene");
	}

	private void DownloadCallback(GameObject go)
	{
		UnityEngine.Object.Destroy(go);
		label.transform.parent.gameObject.SetActive(value: false);
	}

	private bool hasData()
	{
		for (int i = 1; i <= 2; i++)
		{
			if (!File.Exists(Application.persistentDataPath + "/Pack" + i + ".bytes"))
			{
				return false;
			}
		}
		return true;
	}

	private bool isOldData()
	{
		if (Application.internetReachability == NetworkReachability.NotReachable)
		{
			return false;
		}
		for (int i = 1; i <= 2; i++)
		{
			try
			{
				HttpWebRequest httpWebRequest = (HttpWebRequest)WebRequest.Create("http://noodle.at.ua/xml/Pack" + i.ToString() + ".bytes");
				HttpWebResponse httpWebResponse = (HttpWebResponse)httpWebRequest.GetResponse();
				DateTime lastWriteTime = File.GetLastWriteTime(Application.persistentDataPath + "/Pack" + i + ".bytes");
				DateTime lastModified = httpWebResponse.LastModified;
				if (lastModified > lastWriteTime)
				{
					return true;
				}
			}
			catch (WebException)
			{
			}
		}
		return false;
	}
}
