using UnityEngine;

public class InterfaceExample : InterfaceModelBase
{
	public GameObject ButtonStart;

	public GameObject ButtonPause;

	public GameObject ButtonRestart;

	public GameObject ButtonResume;

	public GameObject ButtonLevels;

	public GameObject DialogPanel;

	public override void OnButtonStartClick()
	{
		UnityEngine.Debug.Log("Start");
	}

	public override void OnButtonRestartClick()
	{
		UnityEngine.Debug.Log("Restart");
	}

	public override void OnButtonLevelsClick()
	{
		UnityEngine.Debug.Log("Levels Gallery");
	}

	public override void OnButtonPauseClick()
	{
		UnityEngine.Debug.Log("Pause");
		DialogPanel.SetActive(value: true);
		ButtonPause.SetActive(value: false);
	}

	public override void OnButtonResumeClick()
	{
		UnityEngine.Debug.Log("Resume");
		DialogPanel.SetActive(value: false);
		ButtonPause.SetActive(value: true);
	}
}
