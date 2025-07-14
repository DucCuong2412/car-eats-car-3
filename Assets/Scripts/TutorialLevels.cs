using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TutorialLevels : MonoBehaviour
{
	public GameObject clickHolder;

	public TweenColor transperent;

	public GameObject text;

	public GameObject text2;

	public GameObject[] widgets;

	public GameObject[] widgets2;

	public GameObject hand;

	public UILabel labelEachStart;

	private bool scaling;

	public static bool needTutorial
	{
		get
		{
			if (Progress.levels.active_level == 1 && Progress.levels.active_pack == 1)
			{
				return (PlayerPrefs.GetInt("TutorialLevels", 0) == 0) ? true : false;
			}
			return false;
		}
	}

	private void Awake()
	{
		if (!needTutorial)
		{
			base.gameObject.SetActive(value: false);
			return;
		}
		labelEachStart.text = $"{PriceConfig.instance.energy.eachStart}";
		clickHolder.SetActive(value: true);
		transperent.gameObject.SetActive(value: true);
	}

	public void ShowTutorial()
	{
		PlayerPrefs.SetInt("TutorialLevels", 1);
		List<UIWidget> list = new List<UIWidget>();
		GameObject[] array = widgets;
		foreach (GameObject gameObject in array)
		{
			list.AddRange(gameObject.GetComponentsInChildren<UIWidget>(includeInactive: true));
		}
		List<GameObject> list2 = new List<GameObject>();
		foreach (UIWidget item in list)
		{
			UIWidget uIWidget = UnityEngine.Object.Instantiate(item);
			uIWidget.transform.parent = item.transform.parent;
			uIWidget.transform.position = item.transform.position;
			uIWidget.transform.localScale = item.transform.localScale;
			uIWidget.alpha = 0f;
			uIWidget.depth += 60;
			if (!item.gameObject.name.Contains("anim"))
			{
				TweenColor tweenColor = uIWidget.gameObject.AddComponent<TweenColor>();
				tweenColor.from = new Color(1f, 1f, 1f, 0f);
				tweenColor.to = new Color(1f, 1f, 1f, 1f);
				tweenColor.duration = 0.5f;
				tweenColor.PlayForward();
			}
			else
			{
				item.depth += 60;
			}
			list2.Add(uIWidget.gameObject);
		}
		StartCoroutine(waitAnim(list2));
	}

	private IEnumerator waitAnim(List<GameObject> ascalingObjects)
	{
		TweenColor[] tweens2 = text.GetComponentsInChildren<TweenColor>(includeInactive: true);
		TweenColor[] array = tweens2;
		foreach (TweenColor tweenColor in array)
		{
			tweenColor.gameObject.SetActive(value: true);
		}
		float dt = 1.5f;
		while (dt > 0f)
		{
			dt -= Time.unscaledDeltaTime;
			yield return null;
		}
		foreach (GameObject ascalingObject in ascalingObjects)
		{
			StartCoroutine(animateScale(ascalingObject.gameObject));
		}
		while (scaling)
		{
			yield return null;
		}
		List<UIWidget> list = new List<UIWidget>();
		GameObject[] array2 = widgets2;
		foreach (GameObject gameObject in array2)
		{
			list.AddRange(gameObject.GetComponentsInChildren<UIWidget>(includeInactive: true));
		}
		foreach (UIWidget item in list)
		{
			if (item.gameObject.name.ToLower().Contains("label"))
			{
				item.alpha = 0.01f;
			}
			item.depth += 60;
		}
		Object.FindObjectOfType<LevelGalleryView>().TeleportCarFromTo(0, 0, 1, 1);
		tweens2 = text2.GetComponentsInChildren<TweenColor>(includeInactive: true);
		TweenColor[] array3 = tweens2;
		foreach (TweenColor tweenColor2 in array3)
		{
			tweenColor2.gameObject.SetActive(value: true);
		}
	}

	public void ScaleHand()
	{
		hand.SetActive(value: true);
		clickHolder.SetActive(value: false);
	}

	private IEnumerator animateScale(GameObject go)
	{
		scaling = true;
		Vector3 startScale = go.transform.localScale;
		for (int k = 0; k < 2; k++)
		{
			for (float j = 1f; j < 1.2f; j += 0.015f)
			{
				go.transform.localScale = startScale * j;
				yield return null;
			}
			for (float i = 1.2f; i > 1f; i -= 0.015f)
			{
				go.transform.localScale = startScale * i;
				yield return null;
			}
		}
		scaling = false;
	}
}
