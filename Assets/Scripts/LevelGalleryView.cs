using AnimationOrTween;
//using CompleteProject;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelGalleryView : MonoBehaviour
{
	public UIRoot root;

	public Transform scrollPanel;

	private LevelGalleryModel _model;

	public GameObject[] OriginalPack;

	public UISprite OriginalPackIndicator;

	public Animation moveBtnLeftAnim;

	public Animation moveBtnRightAnim;

	public GameObject shopButton;

	private const int distanceBetweenPacks = 1280;

	private const int distanceBetweenPacksIndicators = 50;

	private List<GameObject> PackList = new List<GameObject>();

	[SerializeField]
	public List<LevelPackCheck> PackCheckList = new List<LevelPackCheck>();

	public GameObject BuyAllLevelsPanel;

	public UILabel buyAllLevelsLabel;

	public GameObject TransperentLoader;

	public GameObject carIcon;

	public AnimatedAlpha carAlfa;

	public AnimatedAlpha tankAlfa;

	public List<GameObject> tickets;

	public UILabel labelBuyNext;

	public UILabel labelBuyPack;

	public UILabel labelCoins;

	public UILabel labelEnegry;

	public UISprite infiniteSprite;

	public UILabel FuelLabelEachRace;

	public Animation FuelLabelAnim;

	private bool isInfEnergy;

	private LevelGalleryModel Model
	{
		get
		{
			if (_model == null)
			{
				_model = (LevelGalleryModel)UnityEngine.Object.FindObjectOfType(typeof(LevelGalleryModel));
			}
			return _model;
		}
	}

	private void Start()
	{
		FuelLabelEachRace.text = $"-{PriceConfig.instance.energy.eachStart}";
		carAlfa.gameObject.SetActive(Progress.shop.activeCar != 1);
		tankAlfa.gameObject.SetActive(Progress.shop.activeCar == 1);
		SetPricesText();
		labelCoins.text = Progress.shop.currency.ToString();
		isInfEnergy = Progress.gameEnergy.isInfinite;
		if (!Progress.gameEnergy.isInfinite)
		{
			labelEnegry.text = Progress.gameEnergy.energy.ToString();
			return;
		}
		labelEnegry.gameObject.SetActive(value: false);
		infiniteSprite.gameObject.SetActive(value: true);
	}

	private void Update()
	{
		labelCoins.text = Progress.shop.currency.ToString();
		if (!Progress.gameEnergy.isInfinite)
		{
			labelEnegry.text = Progress.gameEnergy.energy.ToString();
		}
		else if (Progress.gameEnergy.isInfinite != isInfEnergy)
		{
			isInfEnergy = Progress.gameEnergy.isInfinite;
			labelEnegry.gameObject.SetActive(value: false);
			infiniteSprite.gameObject.SetActive(value: true);
		}
	}

	public void PlayFuelLabelAnim(EventDelegate.Callback callBack)
	{
		Audio.Play("fuel-1");
		Utilities.RunActor(FuelLabelAnim, isForward: true, callBack);
	}

	public void FocusedOnPack(int _pack, float _strength = 0f)
	{
		if (_strength != 0f)
		{
			StartCoroutine(focusedOnCoroutine(_pack, _strength));
		}
		else
		{
			focusedOn(_pack, 5000f);
		}
		CheckBthMove(_pack);
	}

	private IEnumerator focusedOnCoroutine(int needPack, float strength)
	{
		focusedOn(needPack, strength);
		yield break;
	}

	public void focusedOn(int needPack, float str)
	{
		if (needPack > 0 && needPack < PackList.Count)
		{
			scrollPanel.transform.localPosition = Vector3.left * (needPack - 1) * root.manualWidth;
		}
	}

	public void InitPacks(int count)
	{
		PackList.Add(null);
		PackCheckList.Add(null);
		for (int i = 1; i <= count; i++)
		{
			PackList.Add(OriginalPack[i]);
			PackCheckList.Add(OriginalPack[i].GetComponent<LevelPackCheck>());
		}
	}

	public void HideShopButton()
	{
		shopButton.SetActive(value: false);
	}

	public void SetLevel(int pack, int level, bool isOpened, bool animate, int ticketsFound)
	{
		PackCheckList[pack].IconCheck(level).SetState(isOpened, animate, ticketsFound);
	}

	public void CheckBthMove(int pack)
	{
		if (pack == 1)
		{
			ActiveAnimation.Play(moveBtnLeftAnim, Direction.Forward);
		}
		else
		{
			Vector3 localPosition = moveBtnLeftAnim.gameObject.transform.localPosition;
			if (localPosition.x < 0f)
			{
				ActiveAnimation.Play(moveBtnLeftAnim, Direction.Reverse);
			}
		}
		if (pack == 4)
		{
			ActiveAnimation.Play(moveBtnRightAnim, Direction.Reverse);
			return;
		}
		Vector3 localPosition2 = moveBtnRightAnim.gameObject.transform.localPosition;
		if (localPosition2.x > 0f)
		{
			ActiveAnimation.Play(moveBtnRightAnim, Direction.Forward);
		}
	}

	public void SetBuyLevelsPanel(string str)
	{
		SetPricesText();
		BuyAllLevelsPanel.SetActive(value: true);
		buyAllLevelsLabel.text = str;
	}

	private void SetPricesText()
	{
		//	string text = Purchaser.m_StoreController.products.WithID(Purchaser.UnlockNext).metadata.localizedPrice.ToString();
		//	string text2 = Purchaser.m_StoreController.products.WithID(Purchaser.UnlockWorld1).metadata.localizedPrice.ToString();
		//	string text3 = Purchaser.m_StoreController.products.WithID(Purchaser.UnlockWorld2).metadata.localizedPrice.ToString();
		//	string text4 = Purchaser.m_StoreController.products.WithID(Purchaser.UnlockWorld3).metadata.localizedPrice.ToString();
		//text = ((!string.IsNullOrEmpty(text)) ? text : PriceConfig.instance.levelsGallery.unlockNextLevelDefaultPrice);
		//text2 = ((!string.IsNullOrEmpty(text2)) ? text2 : PriceConfig.instance.levelsGallery.unlockWorld1DefaultPrice);
		//text3 = ((!string.IsNullOrEmpty(text3)) ? text3 : PriceConfig.instance.levelsGallery.unlockWorld2DefaultPrice);
		//text4 = ((!string.IsNullOrEmpty(text4)) ? text4 : PriceConfig.instance.levelsGallery.unlockWorld3DefaultPrice);
		//labelBuyNext.text = text;
		//switch (Model.activePack)
		//{
		//case 1:
		//	labelBuyPack.text = text2;
		//	break;
		//case 2:
		//	labelBuyPack.text = text3;
		//	break;
		//case 3:
		//	labelBuyPack.text = text4;
		//	break;
		//}
	}

	public void HideBuyAllLevelsPanel()
	{
		BuyAllLevelsPanel.SetActive(value: false);
	}

	public void SetTransperent(bool isEnable)
	{
		TransperentLoader.SetActive(isEnable);
	}

	public void TeleportCarFromTo(int fromPack, int fromLevel, int toPack, int toLevel, params Action[] callbacks)
	{
		StartCoroutine(TeleportingCar(fromPack, fromLevel, toPack, toLevel, callbacks));
	}

	private IEnumerator TeleportingCar(int fromPack, int fromLevel, int toPack, int toLevel, params Action[] callbacks)
	{
		GameObject particle = UnityEngine.Object.Instantiate(Resources.Load("lgCarTeleport")) as GameObject;
		ParticleSystem[] componentsInChildren = particle.GetComponentsInChildren<ParticleSystem>();
		foreach (ParticleSystem particleSystem in componentsInChildren)
		{
			particleSystem.GetComponent<Renderer>().sortingLayerName = "Particles";
			particleSystem.GetComponent<Renderer>().sortingOrder = 100;
		}
		particle.transform.parent = carIcon.transform.parent;
		particle.transform.localScale = Vector3.one;
		particle.transform.localPosition = PackCheckList[toPack].transform.localPosition + PackCheckList[toPack].IconCheck(toLevel).transform.localPosition + Vector3.up * 23f;
		yield return StartCoroutine(animateHideCar(fromPack, fromLevel));
		yield return StartCoroutine(animateShowCar(toPack, toLevel));
		yield return new WaitForEndOfFrame();
		foreach (Action action in callbacks)
		{
			action();
		}
	}

	private IEnumerator animateHideCar(int pack, int level)
	{
		if (pack >= 1 && level >= 1)
		{
			AnimatedAlpha carSprite = carIcon.GetComponentInChildren<AnimatedAlpha>();
			for (float f = 1f; f >= 0f; f -= 0.08f)
			{
				carSprite.alpha = f;
				PackCheckList[pack].IconCheck(level).spriteLight.color = new Color(1f, 1f, 1f, f);
				yield return new WaitForEndOfFrame();
			}
			PackCheckList[pack].IconCheck(level).Tickets(hide: false);
		}
	}

	private IEnumerator animateShowCar(int pack, int level)
	{
		AnimatedAlpha carSprite = carIcon.GetComponentInChildren<AnimatedAlpha>();
		carSprite.alpha = 0.01f;
		carIcon.transform.localPosition = PackCheckList[pack].transform.localPosition + PackCheckList[pack].IconCheck(level).transform.localPosition;
		yield return new WaitForSeconds(0.5f);
		PackCheckList[pack].IconCheck(level).Tickets(hide: true);
		PackCheckList[pack].IconCheck(level).spriteLight.color = new Color(1f, 1f, 1f, 1f);
		yield return new WaitForSeconds(0.1f);
		PackCheckList[pack].IconCheck(level).spriteLight.color = new Color(1f, 1f, 1f, 0.01f);
		yield return new WaitForSeconds(0.1f);
		PackCheckList[pack].IconCheck(level).spriteLight.color = new Color(1f, 1f, 1f, 1f);
		for (float f2 = 0.02f; f2 <= 0.5f; f2 += 0.04f)
		{
			carSprite.alpha = f2;
			yield return new WaitForEndOfFrame();
		}
		PackCheckList[pack].IconCheck(level).spriteLight.color = new Color(1f, 1f, 1f, 0.01f);
		for (float f = carSprite.alpha; f <= 1f; f += 0.04f)
		{
			carSprite.alpha = f;
			yield return new WaitForEndOfFrame();
		}
		PackCheckList[pack].IconCheck(level).spriteLight.color = new Color(1f, 1f, 1f, 1f);
		carIcon.SetActive(value: true);
	}
}
