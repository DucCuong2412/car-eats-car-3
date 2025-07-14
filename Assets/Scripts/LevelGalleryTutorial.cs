using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LevelGalleryTutorial : MonoBehaviour
{
	public Text energy;

	public Text textRaceCosts;

	[SerializeField]
	private List<Animation> animHideShowParticks = new List<Animation>();

	private bool Clicked;

	public static bool needTutorial => false;

	public IEnumerator Start()
	{
		if (!needTutorial)
		{
			base.gameObject.SetActive(value: false);
			yield break;
		}
		foreach (Animation animHideShowPartick in animHideShowParticks)
		{
			animHideShowPartick["gateMiricle_hide"].speed = 1f;
			animHideShowPartick.Play("gateMiricle_hide");
		}
		energy.text = GameEnergyLogic.GetEnergy.ToString();
		yield return null;
		textRaceCosts.text = textRaceCosts.text.Replace("*", PriceConfig.instance.energy.eachStart.ToString());
	}

	public void AnimateFuelAndGo()
	{
		if (!Clicked)
		{
			Clicked = true;
			StartCoroutine(Animation());
		}
	}

	private IEnumerator Animation()
	{
		Audio.Play("fuel-1");
		GameEnergyLogic.GetFuelForRace();
		int eachStart = PriceConfig.instance.energy.eachStart;
		energy.text = GameEnergyLogic.GetEnergy.ToString();
		GameObject anim = UnityEngine.Object.Instantiate(energy.gameObject);
		Text text = anim.GetComponent<Text>();
		text.text = $"-{PriceConfig.instance.energy.eachStart}";
		text.rectTransform.SetParent(energy.rectTransform.parent);
		text.rectTransform.localScale = energy.rectTransform.localScale;
		text.transform.position = energy.transform.position;
		float dx = 0f;
		while (dx < 50f)
		{
			text.rectTransform.anchoredPosition = energy.rectTransform.anchoredPosition - Vector2.up * dx;
			dx += 0.5f;
			yield return null;
		}
		PlayerPrefs.SetInt("TutorialLevels", 1);
		UnityEngine.SceneManagement.SceneManager.LoadScene("Race");
	}
}
