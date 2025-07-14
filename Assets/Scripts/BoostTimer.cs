using AnimationOrTween;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BoostTimer : MonoBehaviour
{
	[Serializable]
	public class TimerSettings
	{
		public string Name;

		public Image Ico;

		public Color EffectColor;
	}

	public List<TimerSettings> Icons = new List<TimerSettings>();

	private bool init;

	private GUIColorTint Tint;

	private float t;

	private float time;

	private int number;

	private Color effectstart;

	private Animation anim;

	private Animation Anim
	{
		get
		{
			if (anim == null)
			{
				anim = base.gameObject.GetComponent<Animation>();
			}
			return anim;
		}
	}

	private int Number(string name)
	{
		for (int i = 0; i < Icons.Count; i++)
		{
			if (name == Icons[i].Name)
			{
				return i;
			}
		}
		return 0;
	}

	public void Init(GUIColorTint _tint)
	{
		Tint = _tint;
		init = true;
	}

	public void OnEnable()
	{
		if (t > 0f)
		{
			StartCoroutine(TimerWork());
		}
	}

	public void StartTimer(float _time, string name)
	{
		if (!init)
		{
			UnityEngine.Debug.LogWarning("BoostTimer Not inited");
			return;
		}
		number = Number(name);
		time = _time;
		t = _time;
		effectstart = Tint.getGameWorldColor();
		StartCoroutine(TimerWork());
	}

	private IEnumerator TimerWork()
	{
		Tint.SetGameWorldColor(effectstart, Icons[number].EffectColor, time);
		ActiveAnimation.Play(Anim, Direction.Forward);
		Icons[number].Ico.gameObject.SetActive(value: true);
		while (t > 0f)
		{
			Icons[number].Ico.fillAmount = t / time;
			t -= Time.deltaTime;
			yield return null;
		}
		Icons[number].Ico.gameObject.SetActive(value: false);
		Tint.SetGameWorldColor(Icons[number].EffectColor, effectstart, time);
	}

	public void Stop()
	{
		t = 0f;
	}
}
