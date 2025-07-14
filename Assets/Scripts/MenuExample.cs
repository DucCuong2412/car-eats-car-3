using UnityEngine;

public class MenuExample : MenuModelBase
{
	public GameObject ButtonPlay;

	public GameObject ButtonMoreGames;

	public GameObject ButtonSettings;

	public GameObject ButtonClose;

	public GameObject ButtonMusic;

	public GameObject ButtonSound;

	public GameObject ButtonCredits;

	public GameObject ButtonReset;

	public GameObject PanelSettings;

	public GameObject[] CheckMarks;

	private bool isMusic = true;

	private bool isSound = true;

	public override void OnButtonPlayClick()
	{
		UnityEngine.Debug.Log("Play");
	}

	public override void OnButtonMoreGamesClick()
	{
		Application.OpenURL("http://maxmixgames.com");
	}

	public override void OnButtonSettingsClick()
	{
		UnityEngine.Debug.Log("Settings");
		ButtonPlay.SetActive(value: false);
		ButtonMoreGames.SetActive(value: false);
		ButtonSettings.SetActive(value: false);
		PanelSettings.SetActive(value: true);
	}

	public override void OnButtonCloseClick()
	{
		UnityEngine.Debug.Log("Close");
		PanelSettings.SetActive(value: false);
		ButtonPlay.SetActive(value: true);
		ButtonMoreGames.SetActive(value: true);
		ButtonSettings.SetActive(value: true);
	}

	public override void OnButtonButtonMusicClick()
	{
		if (isMusic)
		{
			UnityEngine.Debug.Log("Music off");
			isMusic = false;
			CheckMarks[0].SetActive(value: false);
		}
		else
		{
			UnityEngine.Debug.Log("Music on");
			isMusic = true;
			CheckMarks[0].SetActive(value: true);
		}
	}

	public override void OnButtonButtonSoundClick()
	{
		if (isSound)
		{
			UnityEngine.Debug.Log("Sound off");
			isSound = false;
			CheckMarks[1].SetActive(value: false);
		}
		else
		{
			UnityEngine.Debug.Log("Sound on");
			isSound = true;
			CheckMarks[1].SetActive(value: true);
		}
	}

	public override void OnButtonButtonCreditsClick()
	{
		UnityEngine.Debug.Log("Credits");
	}

	public override void OnButtonButtonResetClick()
	{
		UnityEngine.Debug.Log("Reset");
	}
}
