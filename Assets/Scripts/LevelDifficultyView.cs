using UnityEngine;
using UnityEngine.UI;

public class LevelDifficultyView : MonoBehaviour
{
	public Button Start;

	public Button easy;

	public Button medium;

	public Button hard;

	public Button Exit;

	public Toggle Easy;

	public Toggle Medium;

	public Toggle Hard;

	public CellContainer CC;

	public void ClicExit()
	{
		Start.interactable = true;
		easy.interactable = true;
		medium.interactable = true;
		hard.interactable = true;
		Easy.isOn = true;
		Medium.isOn = false;
		Hard.isOn = false;
		CC = null;
		base.transform.parent.gameObject.SetActive(value: false);
	}

	public void ClicEasy()
	{
		Easy.isOn = true;
		Medium.isOn = false;
		Hard.isOn = false;
		Progress.settings.Easy = true;
		Progress.settings.Medium = false;
		Progress.settings.Hard = false;
	}

	public void ClicMedium()
	{
		Easy.isOn = false;
		Medium.isOn = true;
		Hard.isOn = false;
		Progress.settings.Easy = false;
		Progress.settings.Medium = true;
		Progress.settings.Hard = false;
	}

	public void ClicHard()
	{
		Easy.isOn = false;
		Medium.isOn = false;
		Hard.isOn = true;
		Progress.settings.Easy = false;
		Progress.settings.Medium = false;
		Progress.settings.Hard = true;
	}

	public void ClicStart()
	{
		Start.interactable = false;
		easy.interactable = false;
		medium.interactable = false;
		hard.interactable = false;
		CC.Gogogogo();
	}

	public void OnEnable()
	{
		if (Progress.settings.Easy)
		{
			ClicEasy();
		}
		else if (Progress.settings.Medium)
		{
			ClicMedium();
		}
		else if (Progress.settings.Hard)
		{
			ClicHard();
		}
		easy.onClick.AddListener(ClicEasy);
		medium.onClick.AddListener(ClicMedium);
		hard.onClick.AddListener(ClicHard);
		Start.onClick.AddListener(ClicStart);
		Exit.onClick.AddListener(ClicExit);
	}

	public void OnDisable()
	{
		easy.onClick.RemoveAllListeners();
		medium.onClick.RemoveAllListeners();
		hard.onClick.RemoveAllListeners();
		Start.onClick.RemoveAllListeners();
		Exit.onClick.RemoveAllListeners();
	}
}
