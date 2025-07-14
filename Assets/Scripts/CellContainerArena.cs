using SmartLocalization;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CellContainerArena : MonoBehaviour
{
	public enum State
	{
		Closed,
		Available,
		Active
	}

	public List<Animation> animHideShowParticks = new List<Animation>();

	public GameObject brifing;

	public Button btnCloseBrifing;

	public GameObject fakeButton;

	public List<Animation> fills = new List<Animation>();

	public List<CellContainerArena> CCA = new List<CellContainerArena>();

	public List<CellContainer> CC = new List<CellContainer>();

	public int Pack = 1;

	public int Level = 1;

	public int PackS = 1;

	public int LevelS = 1;

	public GameObject Lock;

	public GameObject buyLevelsWindow;

	[Header(" Active object ")]
	public GameObject Active;

	public List<GameObject> cars = new List<GameObject>();

	public Button button;

	public bool Arena = true;

	public State state;

	public Text arenanamer1;

	public Action<CellContainer> OnCellClick = delegate
	{
	};

	public Text txtEnergy;

	private bool IsEnoughFuel => GameEnergyLogic.isEnoughForRace;

	private void Update()
	{
		if (UnityEngine.Input.GetKeyDown(KeyCode.Escape))
		{
			BrifingClickClose();
		}
		arenanamer1.text = LanguageManager.Instance.GetTextValue("ARENA *").Replace("*", Pack.ToString());
		if (state == State.Active)
		{
			Active.SetActive(value: true);
			cars[Progress.shop.activeCar].SetActive(value: true);
			for (int i = 0; i < cars.Count; i++)
			{
				if (i != Progress.shop.activeCar)
				{
					cars[i].SetActive(value: false);
				}
			}
		}
		else
		{
			Active.SetActive(value: false);
			foreach (GameObject car in cars)
			{
				car.SetActive(value: false);
			}
		}
	}

	private void OnEnable()
	{
		if (Progress.levels.Pack(Pack).Level(Level).isOpen)
		{
			state = State.Available;
			Lock.SetActive(value: false);
		}
		else
		{
			state = State.Closed;
			Lock.SetActive(value: true);
		}
		StartCoroutine(qwer());
	}

	private IEnumerator qwer()
	{
		for (int i = 0; i < 4; i++)
		{
			yield return 0;
		}
		if (Progress.levels.Pack(Pack).Level(Level).isOpen)
		{
			state = State.Available;
			Lock.SetActive(value: false);
		}
		else
		{
			state = State.Closed;
			Lock.SetActive(value: true);
		}
		arenanamer1.text = LanguageManager.Instance.GetTextValue("ARENA *").Replace("*", Pack.ToString());
		if (Progress.levels.active_level == 10)
		{
			if (Progress.levels.active_pack == 1)
			{
				CCA[0].state = State.Active;
			}
			if (Progress.levels.active_pack == 2)
			{
				CCA[1].state = State.Active;
			}
			if (Progress.levels.active_pack == 3)
			{
				CCA[2].state = State.Active;
			}
			if (Progress.levels.active_pack == 4)
			{
				CCA[3].state = State.Active;
			}
		}
	}

	private void Awake()
	{
		btnCloseBrifing.onClick.AddListener(BrifingClickClose);
		button.onClick.AddListener(BrifingClick);
	}

	private void SetInAppPrices()
	{
	}

	private void BrifingClick()
	{
		StartCoroutine(clic());
	}

	private IEnumerator clic()
	{
		if (state != 0)
		{
			foreach (CellContainerArena item in CCA)
			{
				item.Active.SetActive(value: false);
			}
			Progress.shop.endlessLevel = Arena;
			SetState(State.Active, 0, isActive: true);
			Progress.levels.arena_pack = (byte)PackS;
			Progress.levels.arena_level = (byte)LevelS;
			Progress.levels.active_pack = (byte)PackS;
			Progress.levels.active_level = (byte)LevelS;
			foreach (CellContainer item2 in CC)
			{
				item2.states();
			}
		}
		foreach (Animation animHideShowPartick in animHideShowParticks)
		{
			animHideShowPartick["gateMiricle_hide"].speed = 1f;
			animHideShowPartick.Play("gateMiricle_hide");
		}
		if (Pack == 1)
		{
			if (Progress.levels.winArena1)
			{
				Progress.levels.active_pack = (byte)PackS;
				Progress.levels.active_level = (byte)LevelS;
				OnButtonClick();
			}
			else
			{
				brifing.SetActive(value: true);
				for (int i = 0; i < fills.Count; i++)
				{
					if (i != Pack - 1)
					{
						fills[i].Play("detaleBlink");
					}
					else
					{
						fills[i].Play("detaleHighlight");
					}
				}
				if (state == State.Active)
				{
					fakeButton.SetActive(value: true);
					Progress.levels.active_pack = (byte)PackS;
					Progress.levels.active_level = (byte)LevelS;
				}
				else
				{
					fakeButton.SetActive(value: false);
				}
			}
		}
		if (Pack == 2)
		{
			if (Progress.levels.winArena2)
			{
				Progress.levels.active_pack = (byte)PackS;
				Progress.levels.active_level = (byte)LevelS;
				OnButtonClick();
			}
			else
			{
				brifing.SetActive(value: true);
				if (Progress.levels.winArena1)
				{
					fills[0].Play("detaleNorm");
				}
				else
				{
					fills[0].Play("detaleBlink");
				}
				for (int j = 1; j < fills.Count; j++)
				{
					if (j != Pack - 1)
					{
						fills[j].Play("detaleBlink");
					}
					else
					{
						fills[j].Play("detaleHighlight");
					}
				}
				if (state == State.Active)
				{
					fakeButton.SetActive(value: true);
					Progress.levels.active_pack = (byte)PackS;
					Progress.levels.active_level = (byte)LevelS;
				}
				else
				{
					fakeButton.SetActive(value: false);
				}
			}
		}
		if (Pack == 3)
		{
			if (Progress.levels.winArena3)
			{
				Progress.levels.active_pack = (byte)PackS;
				Progress.levels.active_level = (byte)LevelS;
				OnButtonClick();
			}
			else
			{
				brifing.SetActive(value: true);
				if (Progress.levels.winArena1)
				{
					fills[0].Play("detaleNorm");
				}
				else
				{
					fills[0].Play("detaleBlink");
				}
				if (Progress.levels.winArena2)
				{
					fills[1].Play("detaleNorm");
				}
				else
				{
					fills[1].Play("detaleBlink");
				}
				for (int k = 1; k < fills.Count; k++)
				{
					if (k != Pack - 1)
					{
						fills[k].Play("detaleBlink");
					}
					else
					{
						fills[k].Play("detaleHighlight");
					}
				}
				if (state == State.Active)
				{
					fakeButton.SetActive(value: true);
					Progress.levels.active_pack = (byte)PackS;
					Progress.levels.active_level = (byte)LevelS;
				}
				else
				{
					fakeButton.SetActive(value: false);
				}
			}
		}
		if (Pack == 4)
		{
			if (Progress.levels.winArena4)
			{
				Progress.levels.active_pack = (byte)PackS;
				Progress.levels.active_level = (byte)LevelS;
				OnButtonClick();
			}
			else
			{
				brifing.SetActive(value: true);
				if (Progress.levels.winArena1)
				{
					fills[0].Play("detaleNorm");
				}
				else
				{
					fills[0].Play("detaleBlink");
				}
				if (Progress.levels.winArena2)
				{
					fills[1].Play("detaleNorm");
				}
				else
				{
					fills[1].Play("detaleBlink");
				}
				if (Progress.levels.winArena2)
				{
					fills[2].Play("detaleNorm");
				}
				else
				{
					fills[2].Play("detaleBlink");
				}
				fills[3].Play("detaleHighlight");
				if (state == State.Active)
				{
					fakeButton.SetActive(value: true);
					Progress.levels.active_pack = (byte)PackS;
					Progress.levels.active_level = (byte)LevelS;
				}
				else
				{
					fakeButton.SetActive(value: false);
				}
			}
		}
		yield return 0;
	}

	private void BrifingClickClose()
	{
		StartCoroutine(closeClic());
	}

	private IEnumerator closeClic()
	{
		yield return 0;
		foreach (Animation animHideShowPartick in animHideShowParticks)
		{
			animHideShowPartick["gateMiricle_hide"].speed = -1f;
			animHideShowPartick.Play("gateMiricle_hide");
		}
		Game.OnStateChange(Game.gameState.Menu);
		brifing.SetActive(value: false);
	}

	private void OnClosedLevelClick()
	{
		Game.PushInnerState("levels_buy", ButtonBuyClose);
		SetInAppPrices();
		buyLevelsWindow.SetActive(value: true);
	}

	private void ButtonBuyClose()
	{
		Game.PopInnerState("levels_buy", executeClose: false);
	}

	public void SetState(State state, int boxesCollected = 0, bool isActive = false)
	{
		this.state = state;
		switch (state)
		{
		case State.Closed:
			Lock.SetActive(value: true);
			break;
		}
		if (isActive)
		{
			this.state = State.Active;
		}
	}

	public void OnButtonClick()
	{
		OnAvailableClick();
		Audio.PlayAsync("gui_button_02_sn");
	}

	private void OnAvailableClick()
	{
		TakeFuelAndGo();
	}

	private void TakeFuelAndGo()
	{
		int eachStart = GameEnergyLogic.instance.energyConfig.eachStart;
		if (!Progress.shop.endlessLevel)
		{
			PriceConfig.instance.energy.eachStart = 5;
			GameEnergyLogic.instance.energyConfig.eachStart = PriceConfig.instance.energy.eachStart;
		}
		else
		{
			PriceConfig.instance.energy.eachStart = 10;
			GameEnergyLogic.instance.energyConfig.eachStart = PriceConfig.instance.energy.eachStart;
		}
		if (IsEnoughFuel)
		{
			StartCoroutine(AnimateFuel());
		}
		else
		{
			NotEnoughFuel();
		}
		GameEnergyLogic.instance.energyConfig.eachStart = eachStart;
		PriceConfig.instance.energy.eachStart = eachStart;
	}

	private int GetFuelForRace()
	{
		GameEnergyLogic.GetFuelForRace();
		return PriceConfig.instance.energy.eachStart;
	}

	private IEnumerator AnimateFuel()
	{
		Audio.Play("fuel-1");
		GetFuelForRace();
		txtEnergy.text = GameEnergyLogic.GetEnergy.ToString();
		GameObject anim = UnityEngine.Object.Instantiate(txtEnergy.gameObject);
		Text text = anim.GetComponent<Text>();
		text.text = $"-{PriceConfig.instance.energy.eachStart}";
		text.rectTransform.SetParent(txtEnergy.rectTransform.parent);
		text.rectTransform.localScale = txtEnergy.rectTransform.localScale;
		text.transform.position = txtEnergy.transform.position;
		float dx = 0f;
		while (dx < 50f)
		{
			text.rectTransform.anchoredPosition = txtEnergy.rectTransform.anchoredPosition - Vector2.up * dx;
			dx += 0.5f;
			yield return null;
		}
		UnityEngine.SceneManagement.SceneManager.LoadScene("Race");
	}

	private void NotEnoughFuel()
	{
		GUI_shop.instance.ShowBuyCanvasWindow();
	}
}
