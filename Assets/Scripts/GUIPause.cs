using SmartLocalization;
using System;
using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class GUIPause : MonoBehaviour
{
	public Results_Glogal_controller RGC;

	public GameObject pauseGameObject;

	public GameObject GUITransparent;

	public Text levelNumberLable;

	public Text levelLable;

	public Button restartReg;

	public Button levelsReg;

	public Button levelsSpec;

	public Toggle SoundBtn;

	public Toggle MusicBtn;

	[Header("Badges")]
	public GameObject Badges;

	public Image Token1;

	public Image Token2;

	public Image Token3;

	[Header("Badges Undeground")]
	public GameObject BadgesUndeground;

	public Image Token1Undeground;

	public Image Token2Undeground;

	public Image Token3Undeground;

	public Animator Anim;

	public Text FuelCounter;

	public GameObject inf;

	private Action onResumeAction;

	private Action onRestartAction;

	private Action onLevelsAction;

	private Action Sound;

	private Action Music;

	private bool _isResumeClicked;

	private bool hideMGbtn;

	private int hash_IsON = Animator.StringToHash("isON");

	private void OnEnable()
	{
		Badges.SetActive(value: false);
		BadgesUndeground.SetActive(value: false);
		if (Progress.shop.endlessLevel && !Progress.shop.bossLevel)
		{
			levelsReg.gameObject.SetActive(value: false);
			restartReg.gameObject.SetActive(value: false);
			levelsSpec.gameObject.SetActive(value: true);
			return;
		}
		if (Progress.shop.ArenaNew || Progress.shop.EsterLevelPlay)
		{
			levelsReg.gameObject.SetActive(value: false);
			restartReg.gameObject.SetActive(value: false);
			levelsSpec.gameObject.SetActive(value: true);
			return;
		}
		levelsReg.gameObject.SetActive(value: true);
		restartReg.gameObject.SetActive(value: true);
		levelsSpec.gameObject.SetActive(value: false);
		if (Progress.levels.InUndeground)
		{
			BadgesUndeground.SetActive(value: true);
		}
		else
		{
			Badges.SetActive(value: true);
		}
	}

	public void Show(string levelNumber, Action onResumeAction, Action onRestartAction, Action onLevelsAction, Action Sound, Action Music, bool isSound, bool isMusic)
	{
		if (RaceLogic.instance.gui.CageBut.gameObject.activeSelf)
		{
			hideMGbtn = true;
			RaceLogic.instance.gui.CageBut.gameObject.SetActive(value: false);
		}
		if (Progress.gameEnergy.isInfinite)
		{
			FuelCounter.text = string.Empty;
			inf.SetActive(value: true);
		}
		else
		{
			FuelCounter.text = Progress.gameEnergy.energy.ToString();
			inf.SetActive(value: false);
		}
		pauseGameObject.SetActive(value: true);
		GUITransparent.SetActive(value: true);
		ChangeLevellabel(levelNumber);
		SoundBtn.isOn = !isSound;
		MusicBtn.isOn = !isMusic;
		SetTokens();
		Badges.SetActive(value: false);
		BadgesUndeground.SetActive(value: false);
		if (!Progress.shop.bossLevel && !Progress.shop.ArenaNew && !Progress.shop.EsterLevelPlay)
		{
			if (Progress.levels.InUndeground)
			{
				BadgesUndeground.SetActive(value: true);
			}
			else
			{
				Badges.SetActive(value: true);
			}
		}
		this.onResumeAction = onResumeAction;
		this.onRestartAction = onRestartAction;
		this.onLevelsAction = onLevelsAction;
		this.Music = Music;
		this.Sound = Sound;
		Anim.SetBool(hash_IsON, value: true);
	}

	public void SetTokens()
	{
		int counterEmemys = RaceLogic.instance.CounterEmemys;
		float num = (float)RaceLogic.instance.MaxEnemysInLevel / 100f;
		int num2 = (int)(num * (float)DifficultyConfig.instance.GetCurrentLevel().PercTo1);
		int num3 = (int)(num * (float)DifficultyConfig.instance.GetCurrentLevel().PercTo2);
		int num4 = (int)(num * (float)DifficultyConfig.instance.GetCurrentLevel().PercTo3);
		Token1.fillAmount = 0f;
		Token2.fillAmount = 0f;
		Token3.fillAmount = 0f;
		Token1Undeground.fillAmount = 0f;
		Token2Undeground.fillAmount = 0f;
		Token3Undeground.fillAmount = 0f;
		if (Progress.levels.InUndeground)
		{
			if (counterEmemys <= num2)
			{
				Token1Undeground.fillAmount = 100f / (float)num2 * (float)counterEmemys / 100f;
				return;
			}
			if (counterEmemys <= num3)
			{
				if (Token1Undeground.fillAmount != 1f)
				{
					Token1Undeground.fillAmount = 1f;
				}
				Token2Undeground.fillAmount = 100f / (float)(num3 - num2) * (float)(counterEmemys - num2) / 100f;
				return;
			}
			if (Token1Undeground.fillAmount <= 1f)
			{
				Token1Undeground.fillAmount = 1f;
			}
			if (Token2Undeground.fillAmount <= 1f)
			{
				Token2Undeground.fillAmount = 1f;
			}
			Token3Undeground.fillAmount = 100f / (float)(num4 - num3) * (float)(counterEmemys - num3) / 100f;
		}
		else if (counterEmemys <= num2)
		{
			Token1.fillAmount = 100f / (float)num2 * (float)counterEmemys / 100f;
		}
		else if (counterEmemys <= num3)
		{
			if (Token1.fillAmount != 1f)
			{
				Token1.fillAmount = 1f;
			}
			Token2.fillAmount = 100f / (float)(num3 - num2) * (float)(counterEmemys - num2) / 100f;
		}
		else
		{
			if (Token1.fillAmount <= 1f)
			{
				Token1.fillAmount = 1f;
			}
			if (Token2.fillAmount <= 1f)
			{
				Token2.fillAmount = 1f;
			}
			Token3.fillAmount = 100f / (float)(num4 - num3) * (float)(counterEmemys - num3) / 100f;
		}
	}

	public void ChangeLevellabel(string num)
	{
		if (!Progress.shop.Tutorial)
		{
			if (!Progress.shop.EsterLevelPlay)
			{
				if (Progress.shop.ArenaNew)
				{
					levelLable.text = LanguageManager.Instance.GetTextValue("ARENA *").Replace("*", string.Empty);
					levelNumberLable.text = levelNumberLable.text.Replace("*", RaceLogic.instance.pack.ToString());
				}
				else if (!Progress.shop.endlessLevel && !Progress.shop.bossLevel)
				{
					levelLable.text = LanguageManager.Instance.GetTextValue("LEVEL *").Replace("*", string.Empty);
					levelNumberLable.text = levelNumberLable.text.Replace("*", num);
				}
				else if (!Progress.shop.bossLevel)
				{
					levelNumberLable.text = string.Empty;
					levelLable.text = LanguageManager.Instance.GetTextValue("SPECIAL MISSION");
				}
				else if (Progress.shop.bossLevel)
				{
					levelNumberLable.text = string.Empty;
					levelLable.text = LanguageManager.Instance.GetTextValue("BOSS LEVEL");
				}
			}
			else
			{
				levelNumberLable.text = string.Empty;
				levelLable.text = LanguageManager.Instance.GetTextValue("EASTER EGG HUNT");
			}
		}
		else
		{
			levelNumberLable.text = string.Empty;
			levelLable.text = LanguageManager.Instance.GetTextValue("TUTORIAL");
		}
	}

	public void Hide()
	{
		GUITransparent.SetActive(value: false);
		pauseGameObject.SetActive(value: false);
		if (hideMGbtn)
		{
			hideMGbtn = false;
			RaceLogic.instance.gui.CageBut.gameObject.SetActive(value: true);
		}
	}

	public void PlayAnimHide()
	{
		Anim.SetBool(hash_IsON, value: false);
	}

	public void onResumeClick()
	{
		if (!_isResumeClicked && onResumeAction != null)
		{
			onResumeAction();
		}
	}

	public void onRestartClick()
	{
		if (GameEnergyLogic.isEnoughForRace)
		{
			StartCoroutine(StartFuelAnim());
		}
		else if (onRestartAction != null)
		{
			onRestartAction();
		}
	}

	private IEnumerator StartFuelAnim()
	{
		_isResumeClicked = true;
		RGC.AnimfuelText.text = "-" + PriceConfig.instance.energy.eachStart.ToString();
		RGC.animfuel.Play();
		RGC.animfuel["bodov_PAUSE_decreasFuel"].speed = 0.2f;
		yield return null;
		_isResumeClicked = false;
		if (onRestartAction != null)
		{
			onRestartAction();
		}
	}

	public void onLevelsClick()
	{
		if (!_isResumeClicked)
		{
			if (Progress.shop.Tutorial)
			{
				Progress.shop.Tutorial = false;
			}
			if (onLevelsAction != null)
			{
				onLevelsAction();
			}
		}
	}

	public void OnMusicClick()
	{
		if (Music != null)
		{
			Music();
		}
	}

	public void OnSoundClick()
	{
		if (Sound != null)
		{
			Sound();
		}
	}
}
