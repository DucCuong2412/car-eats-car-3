using System;
using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class EsterController : MonoBehaviour
{
	public Button IncubatorBut;

	public Button MapEventBut;

	public Text MapEventEndTimer;

	public Animator MapFuncObj;

	public GameObject PanelObj;

	public GameObject IntroObj;

	public GameObject OffersObj;

	public Button CloseBut;

	//public Text BalanceEggs;

	public Text TimerToEnd;

	public Animator Anim;

	public GameObject IntroLoadingObj;

	public Button IntroPlayBut;

	[Header("Offers")]
	public Button OffersPlayBut;

	public Button OffersPlayVideoBut;

	public Text OffersPlayVideoTimer;

	public Button Сrystal1But;

	public Button Сrystal2But;

	public Button Сrystal3But;

	public Button Сrystal4But;

	public Button Сrystal1ButFree;

	public Button Сrystal2ButFree;

	public Button Сrystal3ButFree;

	public Button Сrystal4ButFree;

	public GameObject Сrystal1ButNoMoney;

	public GameObject Сrystal2ButNoMoney;

	public GameObject Сrystal3ButNoMoney;

	public GameObject Сrystal4ButNoMoney;

	public Text Сrystal1ButText;

	public Text Сrystal2ButText;

	public Text Сrystal3ButText;

	public Text Сrystal4ButText;

	public Button X2BuyBut;

	public GameObject X2NoVideoObl;

	public GameObject X2TimerObl;

	public Text X2Timer;

	public Slider X2Slider;

	public Button FreeBuyBut;

	public GameObject FreeTimerObl;

	public Text FreeTimer;

	private const string zero = "0";

	private const string dots = ":";

	private const string days = "d ";

	private Coroutine TimeX2Corut;

	private Coroutine TimeEndTimeCorut;

	private Coroutine TimeNextPlayCorut;

	private Action Act;

	private NewControllerForButtonPlayOnMap _controllerForPlayButton;

	private void Start()
	{
		PanelObj.SetActive(value: false);
		Progress.shop.EsterLevelPlay = false;
		X2BuyBut.gameObject.SetActive(!Progress.shop.EsterX2TimeActivate);
		if (GetTimeNextPlayMinutes() <= 0 && GetTimeNextPlayHours() <= 0 && GetTimeNextPlayDay() <= 0 && GetTimeNextPlaySeconds() <= 0)
		{
			base.gameObject.SetActive(value: false);
			MapEventBut.gameObject.SetActive(value: false);
			return;
		}
		_controllerForPlayButton = UnityEngine.Object.FindObjectOfType<NewControllerForButtonPlayOnMap>();
		if (Progress.shop.EsterShowIntroPlay || Progress.shop.EsterShowOnMap)
		{
			Show();
		}
		Progress.shop.EsterShowOnMap = false;
		TimeEndTimeCorut = StartCoroutine(TimerToEndCorutine());
		MapEventBut.gameObject.SetActive(value: true);
		MapEventBut.onClick.AddListener(Show);
		if (Progress.shop.EsterForMap)
		{
			Show();
			Progress.shop.EsterForMap = false;
		}
	}

	public void Show()
	{
		Game.OnStateChange(Game.gameState.OpenWindow);
		MapFuncObj.SetBool("is_ON", value: false);
		PanelObj.SetActive(value: true);
		IntroObj.SetActive(value: true);
		OffersObj.SetActive(value: false);
		IntroLoadingObj.SetActive(value: true);
		IntroPlayBut.gameObject.SetActive(value: false);
		StartCoroutine(DelayToOffers());
		CloseBut.onClick.AddListener(ClosePress);
		X2BuyBut.onClick.AddListener(X2EggsVideoPress);
		FreeBuyBut.onClick.AddListener(ShowVideoCoin);
		IncubatorBut.onClick.AddListener(IncubatorPress);
		OffersPlayBut.onClick.AddListener(PlayPress);
		OffersPlayVideoBut.onClick.AddListener(PlayVideoPress);
		Сrystal1But.onClick.AddListener(Сrystal1Press);
		Сrystal2But.onClick.AddListener(Сrystal2Press);
		Сrystal3But.onClick.AddListener(Сrystal3Press);
		Сrystal4But.onClick.AddListener(Сrystal4Press);
		Сrystal1ButFree.onClick.AddListener(Сrystal1FreePress);
		Сrystal2ButFree.onClick.AddListener(Сrystal2FreePress);
		Сrystal3ButFree.onClick.AddListener(Сrystal3FreePress);
		Сrystal4ButFree.onClick.AddListener(Сrystal4FreePress);
		Сrystal1ButText.text = ConfigForEster.instance.Config.price1.ToString();
		Сrystal2ButText.text = ConfigForEster.instance.Config.price2.ToString();
		Сrystal3ButText.text = ConfigForEster.instance.Config.price3.ToString();
		Сrystal4ButText.text = ConfigForEster.instance.Config.price4.ToString();
		Сrystal1ButNoMoney.GetComponentInChildren<Text>().text = ConfigForEster.instance.Config.price1.ToString();
		Сrystal2ButNoMoney.GetComponentInChildren<Text>().text = ConfigForEster.instance.Config.price2.ToString();
		Сrystal3ButNoMoney.GetComponentInChildren<Text>().text = ConfigForEster.instance.Config.price3.ToString();
		Сrystal4ButNoMoney.GetComponentInChildren<Text>().text = ConfigForEster.instance.Config.price4.ToString();
		CheckEags();
		X2TimerObl.SetActive(Progress.shop.EsterX2TimeActivate);
		X2BuyBut.gameObject.SetActive(!Progress.shop.EsterX2TimeActivate);
		//BalanceEggs.text = Progress.shop.EsterEggsBalance.ToString();
		if (GetTimeNextPlay() <= 0)
		{
			OffersPlayBut.gameObject.SetActive(value: true);
			OffersPlayVideoBut.gameObject.SetActive(value: false);
		}
		else
		{
			OffersPlayBut.gameObject.SetActive(value: false);
			OffersPlayVideoBut.gameObject.SetActive(value: true);
			TimeNextPlayCorut = StartCoroutine(TimerNextPlayCorutine());
		}
		_controllerForPlayButton.ReinitListenersValentine();
	}

	private void CheckEags()
	{
		Сrystal1ButFree.transform.parent.gameObject.SetActive(value: false);
		Сrystal2ButFree.transform.parent.gameObject.SetActive(value: false);
		Сrystal3ButFree.transform.parent.gameObject.SetActive(value: false);
		Сrystal4ButFree.transform.parent.gameObject.SetActive(value: false);
		if (Progress.shop.Incubator_Ruby1CanGateFree)
		{
			Сrystal1ButFree.transform.parent.gameObject.SetActive(value: true);
			Сrystal1But.gameObject.SetActive(value: false);
			Сrystal1ButNoMoney.SetActive(value: false);
		}
		else if (ConfigForEster.instance.Config.price1 < Progress.shop.EsterEggsBalance)
		{
			Сrystal1But.gameObject.SetActive(value: true);
			Сrystal1ButNoMoney.SetActive(value: false);
		}
		else
		{
			Сrystal1But.gameObject.SetActive(value: false);
			Сrystal1ButNoMoney.SetActive(value: true);
		}
		if (Progress.shop.Incubator_Ruby2CanGateFree)
		{
			Сrystal2ButFree.transform.parent.gameObject.SetActive(value: true);
			Сrystal2But.gameObject.SetActive(value: false);
			Сrystal2ButNoMoney.SetActive(value: false);
		}
		else if (ConfigForEster.instance.Config.price2 < Progress.shop.EsterEggsBalance)
		{
			Сrystal2But.gameObject.SetActive(value: true);
			Сrystal2ButNoMoney.SetActive(value: false);
		}
		else
		{
			Сrystal2But.gameObject.SetActive(value: false);
			Сrystal2ButNoMoney.SetActive(value: true);
		}
		if (Progress.shop.Incubator_Ruby3CanGateFree)
		{
			Сrystal3ButFree.transform.parent.gameObject.SetActive(value: true);
			Сrystal3But.gameObject.SetActive(value: false);
			Сrystal3ButNoMoney.SetActive(value: false);
		}
		else if (ConfigForEster.instance.Config.price3 < Progress.shop.EsterEggsBalance)
		{
			Сrystal3But.gameObject.SetActive(value: true);
			Сrystal3ButNoMoney.SetActive(value: false);
		}
		else
		{
			Сrystal3But.gameObject.SetActive(value: false);
			Сrystal3ButNoMoney.SetActive(value: true);
		}
		if (Progress.shop.Incubator_Ruby4CanGateFree)
		{
			Сrystal4ButFree.transform.parent.gameObject.SetActive(value: true);
			Сrystal4But.gameObject.SetActive(value: false);
			Сrystal4ButNoMoney.SetActive(value: false);
		}
		else if (ConfigForEster.instance.Config.price4 < Progress.shop.EsterEggsBalance)
		{
			Сrystal4But.gameObject.SetActive(value: true);
			Сrystal4ButNoMoney.SetActive(value: false);
		}
		else
		{
			Сrystal4But.gameObject.SetActive(value: false);
			Сrystal4ButNoMoney.SetActive(value: true);
		}
	}

	private IEnumerator TimerToEndCorutine()
	{
		while (GetTimeNextPlayMinutes() > 0 || GetTimeNextPlayHours() > 0 || GetTimeNextPlayDay() > 0 || GetTimeNextPlaySeconds() > 0)
		{
			TimerToEnd.text = GetTimeToEnd();
			MapEventEndTimer.text = GetTimeToEnd();
			float t = 1f;
			while (t > 0f)
			{
				t -= Time.deltaTime;
				yield return null;
			}
		}
	}

	private string GetTimeToEnd()
	{
		string text = (GetTimeNextPlayDay() <= 9) ? ("0" + GetTimeNextPlayDay() + "d ") : (GetTimeNextPlayDay() + "d ");
		if (GetTimeNextPlayHours() > 9)
		{
			text = text + GetTimeNextPlayHours() + ":";
		}
		else
		{
			string text2 = text;
			text = text2 + "0" + GetTimeNextPlayHours() + ":";
		}
		if (GetTimeNextPlayMinutes() > 9)
		{
			text = text + GetTimeNextPlayMinutes() + ":";
		}
		else
		{
			string text2 = text;
			text = text2 + "0" + GetTimeNextPlayMinutes() + ":";
		}
		if (GetTimeNextPlaySeconds() > 9)
		{
			return text + GetTimeNextPlaySeconds();
		}
		return text + "0" + GetTimeNextPlaySeconds();
	}

	private int GetTimeNextPlayDay()
	{
		return (Progress.shop.EsterEndTime - DateTime.UtcNow).Days;
	}

	private int GetTimeNextPlayHours()
	{
		return (Progress.shop.EsterEndTime - DateTime.UtcNow).Hours;
	}

	private int GetTimeNextPlayMinutes()
	{
		return (Progress.shop.EsterEndTime - DateTime.UtcNow).Minutes;
	}

	private int GetTimeNextPlaySeconds()
	{
		return (Progress.shop.EsterEndTime - DateTime.UtcNow).Seconds;
	}

	private IEnumerator DelayToOffers()
	{
		float t = 1.5f;
		while (t > 0f)
		{
			t -= Time.deltaTime;
			yield return null;
		}
		if (Progress.shop.EsterShowIntroPlay)
		{
			Progress.shop.EsterShowIntroPlay = false;
			IntroLoadingObj.SetActive(value: false);
			IntroPlayBut.gameObject.SetActive(value: true);
			IntroPlayBut.onClick.AddListener(IntroPlayPress);
		}
		else
		{
			IntroPlayPress();
		}
	}

	private void IncubatorPress()
	{
		SceneManager.LoadScene("scene_incubator");
	}

	private void PlayPress()
	{
		HideFunk();
	}

	private void PlayVideoPress()
	{
		Act = delegate
		{
			Progress.shop.EsterLastPlayTime = DateTime.UtcNow;
			OffersPlayVideoBut.gameObject.SetActive(value: true);
			TimeNextPlayCorut = StartCoroutine(TimerNextPlayCorutine());
			OffersPlayVideoBut.interactable = true;
			HideFunk();
		};
		OffersPlayVideoBut.interactable = false;
		NewWrapperForAppodeal.instance.ShowVideo(delegate(bool sucsess)
		{
			if (sucsess)
			{
				Act();
			}
			else
			{
				OffersPlayVideoBut.interactable = true;
			}
		}, delegate
		{
			GameObject gameObject = UnityEngine.Object.Instantiate(Resources.Load("noVideoBox", typeof(GameObject))) as GameObject;
			OffersPlayVideoBut.interactable = true;
		}, delegate
		{
			OffersPlayVideoBut.interactable = true;
		});
	}

	private void X2EggsVideoPress()
	{
		Act = delegate
		{
			Progress.shop.EsterX2TimeActivate = true;
			X2BuyBut.gameObject.SetActive(value: false);
			X2TimerObl.SetActive(value: true);
		};
		NewWrapperForAppodeal.instance.ShowVideo(delegate(bool sucsess)
		{
			if (sucsess)
			{
				Act();
			}
			else
			{
				X2BuyBut.interactable = true;
			}
		}, delegate
		{
			GameObject gameObject = UnityEngine.Object.Instantiate(Resources.Load("noVideoBox", typeof(GameObject))) as GameObject;
			X2BuyBut.interactable = true;
		}, delegate
		{
			X2BuyBut.interactable = true;
		});
	}

	private IEnumerator TimerX2RevardCorutine()
	{
		while (true)
		{
			int min = GetTimeX2() / 60;
			int sec = GetTimeX2() - min * 60;
			if (min <= 0 && sec <= 0)
			{
				break;
			}
			if (min <= 9)
			{
				X2Timer.text = "0" + min.ToString();
			}
			else
			{
				X2Timer.text = min.ToString();
			}
			if (sec <= 9)
			{
				Text x2Timer = X2Timer;
				x2Timer.text = x2Timer.text + ":0" + sec.ToString();
			}
			else
			{
				Text x2Timer2 = X2Timer;
				x2Timer2.text = x2Timer2.text + ":" + sec.ToString();
			}
			X2Slider.value = (float)min / (float)ConfigForEster.instance.Config.TimeToOpensVideoX2Mins;
			float t = 1f;
			while (t > 0f)
			{
				t -= Time.deltaTime;
				yield return null;
			}
		}
		X2TimerObl.SetActive(value: false);
		X2BuyBut.gameObject.SetActive(value: true);
	}

	public int GetTimeX2()
	{
		return ConfigForEster.instance.Config.TimeToOpensVideoX2Mins * 60 - (int)(DateTime.UtcNow - Progress.shop.EsterX2BuyTime).TotalSeconds;
	}

	private IEnumerator TimerNextPlayCorutine()
	{
		while (true)
		{
			int min = GetTimeNextPlay() / 60;
			int sec = GetTimeNextPlay() - min * 60;
			if (min <= 0 && sec <= 0)
			{
				break;
			}
			if (min <= 9)
			{
				OffersPlayVideoTimer.text = "0" + min.ToString();
			}
			else
			{
				OffersPlayVideoTimer.text = min.ToString();
			}
			if (sec <= 9)
			{
				Text offersPlayVideoTimer = OffersPlayVideoTimer;
				offersPlayVideoTimer.text = offersPlayVideoTimer.text + ":0" + sec.ToString();
			}
			else
			{
				Text offersPlayVideoTimer2 = OffersPlayVideoTimer;
				offersPlayVideoTimer2.text = offersPlayVideoTimer2.text + ":" + sec.ToString();
			}
			float t = 1f;
			while (t > 0f)
			{
				t -= Time.deltaTime;
				yield return null;
			}
		}
		OffersPlayVideoBut.gameObject.SetActive(value: false);
		OffersPlayBut.gameObject.SetActive(value: true);
	}

	public int GetTimeNextPlay()
	{
		return ConfigForEster.instance.Config.TimeToNextPlayMins * 60 - (int)(DateTime.UtcNow - Progress.shop.EsterLastPlayTime).TotalSeconds;
	}

	private void Сrystal1FreePress()
	{
		Progress.shop.Incubator_CountRuby1++;
		Progress.shop.Incubator_Ruby1CanGateFree = false;
		CheckEags();
	}

	private void Сrystal2FreePress()
	{
		Progress.shop.Incubator_CountRuby2++;
		Progress.shop.Incubator_Ruby2CanGateFree = false;
		CheckEags();
	}

	private void Сrystal3FreePress()
	{
		Progress.shop.Incubator_CountRuby4++;
		Progress.shop.Incubator_Ruby3CanGateFree = false;
		CheckEags();
	}

	private void Сrystal4FreePress()
	{
		Progress.shop.Incubator_CountRuby3++;
		Progress.shop.Incubator_Ruby4CanGateFree = false;
		CheckEags();
	}

	private void Сrystal1Press()
	{
		if (ConfigForEster.instance.Config.price1 <= Progress.shop.EsterEggsBalance)
		{
			Progress.shop.EsterEggsBalance -= ConfigForEster.instance.Config.price1;
			Progress.shop.Incubator_CountRuby1++;
		}
		CheckEags();
	}

	private void Сrystal2Press()
	{
		if (ConfigForEster.instance.Config.price2 <= Progress.shop.EsterEggsBalance)
		{
			Progress.shop.EsterEggsBalance -= ConfigForEster.instance.Config.price2;
			Progress.shop.Incubator_CountRuby2++;
		}
		CheckEags();
	}

	private void Сrystal3Press()
	{
		if (ConfigForEster.instance.Config.price3 <= Progress.shop.EsterEggsBalance)
		{
			Progress.shop.EsterEggsBalance -= ConfigForEster.instance.Config.price3;
			Progress.shop.Incubator_CountRuby4++;
		}
		CheckEags();
	}

	private void Сrystal4Press()
	{
		if (ConfigForEster.instance.Config.price4 <= Progress.shop.EsterEggsBalance)
		{
			Progress.shop.EsterEggsBalance -= ConfigForEster.instance.Config.price4;
			Progress.shop.Incubator_CountRuby3++;
		}
		CheckEags();
	}

	private void IntroPlayPress()
	{
		IntroPlayBut.gameObject.SetActive(value: false);
		IntroObj.SetActive(value: false);
		OffersObj.SetActive(value: true);
	}

	private void ClosePress()
	{
		HideFunk();
	}

	private IEnumerator HIDE()
	{
		Game.OnStateChange(Game.gameState.Levels);
		Anim.SetBool("isON", value: false);
		if (TimeX2Corut != null)
		{
			StopCoroutine(TimeX2Corut);
			TimeX2Corut = null;
		}
		if (TimeNextPlayCorut != null)
		{
			StopCoroutine(TimeNextPlayCorut);
			TimeNextPlayCorut = null;
		}
		IncubatorBut.onClick.RemoveAllListeners();
		OffersPlayBut.onClick.RemoveAllListeners();
		OffersPlayVideoBut.onClick.RemoveAllListeners();
		CloseBut.onClick.RemoveAllListeners();
		X2BuyBut.onClick.RemoveAllListeners();
		FreeBuyBut.onClick.RemoveAllListeners();
		Сrystal1But.onClick.RemoveAllListeners();
		Сrystal2But.onClick.RemoveAllListeners();
		Сrystal3But.onClick.RemoveAllListeners();
		Сrystal4But.onClick.RemoveAllListeners();
		Сrystal1ButFree.onClick.RemoveAllListeners();
		Сrystal2ButFree.onClick.RemoveAllListeners();
		Сrystal3ButFree.onClick.RemoveAllListeners();
		Сrystal4ButFree.onClick.RemoveAllListeners();
		yield return 0;
		MapFuncObj.SetBool("is_ON", value: true);
		PanelObj.SetActive(value: false);
	}

	private void HideFunk()
	{
		StartCoroutine(HIDE());
	}

	private void OnDisable()
	{
		MapEventBut.onClick.RemoveAllListeners();
		if (TimeEndTimeCorut != null)
		{
			StopCoroutine(TimeEndTimeCorut);
			TimeEndTimeCorut = null;
		}
	}

	private void Update()
	{
		ruby();
		//BalanceEggs.text = Progress.shop.EsterEggsBalance.ToString();
		if (UnityEngine.Input.GetKeyDown(KeyCode.Escape))
		{
			HideFunk();
		}
	}

	private void ShowVideoCoin()
	{
		Act = delegate
		{
			Progress.shop.EsterEggsBalance += ConfigForEster.instance.Config.priceForVideo;
			Progress.shop.EsterFreeBuyTime = DateTime.Now.ToString();
			Progress.shop.estetForFreeVideo++;
		};
		NewWrapperForAppodeal.instance.ShowVideo(delegate(bool sucsess)
		{
			if (sucsess)
			{
				Act();
			}
			else
			{
				X2BuyBut.interactable = true;
			}
		}, delegate
		{
			GameObject gameObject = UnityEngine.Object.Instantiate(Resources.Load("noVideoBox", typeof(GameObject))) as GameObject;
			X2BuyBut.interactable = true;
		}, delegate
		{
			X2BuyBut.interactable = true;
		});
	}

	private void ruby()
	{
		if (Progress.shop.EsterFreeBuyTime != null)
		{
			if (Progress.shop.estetForFreeVideo == 3)
			{
				if (Progress.shop.EsterFreeBuyTime == string.Empty)
				{
					Progress.shop.EsterFreeBuyTime = DateTime.MinValue.ToString();
				}
				int num = ConfigForEster.instance.Config.TimeToOpensVideoFreeRubiesMins * 60;
				TimeSpan timeSpan = DateTime.Now - DateTime.Parse(Progress.shop.EsterFreeBuyTime);
				double num2 = (double)num - timeSpan.TotalSeconds;
				int num3 = (int)(num2 % 3600.0) / 60;
				int num4 = (int)(num2 % 60.0);
				string text = string.Format("{0}:{1}", (num3 >= 10) ? num3.ToString() : ("0" + num3.ToString()), (num4 >= 10) ? num4.ToString() : ("0" + num4.ToString()));
				if (timeSpan.TotalSeconds > (double)num)
				{
					FreeTimerObl.SetActive(value: false);
					FreeBuyBut.gameObject.SetActive(value: true);
					Progress.shop.estetForFreeVideo = 0;
				}
				else
				{
					FreeTimerObl.SetActive(value: true);
					FreeBuyBut.gameObject.SetActive(value: false);
					FreeTimer.text = text;
				}
			}
		}
		else
		{
			Progress.shop.EsterFreeBuyTime = DateTime.MinValue.ToString();
		}
	}
}
