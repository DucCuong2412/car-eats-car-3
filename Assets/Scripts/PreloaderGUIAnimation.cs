using System;
using System.Collections;
using UnityEngine;

public class PreloaderGUIAnimation : MonoBehaviour
{
	[SerializeField]
	private Transform left_point;

	[SerializeField]
	private Transform right_point;

	[SerializeField]
	private Transform target_loader;

	[SerializeField]
	private UILabel levelLable;

	private TweenAlpha tween;

	private float percentCurrent;

	private Vector3 point_l;

	private Vector3 point_r;

	public void HideAnim(Action action = null)
	{
		tween.AddOnFinished(delegate
		{
			base.gameObject.SetActive(value: false);
			if (action != null)
			{
				action();
			}
		});
		tween.PlayForward();
	}

	private void OnEnable()
	{
		Init();
	}

	private void Init()
	{
		levelLable.text = "LEVEL " + Utilities.LevelNumberGlobal(Progress.levels.active_level, Progress.levels.active_pack).ToString();
		Vector3 position = left_point.position;
		float x = position.x;
		Vector3 position2 = target_loader.position;
		float y = position2.y;
		Vector3 position3 = target_loader.position;
		point_l = new Vector3(x, y, position3.z);
		Vector3 position4 = right_point.position;
		float x2 = position4.x;
		Vector3 position5 = target_loader.position;
		float y2 = position5.y;
		Vector3 position6 = target_loader.position;
		point_r = new Vector3(x2, y2, position6.z);
		tween = base.gameObject.GetComponent<TweenAlpha>();
	}

	public IEnumerator MoveToBy(float percentTo, float timeBy)
	{
		float totalFrames = (float)Application.targetFrameRate * timeBy;
		float frames = totalFrames;
		while (frames > 0f)
		{
			float dt = (totalFrames - frames) / totalFrames;
			target_loader.position = Vector3.Lerp(point_l, point_r, percentCurrent + (percentTo - percentCurrent) * dt);
			frames -= 1f;
			yield return null;
		}
		percentCurrent = percentTo;
	}
}
