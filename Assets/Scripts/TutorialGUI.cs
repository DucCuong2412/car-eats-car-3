using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TutorialGUI : MonoBehaviour
{
	public GameObject tint;

	public Text labelTilt;

	public Text labelNitro;

	public Text labelBomb;

	public GameObject tiltButton1;

	public GameObject tiltButton2;

	public GameObject bombButton;

	public GameObject nitroButton;

	public GUIBoosters boosters;

	public GameObject labelBoosters;

	public Text labelRevive;

	public GameObject[] hideOnTutorial;

	public List<GameObject> hideForTicket = new List<GameObject>();

	public GameObject ticketsInterface;

	public GameObject tapToContinueButton;

	public Text labelTickets;

	public Text labelTapToContinue;

	private IEnumerator scaling;

	public void HideTapToContinue()
	{
		UIWidget[] componentsInChildren = ticketsInterface.GetComponentsInChildren<UIWidget>(includeInactive: true);
		UIWidget[] array = componentsInChildren;
		foreach (UIWidget uIWidget in array)
		{
			uIWidget.depth -= 25;
		}
		StopCoroutine(scaling);
		scaling = null;
		ResetScale(ticketsInterface);
		tapToContinueButton.SetActive(value: false);
		labelTickets.gameObject.SetActive(value: false);
		labelTapToContinue.gameObject.SetActive(value: false);
		tint.SetActive(value: false);
		Time.timeScale = 1f;
		tapToContinueButton.SetActive(value: false);
		Game.OnStateChange(Game.gameState.Race);
		foreach (GameObject item in hideForTicket)
		{
			item.SetActive(value: false);
		}
	}

	public void ShowTicketsTutorial()
	{
		if (PlayerPrefs.GetInt("tutorial_ticket", 0) == 0)
		{
			PlayerPrefs.SetInt("tutorial_ticket", 1);
			Game.OnStateChange(Game.gameState.Tutorial);
			UIWidget[] componentsInChildren = ticketsInterface.GetComponentsInChildren<UIWidget>(includeInactive: true);
			UIWidget[] array = componentsInChildren;
			foreach (UIWidget uIWidget in array)
			{
				uIWidget.depth += 25;
			}
			Time.timeScale = 0f;
			foreach (GameObject item in hideForTicket)
			{
				item.SetActive(value: true);
			}
			labelTickets.gameObject.SetActive(value: true);
			tint.SetActive(value: true);
			scaling = ScaleTutorial(ticketsInterface);
			StartCoroutine(scaling);
			StartCoroutine(ScaleTutorial(labelTapToContinue.gameObject));
			StartCoroutine(waitCanTap());
		}
	}

	private IEnumerator waitCanTap()
	{
		float time = 0f;
		while (time < 2f)
		{
			time += Time.unscaledDeltaTime;
			yield return null;
		}
		labelTapToContinue.gameObject.SetActive(value: true);
		tapToContinueButton.SetActive(value: true);
	}

	private IEnumerator ScaleTutorial(GameObject go)
	{
		float scale = 1f;
		Vector3 localScale = go.transform.localScale;
		float minScale = localScale.x;
		float maxScale = minScale + 0.1f;
		bool up = true;
		while (true)
		{
			if ((!(scale < maxScale) || !up) && (!(scale > minScale) || up))
			{
				up = !up;
				continue;
			}
			yield return null;
			scale += 0.005f * (float)(up ? 1 : (-1));
			go.transform.localScale = Vector3.one * scale;
		}
	}

	private void ResetScale(GameObject go)
	{
		go.transform.localScale = Vector3.one;
	}
}
