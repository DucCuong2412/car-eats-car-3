using AnimationOrTween;
using SmartLocalization;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ShopView : MonoBehaviour
{
	public LocalizedWords localizedWordsShop;

	public Text[] healthPrices = new Text[6];

	public Text[] turboPrices = new Text[6];

	public Text[] wheelsPrices = new Text[6];

	public Text[] enginePrices = new Text[6];

	public Text[] weaponsPrices = new Text[6];

	public GameObject[] gadgetsBtns = new GameObject[5];

	public Image[] rubinsIcons = new Image[5];

	public List<Transform> cogWheels;

	public Animation rollerLeftAnim;

	public Animation rollerRightAnim;

	public Animation shopPanelAnim;

	public Animation premiumPanelAnim;

	public GameObject shopContent;

	public GameObject upgradesPanel;

	public GameObject gadgetsPanel;

	public GameObject upgradeBtnChenge;

	public GameObject gadgetsBtnChenge;

	public GameObject confirmPanel;

	public GameObject confirmTransparent;

	public Text bluePrintItemPrice;

	public Text bluePrintItemName;

	public Text bluePrintItemDescription;

	public GameObject[] upgradeImages = new GameObject[5];

	public GameObject[] gadgetImages = new GameObject[5];

	public GameObject premiumPanel;

	public GameObject premiumBg;

	public GameObject premiumBtn;

	public Camera carCamera;

	public Image BG;

	public GameObject RIGHT;

	public GameObject LEFT;

	public GameObject TV;

	public RawImage TVBackground;

	public RawImage BombIconSprite;

	public GameObject BombRecharger;

	public Transform carSpawnParentTransform;

	public Transform bottom_left_point;

	public Transform top_right_point;

	public Camera ShopCamera;

	public Transform[] BtnsLight;

	public GameObject ShopTransparent;

	public GameObject NewItemIcon;

	public Image FuelInfinytyIcon;

	public Image FuelInfinytyIcon2;

	[Space(10f)]
	[Header("GadgetVideo")]
	public List<Text> GadgetVideoBtnTimes = new List<Text>();

	public List<Text> GadgetVideoTimers = new List<Text>();

	public List<int> GadgetVideoTimesMin = new List<int>();

	private List<IEnumerator> GadgetVideoii = new List<IEnumerator>();

	public int MinusTimeByVideo = 30;

	[Space(10f)]
	[Header("PremiumMenu")]
	public Text Name;

	public GameObject newMarker;

	public GameObject[] PremiumStates = new GameObject[4];

	public GameObject[] PremiumIcons = new GameObject[4];

	public Text tankominatorBenefit;

	private int currentStep;

	private int currentPremium;

	private ShopModel _shopModel;

	public Text[] coinsLabel = new Text[2];

	public Texture2D[] tvBacks = new Texture2D[4];

	public Text[] FueLabel = new Text[2];

	public GameObject ShopCanvasTransparent;

	public Texture2D[] bombIcons = new Texture2D[5];

	public Text BUILD;

	public Text BUY;

	private bool _isTurn;

	private string snd_id = string.Empty;

	public ShopModel shopModel
	{
		get
		{
			if (_shopModel == null)
			{
				_shopModel = (ShopModel)UnityEngine.Object.FindObjectOfType(typeof(ShopModel));
			}
			return _shopModel;
		}
	}

	public void ChangePremium(int step = 0)
	{
		for (int i = 0; i < PremiumIcons.Length; i++)
		{
			PremiumIcons[i].SetActive(value: false);
			PremiumStates[i].SetActive(value: false);
		}
		currentStep += step;
		if (currentStep < 0)
		{
			currentStep = PremiumIcons.Length - 1;
		}
		if (currentStep > PremiumIcons.Length - 1)
		{
			currentStep = 0;
		}
		switch (currentStep)
		{
		case 0:
			currentPremium = 5;
			break;
		case 1:
			currentPremium = 0;
			break;
		case 2:
			currentPremium = 3;
			break;
		case 3:
			currentPremium = 1;
			break;
		case 4:
			currentPremium = 2;
			break;
		case 5:
			currentPremium = 4;
			break;
		default:
			currentPremium = currentStep;
			break;
		}
		if (currentStep == 0)
		{
			newMarker.SetActive(value: true);
		}
		else
		{
			newMarker.SetActive(value: false);
		}
		PremiumIcons[currentStep].SetActive(value: true);
		PremiumStates[currentStep].SetActive(value: true);
		Name.text = GetNameOrDescriptionOfPremium(isName: true, currentStep);
		shopModel.RefreshPremiumPanel(currentPremium);
	}

	public void OnPremiumBuy()
	{
		shopModel.BuyPremiumItemForRealMoney(currentPremium);
	}

	public void OnAllPremiumsBuy()
	{
		shopModel.BuyAllPremiumsForRealMoney();
	}

	public void ShowAnim()
	{
	}

	public void ShowShop()
	{
		premiumPanel.SetActive(value: false);
		shopContent.SetActive(value: true);
		ShowAnim();
		ShopTransparent.SetActive(value: true);
		shopModel.isShopOpen = true;
		SetTVBackgroung(shopModel.currentPack);
		CheckTVIcons();
		ActiveAnimation activeAnimation = ActiveAnimation.Play(shopPanelAnim, "Garage_animation", Direction.Forward);
		ActiveAnimation.Play(rollerLeftAnim, Direction.Forward);
		ActiveAnimation.Play(rollerRightAnim, Direction.Forward);
		activeAnimation.onFinished.Add(new EventDelegate(delegate
		{
		}));
		shopModel.CheckForSpawn(shopModel.CurrentCar);
		Audio.Play("gui_screen_on");
		tankominatorBenefit.text = LanguageManager.Instance.GetTextValue("-*%  cheaper than buying parts separately").Replace("*", PriceConfig.instance.premiumConten.tankominatorBenefit.ToString());
		if (Progress.levels.winArena1 && Progress.levels.winArena2 && Progress.levels.winArena3 && Progress.levels.winArena4)
		{
			shopModel.BuyAllPremiums();
		}
	}

	public void ShowShopPremium()
	{
		premiumPanel.SetActive(value: false);
		shopContent.SetActive(value: true);
		ShowAnim();
		ShopTransparent.SetActive(value: true);
		shopModel.isShopOpen = true;
		SetTVBackgroung(shopModel.currentPack);
		CheckTVIcons();
		ActiveAnimation activeAnimation = ActiveAnimation.Play(shopPanelAnim, "Garage_animation", Direction.Forward);
		ActiveAnimation.Play(rollerLeftAnim, Direction.Forward);
		ActiveAnimation.Play(rollerRightAnim, Direction.Forward);
		shopModel.CheckForSpawn(shopModel.CurrentCar);
		Audio.Play("gui_screen_on");
		tankominatorBenefit.text = LanguageManager.Instance.GetTextValue("-*%  cheaper than buying parts separately").Replace("*", PriceConfig.instance.premiumConten.tankominatorBenefit.ToString());
		if (Progress.levels.winArena1 && Progress.levels.winArena2 && Progress.levels.winArena3 && Progress.levels.winArena4)
		{
			shopModel.BuyAllPremiums();
		}
	}

	public void StartVideoButsCurut()
	{
		if (GadgetVideoii.Count < 5)
		{
			GadgetVideoii.Clear();
			GadgetVideoii.Add(null);
			GadgetVideoii.Add(null);
			GadgetVideoii.Add(null);
			GadgetVideoii.Add(null);
			GadgetVideoii.Add(null);
		}
		for (int i = 0; i < 5; i++)
		{
			GadgetVideoTimers[i].text = Progress.shop.GetTimeToBuy(GadgetVideoTimesMin[i], i);
			string textValue = LanguageManager.Instance.GetTextValue("MIN");
			GadgetVideoBtnTimes[i].text = "-" + MinusTimeByVideo.ToString() + " " + textValue;
			if (GadgetVideoii[i] != null)
			{
				StopCoroutine(GadgetVideoii[i]);
			}
			if (GadgetVideoTimers[i].text == "00:00")
			{
				OpenCloseTimers(i, open: false);
				continue;
			}
			OpenCloseTimers(i);
			GadgetVideoii[i] = iTimerVideo(i);
			StartCoroutine(GadgetVideoii[i]);
		}
	}

	public void StopAllCuruts()
	{
		for (int i = 0; i < GadgetVideoii.Count; i++)
		{
			if (GadgetVideoii[i] != null)
			{
				StopCoroutine(GadgetVideoii[i]);
			}
		}
		GadgetVideoii.Clear();
	}

	private IEnumerator iTimerVideo(int index)
	{
		bool end = false;
		while (!end)
		{
			float dt = 1f;
			while (dt > 0f)
			{
				dt -= Time.unscaledDeltaTime;
				yield return null;
			}
			GadgetVideoTimers[index].text = Progress.shop.GetTimeToBuy(GadgetVideoTimesMin[index], index);
			if (GadgetVideoTimers[index].text == "00:00")
			{
				end = true;
				OpenCloseTimers(index, open: false);
				UnityEngine.Debug.Log("END!!");
			}
		}
	}

	private void OpenCloseTimers(int i, bool open = true)
	{
		DateTime d = DateTime.MinValue;
		switch (i)
		{
		case 0:
			d = Progress.shop.Gadget1Time;
			break;
		case 1:
			d = Progress.shop.Gadget2Time;
			break;
		case 2:
			d = Progress.shop.Gadget3Time;
			break;
		case 3:
			d = Progress.shop.Gadget4Time;
			break;
		case 4:
			d = Progress.shop.Gadget5Time;
			break;
		}
		if (!open && d != DateTime.MinValue && Progress.shop.GetTimeToBuy(GadgetVideoTimesMin[i], i) == "00:00")
		{
			shopModel.GadgetTryBuy(i);
		}
		GadgetVideoBtnTimes[i].transform.parent.gameObject.SetActive(open);
		GadgetVideoTimers[i].transform.parent.parent.gameObject.SetActive(open);
	}

	public void GadgetPressVideo(int index)
	{
		index--;
		UnityEngine.Debug.Log("Press " + index);
		StartVideoButsCurut();
	}

	private void SetTVBackgroung(int num)
	{
		if (num > 4)
		{
			num = 4;
		}
		TVBackground.texture = tvBacks[num - 1];
	}

	public void HideShop()
	{
		shopModel.isShopOpen = false;
		ActiveAnimation.Play(shopPanelAnim, "Garage_animation", Direction.Reverse);
		StartCoroutine(HideShopGate());
		Audio.Play("gui_window_01_sn");
	}

	private IEnumerator HideShopGate()
	{
		yield return Utilities.WaitForRealSeconds(0.5f);
		ActiveAnimation.Play(rollerLeftAnim, Direction.Reverse);
		ActiveAnimation.Play(rollerRightAnim, Direction.Reverse);
		yield return Utilities.WaitForRealSeconds(0.5f);
		ShopTransparent.SetActive(value: false);
		shopContent.SetActive(value: false);
		if (shopModel.onHideCallback != null)
		{
			shopModel.onHideCallback();
		}
	}

	public void RefreshUpgradesProgress()
	{
		for (int i = 0; i < 5; i++)
		{
			SetUpgPrices(i);
			RefreshUpgradeIndicators(i);
		}
	}

	public void RefreshUpgradeIndicators(int currentUpg)
	{
		if (shopModel.CurrentCar == 2)
		{
			for (int i = 0; i < 5; i++)
			{
				shopModel.CurrentCarState.getUpgrade(i);
			}
			shopModel.AddUpgPrices();
		}
		shopModel.upgValue[currentUpg].SetProgress(shopModel.CurrentCarState.upgrades[currentUpg].activeLevel);
		if (currentUpg == 4)
		{
			ChangeBombIcon(shopModel.CurrentCarState.upgrades[currentUpg].activeLevel + 1);
		}
	}

	private void CheckTVIcons()
	{
		ChangeBombIcon(shopModel.CurrentCarState.upgrades[4].activeLevel + 1);
		SetBombRecharger(shopModel.Car(0).gadgets[4].bougth[4]);
	}

	public void ChangeBombIcon(int current)
	{
		BombIconSprite.texture = bombIcons[current - 1];
	}

	public void SetBombRecharger(bool show)
	{
		BombRecharger.SetActive(show);
	}

	public void SetPremiumButtonIcon(bool isShowed)
	{
	}

	public void RefreshUpgradeImages(int currentUpg)
	{
		for (int i = 0; i < 5; i++)
		{
			upgradeImages[i].SetActive(value: false);
			gadgetImages[i].SetActive(value: false);
		}
		upgradeImages[currentUpg].SetActive(value: true);
		BUILD.gameObject.SetActive(value: false);
		BUY.gameObject.SetActive(value: true);
		bluePrintItemName.text = GetNameUpgOrGad(isUpg: true, currentUpg);
		bluePrintItemDescription.text = GetDescriptionUpgOrGad(isUpg: true, currentUpg);
	}

	public void RefreshGadgetImages(int currentGad)
	{
		for (int i = 0; i < 5; i++)
		{
			upgradeImages[i].SetActive(value: false);
			gadgetImages[i].SetActive(value: false);
		}
		BUILD.gameObject.SetActive(value: true);
		BUY.gameObject.SetActive(value: false);
		gadgetImages[currentGad].SetActive(value: true);
		bluePrintItemName.text = GetNameUpgOrGad(isUpg: false, currentGad);
		bluePrintItemDescription.text = GetDescriptionUpgOrGad(isUpg: false, currentGad);
	}

	public void SetCoinsLabel(int count)
	{
		for (int i = 0; i < coinsLabel.Length; i++)
		{
			coinsLabel[i].text = count.ToString();
		}
	}

	public void SetUpgPrices(int currentUpg)
	{
		string empty = string.Empty;
		if (shopModel.CurrentCar == 1 || shopModel.CurrentCar == 2)
		{
			empty = localizedWordsShop.KetTextValue(localizedWordsShop.maximum);
			shopModel.upgValue[currentUpg].SetUpgPriceLabel(empty.ToString(), max: true);
		}
		else if (shopModel.CurrentCarState.upgrades[currentUpg].activeLevel + 1 < 6)
		{
			empty = shopModel.CurrentCarState.upgrades[currentUpg].prices[shopModel.CurrentCarState.upgrades[currentUpg].activeLevel + 1].ToString();
			shopModel.upgValue[currentUpg].SetUpgPriceLabel(empty.ToString());
		}
		else
		{
			empty = localizedWordsShop.KetTextValue(localizedWordsShop.maximum);
			shopModel.upgValue[currentUpg].SetUpgPriceLabel(empty.ToString(), max: true);
		}
	}

	public void AddGadgetsPrices()
	{
		for (int i = 0; i < gadgetsBtns.Length; i++)
		{
			if (shopModel.Car(0).gadgets[i].bougth[i])
			{
				SetGadgetBought(i);
			}
			else
			{
				gadgetsBtns[i].GetComponentInChildren<Text>().text = shopModel.Car(0).gadgets[i].price.ToString();
			}
		}
	}

	public void ChangePanels(bool isUpg = true)
	{
		if (isUpg)
		{
			upgradesPanel.SetActive(value: true);
			gadgetsPanel.SetActive(value: false);
			gadgetsBtnChenge.GetComponentInChildren<Button>().interactable = false;
			upgradeBtnChenge.GetComponentInChildren<Text>().color = new Color32(200, 200, 200, byte.MaxValue);
			Image[] componentsInChildren = upgradeBtnChenge.GetComponentsInChildren<Image>();
			componentsInChildren[0].color = new Color32(0, 103, 0, byte.MaxValue);
			componentsInChildren[1].color = new Color32(39, 138, 17, byte.MaxValue);
			componentsInChildren[2].color = new Color32(0, 66, 0, byte.MaxValue);
			gadgetsBtnChenge.GetComponentInChildren<Button>().interactable = true;
			gadgetsBtnChenge.GetComponentInChildren<Text>().color = new Color32(byte.MaxValue, byte.MaxValue, byte.MaxValue, byte.MaxValue);
			Image[] componentsInChildren2 = gadgetsBtnChenge.GetComponentsInChildren<Image>();
			componentsInChildren2[0].color = new Color32(0, 197, 31, byte.MaxValue);
			componentsInChildren2[1].color = new Color32(204, byte.MaxValue, 135, byte.MaxValue);
			componentsInChildren2[2].color = new Color32(0, 92, 19, byte.MaxValue);
			RefreshUpgradesProgress();
		}
		else
		{
			gadgetsPanel.SetActive(value: true);
			upgradesPanel.SetActive(value: false);
			gadgetsBtnChenge.GetComponentInChildren<Button>().interactable = true;
			upgradeBtnChenge.GetComponentInChildren<Text>().color = new Color32(byte.MaxValue, byte.MaxValue, byte.MaxValue, byte.MaxValue);
			Image[] componentsInChildren3 = upgradeBtnChenge.GetComponentsInChildren<Image>();
			componentsInChildren3[0].color = new Color32(0, 197, 31, byte.MaxValue);
			componentsInChildren3[1].color = new Color32(204, byte.MaxValue, 135, byte.MaxValue);
			componentsInChildren3[2].color = new Color32(0, 92, 19, byte.MaxValue);
			gadgetsBtnChenge.GetComponentInChildren<Button>().interactable = false;
			gadgetsBtnChenge.GetComponentInChildren<Text>().color = new Color32(200, 200, 200, byte.MaxValue);
			Image[] componentsInChildren4 = gadgetsBtnChenge.GetComponentsInChildren<Image>();
			componentsInChildren4[0].color = new Color32(0, 103, 0, byte.MaxValue);
			componentsInChildren4[1].color = new Color32(39, 138, 17, byte.MaxValue);
			componentsInChildren4[2].color = new Color32(0, 66, 0, byte.MaxValue);
			AddGadgetsPrices();
			UnityEngine.Debug.Log("!! LEX START SHOW!!!!!!");
			StartVideoButsCurut();
		}
	}

	public void SetGadgetBought(int index)
	{
		gadgetsBtns[index].GetComponentInChildren<Button>().interactable = false;
		Image[] componentsInChildren = gadgetsBtns[index].GetComponentsInChildren<Image>();
		componentsInChildren[0].color = new Color32(215, 102, 0, byte.MaxValue);
		componentsInChildren[1].color = new Color32(byte.MaxValue, 191, 23, byte.MaxValue);
		componentsInChildren[2].color = new Color32(148, 71, 0, byte.MaxValue);
		gadgetsBtns[index].GetComponentInChildren<Text>().text = localizedWordsShop.KetTextValue(localizedWordsShop.bought);
		gadgetsBtns[index].GetComponentInChildren<Text>().transform.localPosition = new Vector3(-79.03f, -2f);
		gadgetsBtns[index].GetComponentInChildren<Text>().GetComponent<RectTransform>().sizeDelta = new Vector2(118f, 48f);
		rubinsIcons[index].enabled = false;
	}

	public void ShowConfirmPanel(bool enoughMoney, int price)
	{
		confirmPanel.SetActive(value: true);
		confirmTransparent.SetActive(value: true);
		bluePrintItemPrice.text = price.ToString();
	}

	public void HideConfirmPanel()
	{
		confirmPanel.SetActive(value: false);
		confirmTransparent.SetActive(value: false);
	}

	public void ShowPremiumPanel()
	{
		Game.PushInnerState("shop_premium", HidePremiumPanel);
		premiumPanel.SetActive(value: true);
		ChangePremium();
		shopModel.RefreshPremiumPanel(currentPremium);
		ActiveAnimation.Play(premiumPanelAnim, "Garage_animation_premium", Direction.Forward);
		ActiveAnimation.Play(shopPanelAnim, "Garage_animation", Direction.Reverse);
		Audio.PlayAsync("gui_screen_on");
		shopContent.SetActive(value: true);
	}

	public void HidePremiumPanel()
	{
		if (Progress.shop.premiumShop)
		{
			shopContent.SetActive(value: false);
			HideShop();
			Progress.shop.premiumShop = false;
		}
		else
		{
			shopContent.SetActive(value: true);
		}
		Game.PopInnerState("shop_premium", executeClose: false);
		ActiveAnimation.Play(premiumPanelAnim, "Garage_animation_premium", Direction.Reverse);
		ActiveAnimation.Play(shopPanelAnim, "Garage_animation", Direction.Forward);
		StartCoroutine(HidePremium());
		Audio.PlayAsync("gui_screen_on");
		StartVideoButsCurut();
	}

	private IEnumerator HidePremium()
	{
		yield return Utilities.WaitForRealSeconds(1.1f);
		premiumPanel.SetActive(value: false);
	}

	public void SetPremiumButton(int current, bool bougth, bool isEquip)
	{
		switch (current)
		{
		case 4:
			current = 0;
			break;
		case 0:
			current = 1;
			break;
		case 3:
			current = 2;
			break;
		case 1:
			current = 3;
			break;
		case 2:
			current = 4;
			break;
		case 5:
			current = 5;
			break;
		default:
			current = currentStep;
			break;
		}
		if (!bougth)
		{
			Image[] componentsInChildren = premiumBtn.GetComponentsInChildren<Image>();
			componentsInChildren[0].color = new Color32(0, 197, 31, byte.MaxValue);
			componentsInChildren[1].color = new Color32(204, byte.MaxValue, 135, byte.MaxValue);
			componentsInChildren[2].color = new Color32(0, 92, 19, byte.MaxValue);
			premiumBtn.GetComponentInChildren<Text>().text = GetPriceForPremium(currentStep);
		}
		if (bougth && isEquip)
		{
			Image[] componentsInChildren2 = premiumBtn.GetComponentsInChildren<Image>();
			componentsInChildren2[0].color = new Color32(212, 45, 0, byte.MaxValue);
			componentsInChildren2[1].color = new Color32(byte.MaxValue, 176, 154, byte.MaxValue);
			componentsInChildren2[2].color = new Color32(82, 8, 0, byte.MaxValue);
			premiumBtn.GetComponentInChildren<Text>().text = localizedWordsShop.KetTextValue(localizedWordsShop.unequip);
		}
		if (bougth && !isEquip)
		{
			Image[] componentsInChildren3 = premiumBtn.GetComponentsInChildren<Image>();
			componentsInChildren3[0].color = new Color32(0, 150, byte.MaxValue, byte.MaxValue);
			componentsInChildren3[1].color = new Color32(152, 220, byte.MaxValue, byte.MaxValue);
			componentsInChildren3[2].color = new Color32(0, 82, 163, byte.MaxValue);
			premiumBtn.GetComponentInChildren<Text>().text = localizedWordsShop.KetTextValue(localizedWordsShop.equip);
		}
	}

	public void RotateCogWheels()
	{
		_isTurn = true;
		snd_id = Audio.Play("gui_shop_cogweels_sn", Audio.soundVolume, loop: true);
		StartCoroutine(RotateCog());
	}

	private IEnumerator RotateCog()
	{
		if (cogWheels == null)
		{
			yield break;
		}
		while (_isTurn)
		{
			for (int i = 0; i < cogWheels.Count; i++)
			{
				cogWheels[i].Rotate(new Vector3(0f, 0f, ((i % 2 != 0) ? (-1f) : 1f) * 5f));
			}
			yield return null;
		}
	}

	public void StopRotateCogWheels()
	{
		_isTurn = false;
		Audio.Stop(snd_id);
	}

	private void OnGUI()
	{
		initBG();
	}

	public void initBG()
	{
		Vector3 vector = ShopCamera.WorldToScreenPoint(bottom_left_point.position);
		Vector3 vector2 = ShopCamera.WorldToScreenPoint(top_right_point.position);
		Vector2 vector3 = new Vector2(vector2.x - vector.x, vector2.y - vector.y);
		carCamera.pixelRect = new Rect(vector.x, vector.y, vector3.x, vector3.y);
	}

	private string GetNameUpgOrGad(bool isUpg, int count)
	{
		if (isUpg)
		{
			switch (count)
			{
			case 0:
				return localizedWordsShop.KetTextValue(localizedWordsShop.armor);
			case 1:
				return localizedWordsShop.KetTextValue(localizedWordsShop.turbo);
			case 2:
				return localizedWordsShop.KetTextValue(localizedWordsShop.wheels);
			case 3:
				return localizedWordsShop.KetTextValue(localizedWordsShop.engine);
			case 4:
				return localizedWordsShop.KetTextValue(localizedWordsShop.bombs);
			}
		}
		else
		{
			switch (count)
			{
			case 0:
				return localizedWordsShop.KetTextValue(localizedWordsShop.magnet);
			case 1:
				return localizedWordsShop.KetTextValue(localizedWordsShop.ram);
			case 2:
				return localizedWordsShop.KetTextValue(localizedWordsShop.recharger);
			case 3:
				return localizedWordsShop.KetTextValue(localizedWordsShop.shield);
			case 4:
				return localizedWordsShop.KetTextValue(localizedWordsShop.regenerator);
			}
		}
		return "UPGRADE";
	}

	private string GetDescriptionUpgOrGad(bool isUpg, int count)
	{
		if (isUpg)
		{
			switch (count)
			{
			case 0:
				return localizedWordsShop.KetTextValue(localizedWordsShop.armorDescription);
			case 1:
				return localizedWordsShop.KetTextValue(localizedWordsShop.turboDescription);
			case 2:
				return localizedWordsShop.KetTextValue(localizedWordsShop.wheelsDescription);
			case 3:
				return localizedWordsShop.KetTextValue(localizedWordsShop.engineDescription);
			case 4:
				return localizedWordsShop.KetTextValue(localizedWordsShop.bombsDescription);
			}
		}
		else
		{
			switch (count)
			{
			case 0:
				return localizedWordsShop.KetTextValue(localizedWordsShop.magnetDescription);
			case 1:
				return localizedWordsShop.KetTextValue(localizedWordsShop.ramDescription);
			case 2:
				return localizedWordsShop.KetTextValue(localizedWordsShop.rechargerDescription);
			case 3:
				return localizedWordsShop.KetTextValue(localizedWordsShop.shieldDescription);
			case 4:
				return localizedWordsShop.KetTextValue(localizedWordsShop.regeneratorDescription);
			}
		}
		return "Increases your parameters";
	}

	private string GetNameOrDescriptionOfPremium(bool isName, int count)
	{
		if (isName)
		{
			switch (count)
			{
			case 0:
				return LanguageManager.Instance.GetTextValue("HARVESTER");
			case 1:
				return localizedWordsShop.KetTextValue(localizedWordsShop.tank);
			case 2:
				return localizedWordsShop.KetTextValue(localizedWordsShop.supergun);
			case 3:
				return localizedWordsShop.KetTextValue(localizedWordsShop.antigravs);
			case 4:
				return localizedWordsShop.KetTextValue(localizedWordsShop.megaturbo);
			case 5:
				return localizedWordsShop.KetTextValue(localizedWordsShop.godmode);
			}
		}
		else
		{
			switch (count)
			{
			case 0:
				return LanguageManager.Instance.GetTextValue("HARVESTER");
			case 1:
				return localizedWordsShop.KetTextValue(localizedWordsShop.tankDescription);
			case 2:
				return localizedWordsShop.KetTextValue(localizedWordsShop.supergunDescription);
			case 3:
				return localizedWordsShop.KetTextValue(localizedWordsShop.antigravsDescription);
			case 4:
				return localizedWordsShop.KetTextValue(localizedWordsShop.megaturboDescription);
			case 5:
				return localizedWordsShop.KetTextValue(localizedWordsShop.godmodeDescription);
			}
		}
		return "Premium item";
	}

	private string GetPriceForPremium(int cp)
	{
		return "$0.49";
	}

	public void SetEnergyLabel(int count)
	{
		for (int i = 0; i < FueLabel.Length; i++)
		{
			FueLabel[i].text = count.ToString();
		}
	}

	public void SetCanvasTransparent(bool enable)
	{
		ShopCanvasTransparent.SetActive(enable);
	}

	public void SetFuelInfinytyIcon(bool enable)
	{
		FuelInfinytyIcon.gameObject.SetActive(enable);
		FuelInfinytyIcon2.gameObject.SetActive(enable);
		for (int i = 0; i < FueLabel.Length; i++)
		{
			FueLabel[i].gameObject.SetActive(!enable);
		}
	}
}
