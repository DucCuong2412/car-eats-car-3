using SmartLocalization;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class IncubatorBuyNow : MonoBehaviour
{
	public List<GameObject> icons = new List<GameObject>();

	public new Text name;

	public Button Clic;

	public GameObject Container;

	private void OnEnable()
	{
		if (Progress.shop.Incubator_CurrentEggNum == 3)
		{
			Container.SetActive(value: false);
			return;
		}
		int count = icons.Count;
		for (int i = 0; i < count; i++)
		{
			icons[i].SetActive(value: false);
		}
		StartCoroutine(wait());
		Clic.onClick.AddListener(Goto);
	}

	private void Update()
	{
		if (Progress.shop.Incubator_EvoStage == 4)
		{
			Container.SetActive(value: false);
		}
	}

	private IEnumerator wait()
	{
		yield return 0;
		switch (Progress.shop.Incubator_CurrentEggNum)
		{
		case 0:
			name.text = LanguageManager.Instance.GetTextValue("RABBITSTER");
			icons[0].SetActive(value: true);
			break;
		case 1:
			name.text = LanguageManager.Instance.GetTextValue("COCKCHAFER");
			icons[1].SetActive(value: true);
			break;
		case 2:
			name.text = LanguageManager.Instance.GetTextValue("SCORPION");
			icons[2].SetActive(value: true);
			break;
		case 4:
			name.text = LanguageManager.Instance.GetTextValue("ALLIGATOR");
			icons[5].SetActive(value: true);
			break;
		case 5:
			name.text = LanguageManager.Instance.GetTextValue("TURTLE");
			icons[4].SetActive(value: true);
			break;
		}
	}

	private void OnDisable()
	{
		Clic.onClick.RemoveAllListeners();
	}

	public void Goto()
	{
		Progress.shop.GotoGarageOutIncubator = true;
		SceneManager.LoadScene("garage_new");
	}
}
