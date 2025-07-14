using SmartLocalization;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class FortuneModel : MonoBehaviour
{
	[Serializable]
	public class SimpleGear
	{
		public GameObject go;

		public float radius;

		public bool right;
	}

	public GameObject ObjCont;

	public GraphicRaycaster qwe;

	public Text ticketsSprite;

	public Text rubinsSprite;

	public Button FreeSpinClic;

	public GameObject tint;

	public Text InButton;

	public GameObject transperent;

	public GameObject transperentTopLevel;

	public UISprite upRoad;

	public UISprite downRoad;

	public List<GameObject> interfaceTop = new List<GameObject>();

	public List<GameObject> interfaceBottom = new List<GameObject>();

	public Fortune fortune;

	public GameObject blocker;

	public GameObject buyButton;

	public GameObject buyBlocker;

	public GameObject continueButton;

	public GameObject contimueBlocker;

	public GameObject pushButton;

	public GameObject tutor_hand;

	[Header("3 buttons")]
	public GameObject Start;

	public GameObject tintForStart;

	public GameObject Stop;

	public GameObject Exit;

	[Header(" ")]
	private int pTickets;

	private int pRubins;

	public int TicketPrice;

	public Transform rootTransform;

	public GameObject allWheel;

	public GameObject TopGear;

	public GameObject BotGear;

	public float gearRadius;

	public float step = 30f;

	public ParticleSystem particleWin;

	public List<SimpleGear> gears = new List<SimpleGear>();

	private bool isMoving;

	private bool animating;

	private Action callback;

	private IEnumerator animatingTicket;

	private Coroutine enumScaleBuyContButtons;

	public int ticketsCount
	{
		get
		{
			return pTickets;
		}
		set
		{
			ticketsSprite.text = value.ToString();
			pTickets = value;
			fortune.canSpin = (value > 0);
		}
	}

	public int rubinsCount
	{
		get
		{
			return pRubins;
		}
		set
		{
			rubinsSprite.text = value.ToString();
			pRubins = value;
		}
	}

	private void OnEnable()
	{
		TicketPrice = PriceConfig.instance.fortuneWheel.rubiesForOneAddictionalSpin;
		Fortune obj = fortune;
		obj.OnSpinEnded = (FortuneBase.SpinEnded)Delegate.Combine(obj.OnSpinEnded, new FortuneBase.SpinEnded(SpinEnd));
		Fortune obj2 = fortune;
		obj2.OnSpinStarted = (FortuneBase.SpinStarted)Delegate.Combine(obj2.OnSpinStarted, new FortuneBase.SpinStarted(SpinStart));
		particleWin.GetComponent<Renderer>().sortingLayerName = "UI";
		particleWin.GetComponent<Renderer>().sortingOrder = 45;
		blocker.SetActive(ticketsCount <= 0);
		buyBlocker.SetActive(Progress.shop.currency < TicketPrice);
		buyButton.GetComponent<GraphicRaycaster>().enabled = (Progress.shop.currency >= TicketPrice);
		pushButton.GetComponent<CircleCollider2D>().enabled = (Progress.levels.tickets > 0);
		FreeSpinClic.onClick.AddListener(OnClic);
	}

	private void OnDisable()
	{
		Fortune obj = fortune;
		obj.OnSpinEnded = (FortuneBase.SpinEnded)Delegate.Remove(obj.OnSpinEnded, new FortuneBase.SpinEnded(SpinEnd));
		Fortune obj2 = fortune;
		obj2.OnSpinStarted = (FortuneBase.SpinStarted)Delegate.Remove(obj2.OnSpinStarted, new FortuneBase.SpinStarted(SpinStart));
		FreeSpinClic.onClick.RemoveAllListeners();
	}

	private void OnClic()
	{
		AnalyticsManager.LogEvent(EventCategoty.video, "ads_watched", "fortune");
		showConfirmBuy();
		Progress.shop.timerForFortune = DateTime.Now.ToString();
	}

	private void SpinStart()
	{
		ticketsCount--;
		continueButton.GetComponent<GraphicRaycaster>().enabled = false;
		contimueBlocker.SetActive(value: true);
		buyButton.GetComponent<GraphicRaycaster>().enabled = false;
		buyBlocker.SetActive(value: true);
		pushButton.GetComponent<CircleCollider2D>().enabled = false;
	}

	public void BuyTicket()
	{
		if (animatingTicket == null && !animating && !fortune.isSpinning && Progress.shop.currency >= TicketPrice && enumScaleBuyContButtons != null)
		{
			StopCoroutine(enumScaleBuyContButtons);
			enumScaleBuyContButtons = null;
		}
	}

	private void showConfirmBuy()
	{
		transperentTopLevel.SetActive(value: true);
		fortune.canSpin = false;
		Confirm(yes: true);
	}

	private void Confirm(bool yes)
	{
		if (yes)
		{
			Progress.levels.tickets++;
			UnityEngine.Debug.Log("Minus currency !!!!!");
			Progress.Save(Progress.SaveType.Levels);
			Progress.Save(Progress.SaveType.Shop);
			Audio.PlayAsync("gui_shop_purchase_01_sn");
			ticketsCount++;
		}
		else
		{
			Audio.PlayAsync("gui_button_02_sn");
		}
		fortune.canSpin = (ticketsCount > 0);
		blocker.SetActive(ticketsCount <= 0);
		buyBlocker.SetActive(value: false);
		pushButton.GetComponent<CircleCollider2D>().enabled = (ticketsCount > 0);
		transperentTopLevel.SetActive(value: false);
	}

	private void SpinEnd(int sectorNum)
	{
		fortune.canSpin = false;
		animating = true;
		StartCoroutine(AnimateWinSprite(fortune.sectors[sectorNum]));
		switch (fortune.sectors[sectorNum].sectorType)
		{
		case FortuneSector.SectorType.rubins:
			Progress.shop.currency += fortune.sectors[sectorNum].amount;
			break;
		case FortuneSector.SectorType.health:
			Progress.shop.healthBost++;
			break;
		case FortuneSector.SectorType.turbo:
			Progress.shop.turboBoost++;
			break;
		}
		Audio.PlayAsync("fortuneWin");
		Progress.Save(Progress.SaveType.Shop);
	}

	public void InitWheel()
	{
	}

	public void ShowWheel(int tickets = 3, Action callback = null)
	{
		base.gameObject.SetActive(value: true);
		if (!isMoving)
		{
			ticketsCount = tickets;
			blocker.SetActive(ticketsCount <= 0);
			this.callback = callback;
			rubinsCount = Progress.shop.currency;
			StartCoroutine(Animation(on: true));
			Game.OnStateChange(Game.gameState.Fortune);
			Audio.Play("gui_screen_on");
		}
	}

	private IEnumerator ScaleTutorial(GameObject go)
	{
		float scale = 1f;
		Vector3 localScale = go.transform.localScale;
		float minScale = localScale.x;
		float maxScale = minScale + 0.1f;
		bool up = true;
		while (true)
		{
			if ((!(scale < maxScale) || !up) && (!(scale > minScale) || up))
			{
				up = !up;
				continue;
			}
			yield return null;
			scale += 0.01f * (float)(up ? 1 : (-1));
			go.transform.localScale = Vector3.one * scale;
		}
	}

	private void ResetScale(GameObject go)
	{
		go.transform.localScale = Vector3.one;
	}

	private void Update()
	{
		qwe.enabled = true;
		ticketsSprite.text = Progress.levels.tickets.ToString();
		spins();
		if (UnityEngine.Input.GetKeyDown(KeyCode.Escape) && !isMoving && !fortune.isSpinning && !animating && !fortune.isDraging && UnityEngine.Object.FindObjectOfType<Confirm_ticket_window>() == null)
		{
			HideWheel();
		}
		if (fortune.forME)
		{
			tintForStart.SetActive(value: true);
		}
		else
		{
			tintForStart.SetActive(value: false);
		}
		if (Progress.levels.ForTutorialFortune && Start.activeSelf)
		{
			StartCoroutine(ScaleTutorial(Start));
		}
		if (Progress.levels.ForTutorialFortune && Stop.activeSelf)
		{
			StartCoroutine(ScaleTutorial(Stop));
		}
		if (Progress.levels.tickets > 0)
		{
			if (fortune.spinning)
			{
				Start.SetActive(value: false);
				Exit.SetActive(value: false);
				Stop.SetActive(value: true);
			}
			else
			{
				Start.SetActive(value: true);
				Exit.SetActive(value: false);
				Stop.SetActive(value: false);
			}
		}
		else if (fortune.spinning)
		{
			Start.SetActive(value: false);
			Exit.SetActive(value: false);
			Stop.SetActive(value: true);
		}
		else
		{
			Start.SetActive(value: false);
			Exit.SetActive(value: true);
			Stop.SetActive(value: false);
		}
	}

	private void spins()
	{
		if (Progress.shop.timerForFortune != null && Progress.shop.timerForFortune != "0")
		{
			TimeSpan timeSpan = DateTime.Now - DateTime.Parse(Progress.shop.timerForFortune);
			double num = (double)PriceConfig.instance.currency.timeForFortune - timeSpan.TotalSeconds;
			int num2 = (int)(num % 3600.0) / 60;
			int num3 = (int)(num % 60.0);
			string text = string.Format("{0}:{1}", (num2 >= 10) ? num2.ToString() : ("0" + num2.ToString()), (num3 >= 10) ? num3.ToString() : ("0" + num3.ToString()));
			if (timeSpan.TotalSeconds > (double)PriceConfig.instance.currency.timeForFortune)
			{
				InButton.text = LanguageManager.Instance.GetTextValue("FREE SPIN");
				FreeSpinClic.interactable = true;
				tint.SetActive(value: false);
			}
			else
			{
				tint.SetActive(value: true);
				FreeSpinClic.interactable = false;
				InButton.text = text;
			}
		}
	}

	public void HideWheel()
	{
		if (!isMoving && !fortune.isSpinning && !animating && !fortune.forME)
		{
			if (callback != null)
			{
				callback();
			}
			if (enumScaleBuyContButtons != null)
			{
				StopCoroutine(enumScaleBuyContButtons);
				enumScaleBuyContButtons = null;
			}
			StartCoroutine(Animation(on: false));
			Game.OnStateChange(Game.previousState);
			Progress.levels.ForTutorialFortune = false;
			Audio.Play("gui_window_01_sn");
		}
	}

	public void DoSpin()
	{
		if (!animating && !isMoving && !fortune.isSpinning && ticketsCount >= 1)
		{
			fortune.DoSpin();
			UnityEngine.Debug.Log("!!!! LEX DoSpin!");
		}
	}

	public void qwrteqwr()
	{
		StartCoroutine(Ani(on: false));
	}

	public IEnumerator Ani(bool on)
	{
		yield return StartCoroutine(Animation(on: true));
		isMoving = true;
		if (on)
		{
			transperent.SetActive(value: true);
			yield return StartCoroutine(moveRoad(on));
			yield return StartCoroutine(moveWheel(on));
		}
		else
		{
			yield return StartCoroutine(moveWheel(on));
			yield return StartCoroutine(moveRoad(on));
			transperent.SetActive(value: false);
		}
		isMoving = false;
		if (!on)
		{
			base.gameObject.SetActive(value: false);
		}
		else if (PlayerPrefs.GetInt("tutorialFortuneHand", 0) == 0)
		{
			PlayerPrefs.SetInt("tutorialFortuneHand", 1);
			StartCoroutine(HandTutorial());
		}
	}

	private IEnumerator Animation(bool on)
	{
		isMoving = true;
		if (on)
		{
			transperent.SetActive(value: true);
			yield return StartCoroutine(moveRoad(on));
			yield return StartCoroutine(moveWheel(on));
		}
		else
		{
			yield return StartCoroutine(moveWheel(on));
			yield return StartCoroutine(moveRoad(on));
			transperent.SetActive(value: false);
		}
		isMoving = false;
		if (!on)
		{
			base.gameObject.SetActive(value: false);
		}
		else if (PlayerPrefs.GetInt("tutorialFortuneHand", 0) == 0)
		{
			PlayerPrefs.SetInt("tutorialFortuneHand", 1);
			StartCoroutine(HandTutorial());
		}
	}

	private IEnumerator HandTutorial()
	{
		tutor_hand.SetActive(value: true);
		while (!isMoving && !animating && !fortune.isSpinning && !fortune.isDraging)
		{
			yield return null;
		}
		tutor_hand.SetActive(value: false);
	}

	private IEnumerator tutorialSpinScale(GameObject go1, GameObject go2 = null)
	{
		while (isMoving)
		{
			yield return null;
		}
		if (animating || isMoving || fortune.isSpinning)
		{
			yield break;
		}
		float scale = 1f;
		float waitTime = 2f;
		bool up2 = true;
		GameObject go3 = go1;
		while (true)
		{
			for (int i = 0; i < 2; i++)
			{
				while (scale < 1.1f && up2)
				{
					if (animating || isMoving || fortune.isSpinning)
					{
						yield break;
					}
					yield return null;
					scale += 0.01f * (float)(up2 ? 1 : (-1));
					go3.transform.localScale = Vector3.one * scale;
				}
				up2 = !up2;
				while (scale > 1f && !up2)
				{
					if (animating || isMoving || fortune.isSpinning)
					{
						yield break;
					}
					yield return null;
					scale += 0.01f * (float)(up2 ? 1 : (-1));
					go3.transform.localScale = Vector3.one * scale;
				}
				up2 = !up2;
			}
			if (go2 != null)
			{
				go3 = ((!(go3 == go1)) ? go1 : go2);
				continue;
			}
			while (waitTime > 0f)
			{
				if (animating || isMoving || fortune.isSpinning)
				{
					yield break;
				}
				waitTime -= Time.unscaledDeltaTime;
				yield return null;
			}
			waitTime = 3f;
		}
	}

	private IEnumerator moveRoad(bool show)
	{
		float pstep = 30f;
		float dx = 300f;
		while (dx > 0f)
		{
			upRoad.transform.localPosition += ((!show) ? Vector3.up : Vector3.down) * pstep;
			downRoad.transform.localPosition += ((!show) ? Vector3.down : Vector3.up) * pstep;
			foreach (GameObject item in interfaceTop)
			{
				item.transform.localPosition += ((!show) ? Vector3.up : Vector3.down) * pstep;
			}
			foreach (GameObject item2 in interfaceBottom)
			{
				item2.transform.localPosition += ((!show) ? Vector3.down : Vector3.up) * pstep;
			}
			dx -= pstep;
			yield return Utilities.WaitForRealSeconds(0.01f);
		}
	}

	private IEnumerator moveWheel(bool right)
	{
		float L2 = (float)Math.PI * 4f * gearRadius;
		float i = (float)Math.PI * 2f * gearRadius;
		float pStep = Mathf.Abs(step) * (float)(right ? 1 : (-1));
		while (L2 > 0f)
		{
			Vector3 a = Vector3.right * i * pStep / 360f;
			Vector3 localScale = rootTransform.localScale;
			Vector3 dx = a * localScale.x;
			allWheel.transform.position += dx;
			TopGear.transform.eulerAngles += Vector3.forward * pStep;
			BotGear.transform.eulerAngles -= Vector3.forward * pStep;
			foreach (SimpleGear gear in gears)
			{
				gear.go.transform.eulerAngles += Vector3.forward * pStep * gearRadius / gear.radius * (gear.right ? 1 : (-1));
			}
			float num = L2;
			float x = dx.x;
			Vector3 localScale2 = rootTransform.localScale;
			L2 = num - Mathf.Abs(x / localScale2.x);
			yield return Utilities.WaitForRealSeconds(0.01f);
		}
	}

	public void STOPSPIN()
	{
		UnityEngine.Debug.Log("!!!! LEX STOPSPIN!");
		Audio.Stop("fortune");
		StartCoroutine(stops());
		Audio.Play("fortune");
	}

	private IEnumerator stops()
	{
		ResetScale(Start);
		ResetScale(Stop);
		fortune.spinning = false;
		yield return 0;
	}

	private IEnumerator AnimateWinSprite(FortuneSector winSector)
	{
		particleWin.gameObject.SetActive(value: true);
		UISprite sprite = UnityEngine.Object.Instantiate(winSector.prizeSprite, fortune.wheelTransform.position, Quaternion.identity);
		sprite.transform.parent = winSector.prizeSprite.transform.parent;
		sprite.depth = 50;
		sprite.transform.localScale = winSector.prizeSprite.transform.localScale;
		float dt4 = 0f;
		float time = 1.5f;
		while (dt4 < time)
		{
			sprite.transform.localScale += Vector3.one * 0.2f;
			dt4 += 0.05f + dt4 / 20f;
			yield return null;
		}
		dt4 = 0f;
		while (dt4 < time / 2f)
		{
			sprite.transform.localScale -= Vector3.one * 0.05f;
			dt4 += 0.05f + dt4 / 10f;
			yield return null;
		}
		dt4 = 0f;
		while (dt4 < time * 0.5f)
		{
			dt4 += 0.05f;
			yield return null;
		}
		if (winSector.sectorType == FortuneSector.SectorType.rubins)
		{
			yield return StartCoroutine(CountWinRubies(winSector, rubinsCount));
		}
		dt4 = 0f;
		while (dt4 < 1f)
		{
			sprite.transform.localScale -= Vector3.one * 0.15f;
			sprite.alpha = 1f - dt4;
			dt4 += 0.05f + dt4 / 20f;
			yield return null;
		}
		UnityEngine.Object.Destroy(sprite.gameObject);
		fortune.canSpin = (ticketsCount > 0);
		blocker.SetActive(ticketsCount <= 0);
		buyBlocker.SetActive(Progress.shop.currency < TicketPrice);
		buyButton.GetComponent<GraphicRaycaster>().enabled = (Progress.shop.currency >= TicketPrice);
		continueButton.GetComponent<GraphicRaycaster>().enabled = true;
		pushButton.GetComponent<CircleCollider2D>().enabled = (ticketsCount > 0);
		contimueBlocker.SetActive(value: false);
		animating = false;
	}

	private IEnumerator CountRubies(int startValue, int addValue)
	{
		int dt = 10;
		int countNow = startValue;
		int step = addValue / dt;
		for (int i = 0; i < dt; i++)
		{
			countNow += step;
			rubinsSprite.text = countNow.ToString();
			yield return null;
			yield return null;
			yield return null;
			yield return null;
		}
		rubinsCount = startValue + addValue;
		animatingTicket = null;
	}

	private IEnumerator CountWinRubies(FortuneSector winSector, int startVal)
	{
		UILabel label = UnityEngine.Object.Instantiate(winSector.prizeLabel, winSector.prizeLabel.transform.position, winSector.prizeLabel.transform.rotation);
		label.fontSize *= 4;
		label.depth = 50;
		float dx = 0f;
		while (dx < 1f)
		{
			label.transform.position = Vector3.Lerp(winSector.prizeLabel.transform.position, rubinsSprite.transform.position + Vector3.right * 0.2f, dx);
			label.transform.rotation = Quaternion.Lerp(winSector.prizeLabel.transform.rotation, Quaternion.identity, dx);
			dx += 0.05f + dx / 10f;
			label.alpha -= 0.02f;
			yield return null;
		}
		UnityEngine.Object.Destroy(label.gameObject);
		Audio.PlayAsync("gui_scoring");
		animatingTicket = CountRubies(startVal, winSector.amount);
		yield return StartCoroutine(animatingTicket);
	}
}
