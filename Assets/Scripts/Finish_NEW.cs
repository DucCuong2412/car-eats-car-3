using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class Finish_NEW : MonoBehaviour
{
	public Animation anim;

	private void OnTriggerEnter2D(Collider2D other)
	{
		if (other.tag == "CarEnemy" || other.tag == "Civilians")
		{
			Car2DAIController componentInParent = other.gameObject.transform.parent.parent.GetComponentInParent<Car2DAIController>();
			if (componentInParent != null)
			{
				componentInParent.Death(withReward: false);
			}
			Car2DControlerForBombCar componentInParent2 = other.gameObject.transform.parent.parent.GetComponentInParent<Car2DControlerForBombCar>();
			if (componentInParent2 != null)
			{
				componentInParent2.Death();
			}
		}
		if (other.tag == "CarMain" || other.tag == "CarMainChild")
		{
			if (anim != null)
			{
				anim.Play();
			}
			RaceLogic.instance.car.HealthModule._barrel.Enable = false;
			RaceLogic.instance.car.HealthModule._barrel.enabled = false;
			if (Progress.levels.InUndeground)
			{
				Audio.PlayAsync("finish_cave");
			}
			else
			{
				Audio.PlayAsync("door_closing");
			}
			StartCoroutine(finishhh());
			if (!Progress.shop.Tutorial)
			{
				RaceLogic.instance.OnFinish1();
				return;
			}
			RaceLogic.instance.gui.THUD.Shade.SetActive(value: true);
			Image component = RaceLogic.instance.gui.THUD.Shade.GetComponent<Image>();
			AnalyticsManager.LogEvent(EventCategoty.result_tutorial, "result_tutorial", "finish");
			StartCoroutine(tutorialShade(component));
		}
	}

	private IEnumerator tutorialShade(Image img)
	{
		img.sprite = null;
		img.color = new Color(0f, 0f, 0f, 0f);
		for (int i = 0; i < 10; i++)
		{
			yield return new WaitForSeconds(0.1f);
			Color color = img.color;
			float r = color.r;
			Color color2 = img.color;
			float g = color2.g;
			Color color3 = img.color;
			float b = color3.b;
			Color color4 = img.color;
			img.color = new Color(r, g, b, color4.a + 0.1f);
		}
		Progress.shop.currency = 250;
		Progress.shop.Tutorial = false;
		UnityEngine.SceneManagement.SceneManager.LoadScene("garage_new");
	}

	private IEnumerator finishhh()
	{
		while (RaceLogic.instance.car != null && RaceLogic.instance.car.gameObject.activeSelf)
		{
			RaceLogic.instance.car.transform.position = Vector3.Lerp(RaceLogic.instance.car.transform.position, RaceLogic.instance.car.transform.position + new Vector3(0.2f, 0f, 0f), 1f);
			yield return 0;
		}
		yield return new WaitForSeconds(1f);
		base.gameObject.SetActive(value: false);
	}
}
