using UnityEngine;
using UnityEngine.UI;

public class CloudGameSave : MonoBehaviour
{
	public Text txtLevelCloud;

	public Text txtCoinsCloud;

	public Button yes;

	public Button no;

	public GameObject curProgressDeleteText;

	public GameObject askProgressDeleteText;

	public Canvas canva;

	private static string Level;

	private static int Coins = -1;

	private static int Distance = -1;

	private int _phase;

	private bool _isReplacing;

	public static bool IsOpened;

	public Button YesRepl;

	public Button NoRepl;

	public static void ShowWithText(string level, int coins)
	{
		Level = level;
		Coins = coins;
		if (Progress.shop.foundProgress)
		{
			GameObject gameObject = UnityEngine.Object.Instantiate(Resources.Load("CloudGameSave", typeof(GameObject))) as GameObject;
			IsOpened = true;
			Progress.shop.foundProgress = false;
		}
	}

	private void OnEnable()
	{
		Init();
	}

	private void Update()
	{
		if (UnityEngine.Input.GetKeyDown(KeyCode.Escape))
		{
			CloseWindow();
		}
	}

	private void Init()
	{
		Camera[] array = UnityEngine.Object.FindObjectsOfType<Camera>();
		Camera[] array2 = array;
		foreach (Camera camera in array2)
		{
			if (camera.name == "Functional Camera")
			{
				canva.worldCamera = camera;
			}
		}
		_isReplacing = false;
		_phase = 0;
		askProgressDeleteText.SetActive(value: true);
		curProgressDeleteText.SetActive(value: false);
		if (txtLevelCloud != null)
		{
			txtLevelCloud.text = Level.ToString();
		}
		if (txtCoinsCloud != null)
		{
			txtCoinsCloud.text = Coins.ToString();
		}
		int num = Coins - Progress.shop.currency;
		AddListeners();
	}

	private void AddListeners()
	{
		yes.onClick.AddListener(OnYesClick);
		no.onClick.AddListener(OnNoClick);
		YesRepl.onClick.AddListener(yesReples);
		NoRepl.onClick.AddListener(noReplese);
	}

	private void RemoveListeners()
	{
		yes.onClick.RemoveAllListeners();
		no.onClick.RemoveAllListeners();
		YesRepl.onClick.RemoveAllListeners();
		NoRepl.onClick.RemoveAllListeners();
	}

	private void OnYesClick()
	{
		_phase++;
		if (_phase == 1)
		{
			_isReplacing = true;
			askProgressDeleteText.SetActive(value: false);
			curProgressDeleteText.SetActive(value: true);
			ChangePhase(_phase);
		}
		else if (!_isReplacing)
		{
		}
	}

	private void yesReples()
	{
		UnityEngine.Debug.Log("REPLACE");
		GameCenterWrapper.ReplaceLocalSaveFromCloud();
		UnityEngine.SceneManagement.SceneManager.LoadScene("Start_Preloader");
		Progress.shop.foundProgress = false;
	}

	private void noReplese()
	{
		UnityEngine.Debug.Log("NOT REPLACING");
		GameCenterWrapper.SaveGameSave();
		Progress.shop.foundProgress = false;
		IsOpened = false;
		RemoveListeners();
		base.gameObject.SetActive(value: false);
		Progress.shop.foundProgress = false;
	}

	private void ChangePhase(int phase)
	{
	}

	public void OnNoClick()
	{
		_phase = 0;
		ChangePhase(_phase);
		noReplese();
	}

	public void CloseWindow()
	{
		noReplese();
	}
}
