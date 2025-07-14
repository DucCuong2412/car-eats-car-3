using System;
using UnityEngine;

public static class tk2dUITime
{
	private static double lastRealTime;

	private static float _deltaTime = 0.0166666675f;

	public static float deltaTime => _deltaTime;

	public static void Init()
	{
		lastRealTime = Time.realtimeSinceStartup;
		_deltaTime = Time.maximumDeltaTime;
	}

	public static void Update()
	{
		float realtimeSinceStartup = Time.realtimeSinceStartup;
		if (Time.timeScale < 0.001f)
		{
			_deltaTime = Mathf.Min(71f / (339f * (float)Math.PI), (float)((double)realtimeSinceStartup - lastRealTime));
		}
		else
		{
			_deltaTime = Time.deltaTime / Time.timeScale;
		}
		lastRealTime = realtimeSinceStartup;
	}
}
