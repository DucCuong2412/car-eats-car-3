using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class IncubatorMG_UI : MonoBehaviour
{
	public GameObject PausePanel;

	public Button PauseBut;

	public Button PauseResumeBut;

	public Button PauseLeaveBut;

	public Button UpBut;

	public Button DownBut;

	public Slider StarSlider;

	[Header("Results")]
	public GameObject resPanel;

	public GameObject winText;

	public GameObject loseText;

	public Button ResLeaveBut;

	[Header("Tutorial")]
	public GameObject TutorialPanel;

	public Button TutoriaBut;

	public GameObject TutorialEvo1;

	public GameObject TutorialEvo2;

	public GameObject TutorialEvo3;

	private IncubatorMG_Controller _controller;

	private int _lastStarCount = -1;

	private void OnEnable()
	{
		_controller = UnityEngine.Object.FindObjectOfType<IncubatorMG_Controller>();
		PauseBut.onClick.AddListener(PausePress);
		UpBut.onClick.AddListener(UpPress);
		DownBut.onClick.AddListener(DownPress);
		PauseResumeBut.onClick.AddListener(PauseResumePress);
		PauseLeaveBut.onClick.AddListener(PauseLeavePress);
		ResLeaveBut.onClick.AddListener(PauseLeavePress);
		TutoriaBut.onClick.AddListener(TutorialPress);
		TutorialPanel.SetActive(value: true);
		_controller.TutorialShow = true;
		TutorialEvo1.SetActive(value: false);
		TutorialEvo2.SetActive(value: false);
		TutorialEvo3.SetActive(value: false);
		switch (Progress.shop.Incubator_EvoStage)
		{
		case 1:
			TutorialEvo1.SetActive(value: true);
			break;
		case 2:
			TutorialEvo2.SetActive(value: true);
			break;
		case 3:
			TutorialEvo3.SetActive(value: true);
			break;
		}
		_lastStarCount = -1;
		StarSlider.value = 0f;
	}

	private void TutorialPress()
	{
		TutorialPanel.SetActive(value: false);
		_controller.TutorialShow = false;
	}

	private void Update()
	{
		if (_lastStarCount != _controller.CurrentStars)
		{
			_lastStarCount = _controller.CurrentStars;
			StarSlider.value = (float)_lastStarCount / (float)_controller.Config.GetStarsToWin();
		}
		if (UnityEngine.Input.GetKeyDown(KeyCode.DownArrow))
		{
			DownPress();
		}
		if (UnityEngine.Input.GetKeyDown(KeyCode.UpArrow))
		{
			UpPress();
		}
	}

	private void UpPress()
	{
		_controller.PressMoveCar(up: true);
	}

	private void DownPress()
	{
		_controller.PressMoveCar(up: false);
	}

	private void PausePress()
	{
		PausePanel.SetActive(value: true);
		_controller.PauseOn = true;
		Time.timeScale = 0f;
	}

	private void PauseResumePress()
	{
		PausePanel.SetActive(value: false);
		_controller.PauseOn = false;
		Time.timeScale = 1f;
	}

	private void PauseLeavePress()
	{
		Time.timeScale = 1f;
		SceneManager.LoadScene("scene_incubator");
	}

	private void OnDisable()
	{
		PauseBut.onClick.RemoveAllListeners();
		UpBut.onClick.RemoveAllListeners();
		DownBut.onClick.RemoveAllListeners();
		PauseResumeBut.onClick.RemoveAllListeners();
		PauseLeaveBut.onClick.RemoveAllListeners();
		ResLeaveBut.onClick.RemoveAllListeners();
	}
}
