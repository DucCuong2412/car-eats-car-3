using SA.Common.Models;
using SA.Common.Pattern;
using System;
using System.Collections.Generic;
using UnityEngine;

public class iCloudManager : Singleton<iCloudManager>
{
	public static event Action<Result> OnCloudInitAction;

	public static event Action<iCloudData> OnCloudDataReceivedAction;

	public static event Action<List<iCloudData>> OnStoreDidChangeExternally;

	private void Awake()
	{
		UnityEngine.Object.DontDestroyOnLoad(base.gameObject);
	}

	public void setString(string key, string val)
	{
	}

	public void setFloat(string key, float val)
	{
	}

	public void setData(string key, byte[] val)
	{
	}

	public void requestDataForKey(string key)
	{
	}

	private void OnCloudInit()
	{
		Result obj = new Result();
		iCloudManager.OnCloudInitAction(obj);
	}

	private void OnCloudInitFail()
	{
		Result obj = new Result(new Error());
		iCloudManager.OnCloudInitAction(obj);
	}

	private void OnCloudDataChanged(string data)
	{
		List<iCloudData> list = new List<iCloudData>();
		string[] array = data.Split('|');
		for (int i = 0; i < array.Length && !(array[i] == "endofline"); i += 2)
		{
			iCloudData item = new iCloudData(array[i], array[i + 1]);
			list.Add(item);
		}
		iCloudManager.OnStoreDidChangeExternally(list);
	}

	private void OnCloudData(string array)
	{
		string[] array2 = array.Split('|');
		iCloudData obj = new iCloudData(array2[0], array2[1]);
		iCloudManager.OnCloudDataReceivedAction(obj);
	}

	private void OnCloudDataEmpty(string array)
	{
		string[] array2 = array.Split('|');
		iCloudData obj = new iCloudData(array2[0], "null");
		iCloudManager.OnCloudDataReceivedAction(obj);
	}

	static iCloudManager()
	{
		iCloudManager.OnCloudInitAction = delegate
		{
		};
		iCloudManager.OnCloudDataReceivedAction = delegate
		{
		};
		iCloudManager.OnStoreDidChangeExternally = delegate
		{
		};
	}
}
