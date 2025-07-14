using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Car2DGhostModuleBase : Car2DModuleBase, IGhostModule
{
	public delegate void GhostSkin(int BodyType = 0, int WheelsType = 0, int TurboType = 0, int EngineType = 0, int AbsorsType = 0, int[] TuningType = null, int PaintType = -1, int color = -1);

	private List<Transform> Wheels = new List<Transform>();

	private char _separator = ' ';

	private char _separatorS = '|';

	private string _recordName;

	private string _alllineReaded;

	private string[] _lineReaded = new string[10000];

	private string[] _arraySplited;

	private string tempShadowCar;

	private string tempShadow;

	private bool isWrite;

	private Transform Body;

	private List<string> shadowList = new List<string>();

	private int i;

	private float rotZ = -10f;

	public event GhostSkin GhostSkinEvent;

	public override void onModuleEnable()
	{
	}

	public override void onModuleDisable()
	{
	}

	public override void onModuleInited()
	{
	}

	public void Init(List<Transform> _wheels, Transform _body = null, string _ghostRecName = "", int engine = 0, int turbo = 0, int body = 0, int wheels = 0, int AbsorsType = 0, int[] TuningType = null, int PaintType = -1, int color = -1)
	{
		Wheels = _wheels;
		Body = _body;
		_recordName = _ghostRecName;
		string text = string.Empty;
		for (int i = 0; i < TuningType.Length; i++)
		{
			text += (TuningType[i] + 1).ToString();
		}
		tempShadowCar = string.Format("{0:F0}" + _separator + "{1:F0}" + _separator + "{2:F0}" + _separator + "{3:F0}" + _separator + "{4:F0}" + _separator + "{5:F0}" + _separator + "{6:F0}" + _separator + "{7:F0}", engine, turbo, body, wheels, AbsorsType, text, PaintType + 1, color);
		base.moduleInited = true;
	}

	private IEnumerator UpdateModule()
	{
		while (base.moduleInited)
		{
			if (!base.moduleEnabled)
			{
				continue;
			}
			if (isWrite)
			{
				if (Body != null)
				{
					string format = "{0:F2}" + _separator + "{1:F2}" + _separator + "{2:F0}" + _separatorS;
					Vector3 position = Body.position;
					object arg = position.x;
					Vector3 position2 = Body.position;
					object arg2 = position2.y;
					Vector3 eulerAngles = Body.rotation.eulerAngles;
					tempShadow = string.Format(format, arg, arg2, eulerAngles.z);
					shadowList.Add(tempShadow);
				}
				else
				{
					SaveRecord();
				}
			}
			else
			{
				_arraySplited = _lineReaded[this.i].Split(_separator);
				Body.position = new Vector2(float.Parse(_arraySplited[0]), float.Parse(_arraySplited[1]));
				Body.rotation = Quaternion.Euler(0f, 0f, float.Parse(_arraySplited[2]));
				for (int i = 0; i < Wheels.Count; i++)
				{
					Wheels[i].rotation = Quaternion.Euler(0f, 0f, (float)this.i * rotZ);
				}
				if (this.i < _lineReaded.Length - 10)
				{
					this.i++;
				}
			}
			yield return new WaitForFixedUpdate();
		}
	}

	public void StartPlay()
	{
		base.moduleEnabled = true;
		bool flag = false;
		for (int i = 0; i < 10; i++)
		{
			if (PlayerPrefs.HasKey(_recordName.Remove(_recordName.Length - 1) + i) && !flag)
			{
				_recordName = _recordName.Remove(_recordName.Length - 1) + i;
				flag = true;
			}
		}
		if (!flag)
		{
			return;
		}
		_arraySplited = new string[4];
		_alllineReaded = PlayerPrefs.GetString(_recordName);
		_lineReaded = _alllineReaded.Split(_separatorS);
		_arraySplited = _lineReaded[0].Split(_separator);
		if (this.GhostSkinEvent != null && _recordName.Substring(_recordName.Length - 1) == "0")
		{
			int[] array = new int[_arraySplited[5].Length];
			for (int j = 0; j < array.Length; j++)
			{
				array[j] = int.Parse(_arraySplited[5].Substring(j, 1)) - 1;
			}
			UnityEngine.Debug.Log("Ghost color " + _arraySplited[7]);
			this.GhostSkinEvent(int.Parse(_arraySplited[2]), int.Parse(_arraySplited[3]), int.Parse(_arraySplited[1]), int.Parse(_arraySplited[0]), int.Parse(_arraySplited[4]), array, int.Parse(_arraySplited[6]), int.Parse(_arraySplited[7]));
		}
		isWrite = false;
		StartCoroutine(UpdateModule());
	}

	public void StartRecording()
	{
		base.moduleEnabled = true;
		if (_recordName != null)
		{
			isWrite = true;
			StartCoroutine(UpdateModule());
		}
	}

	public void Pause()
	{
		base.moduleEnabled = false;
	}

	public void Resume()
	{
		base.moduleEnabled = true;
	}

	public void SaveRecord(float t)
	{
		StartCoroutine(SaveWhileTime(t));
	}

	public void SaveRecord()
	{
		if (shadowList.Count == 0)
		{
			return;
		}
		for (int i = 0; i < 10; i++)
		{
			if (PlayerPrefs.HasKey(_recordName.Remove(_recordName.Length - 1) + i))
			{
				PlayerPrefs.DeleteKey(_recordName.Remove(_recordName.Length - 1) + i);
			}
		}
		PlayerPrefs.SetString(_recordName, tempShadowCar + _separator + string.Concat(shadowList.ToArray()));
	}

	private IEnumerator SaveWhileTime(float t)
	{
		yield return new WaitForSeconds(t);
		SaveRecord();
	}
}
