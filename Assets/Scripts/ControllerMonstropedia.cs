using ArabicSupport;
using SmartLocalization;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class ControllerMonstropedia : MonoBehaviour
{
	public Animator AnimatorPlatform;

	public Animator AnimatorCar;

	public Animator AnimatorGate;

	public Animator AnimatorGetReward;

	public Animator AnimatorName;

	private int _isChanged = Animator.StringToHash("isChanged");

	private int _isChange = Animator.StringToHash("isChange");

	private int _isLocked = Animator.StringToHash("isLocked");

	private int _isRewardTaken = Animator.StringToHash("isRewardTaken");

	private int _isNextLocked = Animator.StringToHash("isNextLocked");

	private int _isNextRewardTaken = Animator.StringToHash("isNextRewardTaken");

	private int _isFree = Animator.StringToHash("isFree");

	public List<MonstropediaButton> Buts = new List<MonstropediaButton>();

	public Text NameTxt;

	public Text RewardTxt;

	public Button GetRewardBut;

	public Button x2;

	public GameObject x2anim;

	public GameObject preloader;

	public Button Shop;

	public Button Exit;

	public CounterController FueLabel;

	public Image FuelInfinytyIcon;

	public CounterController coinsLabel;

	public GameObject ShopCanvasTransparent;

	private int lastCar = -1;

	private int newCar = -1;

	private string[] arrSound = new string[8]
	{
		"actor_roar_01_sn",
		"actor_roar_02_sn",
		"actor_roar_03_sn",
		"actor_roar_04_sn",
		"actor_roar_05_sn",
		"actor_roar_06_sn",
		"actor_roar_07_sn",
		"actor_roar_08_sn"
	};

	public List<GameObject> Cars = new List<GameObject>();

	public float delay_for_change_cars;

	private PremiumShopNew _shopWindowModel;

	private Progress.Shop progressShop;

	private int _coins;

	private bool _isEnergyInfinite;

	public List<Animator> listAnimMates = new List<Animator>();

	public int Coins
	{
		get
		{
			return _coins;
		}
		set
		{
			_coins = value;
		}
	}

	private void OnEnable()
	{
		x2.gameObject.SetActive(value: false);
		if (Progress.shop.MonstroCanGetReward == null || Progress.shop.MonstroCanGetReward.Count == 0)
		{
			for (int i = 0; i < Buts.Count; i++)
			{
				Progress.shop.MonstroCanGetReward.Add(item: true);
				Progress.shop.MonstroLocks.Add(item: true);
			}
		}
		lastCar = -1;
		for (int j = 0; j < Progress.shop.MonstroLocks.Count; j++)
		{
			if (!Progress.shop.MonstroLocks[j] && Progress.shop.MonstroCanGetReward[j])
			{
				lastCar = j + 1;
				break;
			}
		}
		if (lastCar == -1)
		{
			for (int k = 0; k < Progress.shop.MonstroLocks.Count; k++)
			{
				if (!Progress.shop.MonstroLocks[k] && !Progress.shop.MonstroCanGetReward[k])
				{
					lastCar = k + 1;
				}
			}
		}
		if (lastCar <= 0)
		{
			lastCar = 1;
		}
		for (int l = 0; l < Cars.Count; l++)
		{
			if (l == lastCar - 1)
			{
				Cars[l].SetActive(value: true);
			}
			else
			{
				Cars[l].SetActive(value: false);
			}
		}
		for (int m = 0; m < Buts.Count; m++)
		{
			Buts[m].SetIco(m + 1);
			Buts[m].SetLock(Progress.shop.MonstroLocks[m]);
			Buts[m].Anim.SetBool(_isLocked, Progress.shop.MonstroLocks[m]);
			Buts[m].Anim.SetBool(_isRewardTaken, !Progress.shop.MonstroCanGetReward[m]);
		}
		PressBut(lastCar);
		x2.onClick.AddListener(x2Clic);
		GetRewardBut.onClick.RemoveAllListeners();
		GetRewardBut.onClick.AddListener(test);
		Exit.onClick.RemoveAllListeners();
		Exit.onClick.AddListener(PressExit);
		Shop.onClick.RemoveAllListeners();
		Shop.onClick.AddListener(OnClicShop);
		x2.gameObject.SetActive(Progress.shop.MonstroCanGetReward[lastCar - 1]);
		AnimatorGetReward.SetBool(_isRewardTaken, !Progress.shop.MonstroCanGetReward[lastCar - 1]);
		AnimatorGetReward.SetBool(_isLocked, Progress.shop.MonstroLocks[lastCar - 1]);
		AnimatorGate.SetBool(_isLocked, Progress.shop.MonstroCanGetReward[lastCar - 1]);
		progressShop = Progress.shop;
		_coins = progressShop.currency;
		coinsLabel.count = Progress.shop.currency.ToString();
		FueLabel.count = Progress.gameEnergy.energy.ToString();
		FueLabel.gameObject.SetActive(!Progress.gameEnergy.isInfinite);
		FuelInfinytyIcon.gameObject.SetActive(Progress.gameEnergy.isInfinite);
		StartCoroutine(preloderHide());
	}

	private void test()
	{
		GetReward();
	}

	private void x2Clic()
	{
		x2.interactable = false;
		AnalyticsManager.LogEvent(EventCategoty.video, "ads_watched", "x2_monstropedia");
		NewWrapperForAppodeal.instance.ShowVideo(delegate(bool sucess)
		{
			if (sucess)
			{
				Sucses();
			}
			else
			{
				NoSucses();
			}
		}, delegate
		{
			NOvideo();
		}, delegate
		{
			NoSucses();
		});
	}

	public void Sucses()
	{
		x2anim.SetActive(value: true);
		x2.interactable = true;
		x2.gameObject.SetActive(value: false);
		GetReward(video: true);
	}

	public void NoSucses()
	{
		x2.interactable = true;
	}

	public void NOvideo()
	{
		x2.interactable = true;
		GameObject gameObject = UnityEngine.Object.Instantiate(Resources.Load("noVideoBox", typeof(GameObject))) as GameObject;
	}

	private void Update()
	{
		if (!Progress.gameEnergy.isInfinite)
		{
			FueLabel.count = Progress.gameEnergy.energy.ToString();
		}
		FueLabel.gameObject.SetActive(!Progress.gameEnergy.isInfinite);
		FuelInfinytyIcon.gameObject.SetActive(Progress.gameEnergy.isInfinite);
		coinsLabel.count = Progress.shop.currency.ToString();
	}

	private void OnDisable()
	{
	}

	private IEnumerator preloderHide()
	{
		yield return new WaitForSeconds(1f);
		PreloaderCanvas q = preloader.GetComponent<PreloaderCanvas>();
		yield return new WaitForSeconds(1f);
		if (q != null)
		{
			q.Hide();
		}
		NameTxt.text = LanguageManager.Instance.GetTextValue(MonstropediaConfig.instance.Price.Infos[lastCar - 1].CarName);
		string code = LocalizationManager.instance.currentlyLanguageCode;
		if (code.Contains("fa-IR"))
		{
			NameTxt.text = ArabicFixer.Fix(NameTxt.text);
		}
		RewardTxt.text = MonstropediaConfig.instance.Price.Infos[lastCar - 1].Reward.ToString();
		preloader.SetActive(value: false);
		Audio.PlayAsyncRandom(arrSound);
	}

	public void SetEnergyLabel(int count)
	{
		FueLabel.count = count.ToString();
	}

	public void SetCoinsLabel(int count)
	{
		coinsLabel.count = count.ToString();
	}

	public void SetFuelInfinytyIcon(bool enable)
	{
		FuelInfinytyIcon.gameObject.SetActive(enable);
		FueLabel.gameObject.SetActive(!enable);
	}

	public void NameSwich()
	{
		AnimatorName.SetBool(_isChanged, value: false);
		NameTxt.text = LanguageManager.Instance.GetTextValue(MonstropediaConfig.instance.Price.Infos[lastCar - 1].CarName);
		string currentlyLanguageCode = LocalizationManager.instance.currentlyLanguageCode;
		if (currentlyLanguageCode.Contains("fa-IR"))
		{
			NameTxt.text = ArabicFixer.Fix(NameTxt.text);
		}
	}

	public void CarSwich()
	{
		AnimatorCar.SetBool(_isChanged, value: false);
		AnimatorPlatform.SetBool(_isChanged, value: false);
		int num = lastCar;
		lastCar = newCar;
		newCar = -1;
		if (!Progress.shop.MonstroLocks[lastCar - 1] && Progress.shop.MonstroCanGetReward[lastCar - 1])
		{
			RewardTxt.text = MonstropediaConfig.instance.Price.Infos[lastCar - 1].Reward.ToString();
		}
		AnimatorGate.SetBool(_isChange, value: true);
		AnimatorGate.SetBool(_isNextLocked, Progress.shop.MonstroCanGetReward[lastCar - 1]);
		StartCoroutine(delay(Progress.shop.MonstroCanGetReward[lastCar - 1], Progress.shop.MonstroLocks[lastCar - 1]));
		if (Progress.shop.MonstroLocks[num - 1] || Progress.shop.MonstroCanGetReward[num - 1] || !Progress.shop.MonstroLocks[lastCar - 1] || !Progress.shop.MonstroCanGetReward[lastCar - 1])
		{
			AnimatorGetReward.SetBool(_isChanged, value: true);
			AnimatorGetReward.SetBool(_isNextLocked, Progress.shop.MonstroLocks[lastCar - 1]);
			AnimatorGetReward.SetBool(_isNextRewardTaken, !Progress.shop.MonstroCanGetReward[lastCar - 1]);
			AnimatorGetReward.SetBool(_isRewardTaken, !Progress.shop.MonstroCanGetReward[lastCar - 1]);
			if (Progress.shop.MonstroCanGetReward[lastCar - 1] && !Progress.shop.MonstroLocks[lastCar - 1])
			{
				x2.gameObject.SetActive(value: true);
			}
			else
			{
				x2.gameObject.SetActive(value: false);
			}
		}
		AnimatorName.SetBool(_isChanged, value: true);
	}

	private IEnumerator delay(bool isLocked, bool isLokd)
	{
		int t2 = 8;
		while (t2 > 0)
		{
			t2--;
			yield return null;
		}
		AnimatorGate.SetBool(_isChange, value: false);
		AnimatorGate.SetBool(_isLocked, isLocked);
		AnimatorGetReward.SetBool(_isChanged, value: false);
		AnimatorGetReward.SetBool(_isLocked, isLokd);
		t2 = 8;
		while (t2 > 0)
		{
			t2--;
			yield return null;
		}
		RewardTxt.text = MonstropediaConfig.instance.Price.Infos[lastCar - 1].Reward.ToString();
	}

	public void PressBut(int index)
	{
		if (lastCar != index)
		{
			x2.gameObject.SetActive(value: false);
			Audio.PlayAsync("monstropedia_elevator");
			AnimatorCar.SetBool(_isChanged, value: true);
			AnimatorPlatform.SetBool(_isChanged, value: true);
			StartCoroutine(changeCars(index));
			newCar = index;
		}
	}

	private IEnumerator changeCars(int index)
	{
		yield return new WaitForSeconds(delay_for_change_cars);
		for (int i = 0; i < Cars.Count; i++)
		{
			if (i == index - 1)
			{
				Cars[i].SetActive(value: true);
			}
			else
			{
				Cars[i].SetActive(value: false);
			}
		}
	}

	private void OnClicShop()
	{
		Audio.Play("gui_button_02_sn");
		ShowBuyCanvasWindow(isCoins: true);
	}

	public void ShowBuyCanvasWindow(bool isCoins = false)
	{
		StartCoroutine(LoadBuyCanvasWindow(isCoins));
	}

	public IEnumerator LoadBuyCanvasWindow(bool isCoins = false)
	{
		if (_shopWindowModel == null)
		{
			yield return StartCoroutine(InitShopCanvasWindows());
		}
		ShowGetCoinsAndFuel();
	}

	private IEnumerator InitShopCanvasWindows()
	{
		SceneManager.LoadScene("premium_shop", LoadSceneMode.Additive);
		yield return 0;
		GameObject go = UnityEngine.Object.FindObjectOfType<PremiumShopNew>().gameObject;
		PremiumShopNew view = _shopWindowModel = go.GetComponent<PremiumShopNew>();
	}

	private void SetShopCanvasTransparent(bool enable)
	{
		SetCanvasTransparent(enable);
		if (!enable)
		{
			GameObject gameObject = GameObject.Find("ShopCanvasWindows(Clone)");
			if (gameObject != null)
			{
				UnityEngine.Object.Destroy(gameObject);
			}
			_shopWindowModel = null;
		}
	}

	public void SetCanvasTransparent(bool enable)
	{
		ShopCanvasTransparent.SetActive(enable);
	}

	private void ShowGetCoinsAndFuel()
	{
		progressShop.currency = Coins;
		_shopWindowModel.gameObject.SetActive(value: true);
	}

	private void PressExit()
	{
		if (progressShop.Monstroinlevel)
		{
			Results_new_controller results_new_controller = UnityEngine.Object.FindObjectOfType<Results_new_controller>();
			if (results_new_controller != null)
			{
				results_new_controller.RubyText.count = Progress.shop.currency.ToString();
			}
			progressShop.Monstroinlevel = false;
			base.transform.parent.parent.gameObject.SetActive(value: false);
		}
		else
		{
			Audio.Play("gui_button_02_sn");
			if (Progress.levels.InUndeground)
			{
				Game.LoadLevel("scene_underground_map_new");
			}
			else
			{
				Game.LoadLevel("map_new");
			}
		}
	}

	private IEnumerator RubiesLerp(int time)
	{
		int t = 0;
		int temp = (time < 500) ? 10 : 100;
		while (t < time)
		{
			t += temp;
			Progress.shop.currency += temp;
			yield return null;
		}
	}

	private void GetReward(bool video = false)
	{
		GameCenter.OnSaveFriend();
		if (lastCar != -1)
		{
			AnalyticsManager.LogEvent(EventCategoty.monstropedia, "monster saved", "enemy_" + lastCar.ToString());
		}
		AnimatorGate.SetBool(_isLocked, value: false);
		AnimatorGetReward.SetBool(_isRewardTaken, value: true);
		x2.gameObject.SetActive(value: false);
		Progress.shop.MonstroCanGetReward[lastCar - 1] = false;
		if (!video)
		{
			StartCoroutine(RubiesLerp(MonstropediaConfig.instance.Price.Infos[lastCar - 1].Reward));
		}
		else
		{
			StartCoroutine(RubiesLerp(MonstropediaConfig.instance.Price.Infos[lastCar - 1].Reward * 2));
		}
		StartCoroutine(Delay());
		Buts[lastCar - 1].SetLock(Progress.shop.MonstroLocks[lastCar - 1]);
		Buts[lastCar - 1].Anim.SetBool(_isRewardTaken, !Progress.shop.MonstroCanGetReward[lastCar - 1]);
		foreach (Animator listAnimMate in listAnimMates)
		{
			if (listAnimMate.transform.parent.gameObject.activeSelf)
			{
				listAnimMate.SetBool(_isFree, value: true);
			}
		}
	}

	private IEnumerator Delay()
	{
		float t = 1.5f;
		while (t > 0f)
		{
			t -= Time.deltaTime;
			yield return null;
		}
		coinsLabel.count = Progress.shop.currency.ToString();
	}
}
