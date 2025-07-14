using System;
using System.Collections;
using System.Collections.Generic;
using System.Security.Cryptography;
using System.Text;
using UnityEngine;

public class CEC3Score : MonoBehaviour
{
	private static CEC3Score p_instance;

	private const string secretKey = "unity_cec3";

	protected static CEC3Score instance
	{
		get
		{
			if (p_instance == null)
			{
				GameObject gameObject = new GameObject("_CEC3Score");
				p_instance = gameObject.AddComponent<CEC3Score>();
				UnityEngine.Object.DontDestroyOnLoad(gameObject);
			}
			return p_instance;
		}
	}

	public static CEC3Score GetInstance()
	{
		return instance;
	}

	public void PostScore(long id, int score)
	{
		string value = Md5Sum(id.ToString() + score.ToString() + "unity_cec3");
		WWWForm wWWForm = new WWWForm();
		wWWForm.AddField("id", id.ToString());
		wWWForm.AddField("score", score);
		wWWForm.AddField("hash", value);
		StartCoroutine(check("https://fb.myspygames.com/games/mobile/careatscar3/setscore.php", wWWForm));
	}

	private IEnumerator check(string URL, WWWForm Data)
	{
		WWW www = new WWW(URL, Data);
		yield return www;
		if (www.error == null)
		{
		}
	}

	private IEnumerator checkGET(string URL, WWWForm Data)
	{
		WWW www = new WWW(URL, Data);
		yield return www;
		if (www.error != null)
		{
			yield break;
		}
		string[] array = www.text.Split(new string[4]
		{
			"{",
			"}",
			":",
			","
		}, StringSplitOptions.None);
		Dictionary<string, object> dictionary = new Dictionary<string, object>();
		for (int i = 0; i < array.Length; i++)
		{
			if (array[i].Contains("id"))
			{
				dictionary.Add(array[i], array[i + 1]);
			}
			if (array[i].Contains("score"))
			{
				dictionary.Add(array[i], array[i + 1]);
			}
		}
	}

	public void GetScore(string id)
	{
		string value = Md5Sum(id + "unity_cec3");
		WWWForm wWWForm = new WWWForm();
		wWWForm.AddField("id", id);
		wWWForm.AddField("hash", value);
		StartCoroutine(checkGET("https://fb.myspygames.com/games/mobile/careatscar3/getscore.php", wWWForm));
	}

	private string Md5Sum(string strToEncrypt)
	{
		UTF8Encoding uTF8Encoding = new UTF8Encoding();
		byte[] bytes = uTF8Encoding.GetBytes(strToEncrypt);
		MD5CryptoServiceProvider mD5CryptoServiceProvider = new MD5CryptoServiceProvider();
		byte[] array = mD5CryptoServiceProvider.ComputeHash(bytes);
		string text = string.Empty;
		for (int i = 0; i < array.Length; i++)
		{
			text += Convert.ToString(array[i], 16).PadLeft(2, '0');
		}
		return text.PadLeft(32, '0');
	}
}
