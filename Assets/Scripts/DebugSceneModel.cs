using System;
using System.Collections;
using UnityEngine;

public class DebugSceneModel : MonoBehaviour, DebugSceneInterface
{
	private bool _isUse;

	private bool _hasNewVersion;

	private string _currentVersion;

	public bool isUse
	{
		get
		{
			return true;
		}
		set
		{
			_isUse = value;
			UnityEngine.Debug.Log("used: " + _isUse);
		}
	}

	public bool hasNewVersion => _hasNewVersion;

	public string currentVersion => _currentVersion;

	public void UpdateNode(Action<bool> callbaсk)
	{
		_hasNewVersion = false;
		RunAfterTime(0.3f, delegate
		{
			callbaсk(obj: false);
		});
	}

	private void Awake()
	{
		_isUse = true;
		_hasNewVersion = true;
		_currentVersion = "11.02.15";
	}

	private void RunAfterTime(float time, Action _action)
	{
		StartCoroutine(RunAction(time, _action));
	}

	private IEnumerator RunAction(float time, Action _action)
	{
		yield return Utilities.WaitForRealSeconds(time);
		_action();
	}
}
