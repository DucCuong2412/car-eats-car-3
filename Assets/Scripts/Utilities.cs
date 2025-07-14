using AnimationOrTween;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Utilities : MonoBehaviour
{
	private static Utilities _instance;

	public static Utilities instance
	{
		get
		{
			if (_instance == null)
			{
				GameObject gameObject = new GameObject("_utilities");
				_instance = gameObject.AddComponent<Utilities>();
			}
			return _instance;
		}
		set
		{
			_instance = value;
		}
	}

	private void OnDestroy()
	{
		_instance = null;
	}

	public static int LevelNumberGlobal(int level, int pack)
	{
		if (level == 0 || pack == 0)
		{
			return 1;
		}
		return level + 12 * (pack - 1);
	}

	public static Coroutine WaitForRealSeconds(float time)
	{
		return instance.StartCoroutine(WaitForRealSecondsImpl(time));
	}

	public static IEnumerator WaitForRealSecondsImpl(float time)
	{
		float startTime = Time.realtimeSinceStartup;
		while (Time.realtimeSinceStartup - startTime < time)
		{
			yield return 1;
		}
	}

	public void RunAfterTime(float time, Action _action)
	{
		StartCoroutine(RunAction(time, _action));
	}

	public IEnumerator RunAction(float time, Action _action)
	{
		yield return new WaitForSeconds(time);
		_action();
	}

	public static void RunActor(Animation animation, bool isForward, EventDelegate.Callback callBack = null)
	{
		List<Animation> list = new List<Animation>();
		list.Add(animation);
		RunActor(list, isForward, callBack);
	}

	public static void RunActor(List<Animation> animations, bool isForward, EventDelegate.Callback callBack = null)
	{
		Direction playDirection = isForward ? Direction.Forward : Direction.Reverse;
		foreach (Animation animation in animations)
		{
			if (animation != null)
			{
				ActiveAnimation activeAnimation = ActiveAnimation.Play(animation, playDirection);
				if (callBack != null)
				{
					EventDelegate.Add(activeAnimation.onFinished, callBack, oneShot: true);
				}
			}
		}
	}
}
