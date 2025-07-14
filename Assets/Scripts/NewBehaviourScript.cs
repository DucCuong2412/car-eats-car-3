using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class NewBehaviourScript : MonoBehaviour
{
	public GameObject Shade;

	public GameObject HUD;

	public AnimationCurve xCurve;

	public AnimationCurve yCurve;

	public Scrollbar SB;

	public RectTransform trans;

	public float speed = 1f;

	private Vector3 oldpos = Vector3.one;

	private ComicsStartTriger comixContr;

	private bool goMapFlag;

	private void OnEnable()
	{
		if (Progress.shop.StartComixShow)
		{
			base.enabled = false;
		}
		else
		{
			StartCoroutine(delayToStart());
		}
	}

	private IEnumerator delayToStart()
	{
		int t = 2;
		while (t > 0)
		{
			t--;
			yield return null;
		}
		SB.gameObject.SetActive(value: true);
		SB.value = 0f;
		SceneManager.LoadScene("comics_new", LoadSceneMode.Additive);
	}

	public void goMap(ComicsStartTriger contr)
	{
		comixContr = contr;
		goMapFlag = true;
		Shade.SetActive(value: false);
		HUD.SetActive(value: false);
	}

	public void OffAll()
	{
		SB.value = 0f;
		float time = SB.value * xCurve.keys[xCurve.length - 1].time;
		RectTransform rectTransform = trans;
		float x = 0f - xCurve.Evaluate(time);
		float y = yCurve.Evaluate(time);
		Vector3 position = base.transform.position;
		rectTransform.anchoredPosition = new Vector3(x, y, position.z);
		HUD.SetActive(value: true);
		Shade.SetActive(value: true);
		Audio.PlayBackgroundMusic("music_interface");
		base.enabled = false;
	}

	private void Update()
	{
		if (goMapFlag)
		{
			SB.value += Time.deltaTime / 10f;
			if (SB.value >= 0.95f)
			{
				comixContr.SetPlay();
				SB.value = 0.95f;
				goMapFlag = false;
			}
		}
		float time = SB.value * xCurve.keys[xCurve.length - 1].time;
		RectTransform rectTransform = trans;
		float x = 0f - xCurve.Evaluate(time);
		float y = yCurve.Evaluate(time);
		Vector3 position = base.transform.position;
		rectTransform.anchoredPosition = new Vector3(x, y, position.z);
	}
}
