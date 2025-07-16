using System;
using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class CellContainer : MonoBehaviour
{
	public enum State
	{
		Closed,
		Available,
		Active
	}

	public RectTransform ActiveRotate;

	public GameObject PositionForCar;

	public MapFriendContainer FacebookFriendContainer;

	public NewControllerForButtonPlayOnMap ButtonPlay;

	public GameObject ArenaBriff;

	public int Pack = 1;

	public int Level = 1;

	public LevelGalleryCanvasView LGCV;

	public GameObject[] boxes = new GameObject[3];

	[Header(" Active object ")]
	public Animator Active;

	public Animator AnimatorForArena;

	public CounterController text;

	public Button button;

	public bool BossLevel;

	public bool SpecialLevel;

	public bool ArenaNew;

	public State state;

	public Action<CellContainer> OnCellClick = delegate
	{
	};

	private int _isLocked = Animator.StringToHash("isLocked");

	private int _isActive = Animator.StringToHash("isActive");

	private int J;

	public CounterController ARENA;

	private int temp;

	public Text Prices;

	public GameObject RubyText;

	//public GameObject FuelText;

	private int boxesCollecteds;

	public void states()
	{
		state = State.Available;
		if (base.gameObject.activeSelf && Active != null)
		{
			Active.SetBool(_isLocked, value: false);
		}
		if (base.gameObject.activeSelf && Active != null)
		{
			Active.SetBool(_isActive, value: false);
		}
	}

	private void Update()
	{
		Debug.Log(GameEnergyLogic.instance.Energy);
		if (J < 5)
		{
			J++;
			return;
		}
		if (ArenaNew && state == State.Closed)
		{
			state = State.Available;
		}
		if (boxes.Length > 0)
		{
			for (int i = 0; i < boxesCollecteds; i++)
			{
				if (boxes.Length > 0)
				{
					boxes[i].SetActive(value: true);
				}
			}
		}
		if (state == State.Active)
		{
			if (base.gameObject.activeSelf && Active != null)
			{
				Active.SetBool(_isLocked, value: false);
			}
			if (base.gameObject.activeSelf && Active != null)
			{
				Active.SetBool(_isActive, value: true);
			}
			if (Progress.shop.activeCar > 4 && Progress.shop.activeCar < 0)
			{
				Progress.shop.activeCar = 0;
			}
			if (Progress.shop.activeCar < 0)
			{
				Progress.shop.activeCar = 0;
			}
		}
		J = 0;
	}

	private IEnumerator ForChek()
	{
		yield return 0;
		while (state == State.Closed)
		{
			yield return 0;
		}
		OnButtonClick();
	}
    private void Start()
    {
      
		GameEnergyLogic.instance.Energy = 999999;
    }
    private void OnEnable()
	{
		if (Progress.levels.active_level_last_openned == 1 && Progress.levels.active_pack_last_openned == 1 && Pack == 1 && Level == 1 && !ArenaNew && !SpecialLevel && !BossLevel)
		{
			StartCoroutine(ForChek());
		}
		temp = 0;
		for (int i = 1; i <= 3; i++)
		{
			for (int j = 1; j <= 12; j++)
			{
				temp += Progress.levels.Pack(i).Level(j).oldticket;
			}
		}
		if (!ArenaNew)
		{
			return;
		}
		button.gameObject.SetActive(value: true);
		state = State.Available;
		if (Pack == 1)
		{
			ARENA.count = DifficultyConfig.instance.BudgesARENA1.ToString();
		}
		if (Pack == 2)
		{
			ARENA.count = DifficultyConfig.instance.BudgesARENA2.ToString();
		}
		if (Pack == 3)
		{
			ARENA.count = DifficultyConfig.instance.BudgesARENA3.ToString();
		}
		if (Pack == 1 && temp >= DifficultyConfig.instance.BudgesARENA1)
		{
			if (base.gameObject.activeSelf)
			{
				AnimatorForArena.SetBool(_isLocked, value: false);
			}
			if (base.gameObject.activeSelf)
			{
				AnimatorForArena.SetBool(_isActive, value: true);
			}
		}
		if (Pack == 2 && temp >= DifficultyConfig.instance.BudgesARENA2)
		{
			if (base.gameObject.activeSelf)
			{
				AnimatorForArena.SetBool(_isLocked, value: false);
			}
			if (base.gameObject.activeSelf)
			{
				AnimatorForArena.SetBool(_isActive, value: true);
			}
		}
		if (Pack == 3 && temp >= DifficultyConfig.instance.BudgesARENA3)
		{
			if (base.gameObject.activeSelf)
			{
				AnimatorForArena.SetBool(_isLocked, value: false);
			}
			if (base.gameObject.activeSelf)
			{
				AnimatorForArena.SetBool(_isActive, value: true);
			}
		}
	}

	private void Awake()
	{
		button.onClick.AddListener(OnButtonClick);
		if (text != null)
		{
			if (!BossLevel)
			{
				text.count = Utilities.LevelNumberGlobal(Level, Pack).ToString();
			}
			else if (Pack == 1)
			{
				text.count = DifficultyConfig.instance.BudgesBoss1.ToString();
			}
			else if (Pack == 2)
			{
				text.count = DifficultyConfig.instance.BudgesBoss2.ToString();
			}
			else if (Pack == 3)
			{
				text.count = DifficultyConfig.instance.BudgesBoss3.ToString();
			}
		}
	}

	public void OnButtonClickGlog()
	{
		OnButtonClick();
	}

	private void OnButtonClick()
	{
		Progress.shop.endlessLevel = SpecialLevel;
		Progress.shop.bossLevel = BossLevel;
		Progress.shop.ArenaNew = ArenaNew;
		if (Progress.levels.InUndeground)
		{
			Progress.shop.TestFor9 = true;
		}
		else
		{
			Progress.shop.TestFor9 = false;
		}
		Progress.shop.Undeground2 = false;
		if (!ArenaNew)
		{
			Progress.shop.ArenaBrifOpen = false;
			if (state == State.Closed)
			{
				Audio.PlayAsync("gui_denied");
				Gogogogo();
			}
			else
			{
				if (Progress.levels.InUndeground && Pack == 2)
				{
					Progress.shop.Undeground2 = true;
				}
				LGCV.ForPlaybtnAnim.SetBool("isOn", value: true);
				if (!ArenaNew)
				{
					ButtonPlay.Act = Gogogogo;
				}
				ActiveRotate.anchoredPosition = base.gameObject.GetComponent<RectTransform>().anchoredPosition;
				if (state == State.Active)
				{
					ButtonPlay.anim.Play("btn_play_anim");
				}
				if (!ArenaNew)
				{
					RubyText.SetActive(value: false);
					//FuelText.SetActive(value: true);
					Prices.text = "-5";
				}
				Audio.PlayAsync("gui_button_02_sn");
				if (!SpecialLevel && !BossLevel)
				{
					Progress.levels.active_boss_pack_last_openned = -1;
					Progress.levels.active_boss_pack_last_openned_undeground = -1;
					if (Progress.levels.InUndeground)
					{
						Progress.levels.active_level_last_openned_under = Level;
						Progress.levels.active_pack_last_openned_under = Pack;
					}
					else
					{
						Progress.levels.active_level_last_openned = Level;
						Progress.levels.active_pack_last_openned = Pack;
					}
				}
				if (BossLevel)
				{
					if (Progress.levels.InUndeground)
					{
						Progress.levels.active_level_last_openned_under = -1;
						Progress.levels.active_pack_last_openned_under = -1;
						Progress.levels.active_boss_pack_last_openned_undeground = Pack;
					}
					else
					{
						Progress.levels.active_level_last_openned = -1;
						Progress.levels.active_pack_last_openned = -1;
						Progress.levels.active_boss_pack_last_openned = Pack;
					}
				}
				int oldticket = Progress.levels.Pack(Pack).Level(Level).oldticket;
				int num = 0;
				for (int i = 1; i <= 3; i++)
				{
					for (int j = 1; j <= 12; j++)
					{
						num += Progress.levels.Pack(i).Level(j).ticket;
					}
				}
				foreach (CellContainer item in LGCV.LGCL)
				{
					if (item.BossLevel)
					{
						if (item.name.Contains("itm_bossLevel_1"))
						{
							if (num >= DifficultyConfig.instance.BudgesBoss1)
							{
								item.SetState(State.Available, Progress.levels.Pack(item.Pack).Level(item.Level).oldticket);
							}
							else
							{
								item.SetState(State.Closed);
							}
						}
						if (item.name.Contains("itm_bossLevel_2"))
						{
							if (num >= DifficultyConfig.instance.BudgesBoss2)
							{
								item.SetState(State.Available, Progress.levels.Pack(item.Pack).Level(item.Level).oldticket);
							}
							else
							{
								item.SetState(State.Closed);
							}
						}
						if (item.name.Contains("itm_bossLevel_3"))
						{
							if (num >= DifficultyConfig.instance.BudgesBoss3)
							{
								item.SetState(State.Available, Progress.levels.Pack(item.Pack).Level(item.Level).oldticket);
							}
							else
							{
								item.SetState(State.Closed);
							}
						}
					}
					else if (!Progress.levels.Pack(item.Pack).Level(item.Level).isOpen)
					{
						item.SetState(State.Closed);
					}
					else
					{
						item.SetState(State.Available, Progress.levels.Pack(item.Pack).Level(item.Level).oldticket);
					}
				}
				SetState(State.Active, oldticket);
			}
		}
		else
		{
			Progress.levels.active_boss_pack_last_openned = -1;
			Progress.levels.active_boss_pack_last_openned_undeground = -1;
			ActiveRotate.anchoredPosition = base.gameObject.GetComponent<RectTransform>().anchoredPosition;
			if (Progress.levels.InUndeground)
			{
				Progress.levels.active_level_last_openned_under = Level;
				Progress.levels.active_pack_last_openned_under = Pack;
			}
			else
			{
				Progress.levels.active_level_last_openned = Level;
				Progress.levels.active_pack_last_openned = Pack;
			}
			if (!AnimatorForArena.GetBool(_isLocked))
			{
				foreach (CellContainer item2 in LGCV.LGCL)
				{
					if (item2.BossLevel)
					{
						if (item2.name.Contains("itm_bossLevel_1"))
						{
							if (temp >= DifficultyConfig.instance.BudgesBoss1)
							{
								item2.SetState(State.Available, Progress.levels.Pack(item2.Pack).Level(item2.Level).oldticket);
							}
							else
							{
								item2.SetState(State.Closed);
							}
						}
						if (item2.name.Contains("itm_bossLevel_2"))
						{
							if (temp >= DifficultyConfig.instance.BudgesBoss2)
							{
								item2.SetState(State.Available, Progress.levels.Pack(item2.Pack).Level(item2.Level).oldticket);
							}
							else
							{
								item2.SetState(State.Closed);
							}
						}
						if (item2.name.Contains("itm_bossLevel_3"))
						{
							if (temp >= DifficultyConfig.instance.BudgesBoss3)
							{
								item2.SetState(State.Available, Progress.levels.Pack(item2.Pack).Level(item2.Level).oldticket);
							}
							else
							{
								item2.SetState(State.Closed);
							}
						}
					}
					else if (!Progress.levels.Pack(item2.Pack).Level(item2.Level).isOpen)
					{
						item2.SetState(State.Closed);
					}
					else
					{
						item2.SetState(State.Available, Progress.levels.Pack(item2.Pack).Level(item2.Level).oldticket);
					}
				}
				int oldticket2 = Progress.levels.Pack(Pack).Level(Level).oldticket;
				SetState(State.Active, oldticket2);
			}
		}
		if (ArenaNew)
		{
			Progress.shop.ArenaBrifOpen = true;
			if (Progress.levels.active_pack_last_openned == 1 && !Progress.shop.Key1)
			{
				ArenaBriff.SetActive(value: true);
			}
			else if (Progress.levels.active_pack_last_openned == 2 && !Progress.shop.Key2)
			{
				ArenaBriff.SetActive(value: true);
			}
			else if (Progress.levels.active_pack_last_openned == 3 && !Progress.shop.Key3)
			{
				ArenaBriff.SetActive(value: true);
			}
			else if (Progress.levels.active_pack_last_openned == 1)
			{
				RubyText.SetActive(value: true);
				//FuelText.SetActive(value: false);
				Prices.text = DifficultyConfig.instance.RubinivForStartARENA1.ToString();
			}
			else if (Progress.levels.active_pack_last_openned == 2)
			{
				RubyText.SetActive(value: true);
				//FuelText.SetActive(value: false);
				Prices.text = DifficultyConfig.instance.RubinivForStartARENA2.ToString();
			}
			else if (Progress.levels.active_pack_last_openned == 3)
			{
				RubyText.SetActive(value: true);
				//FuelText.SetActive(value: false);
				Prices.text = DifficultyConfig.instance.RubinivForStartARENA3.ToString();
			}
			if (Pack == 1 && temp >= DifficultyConfig.instance.BudgesARENA1)
			{
				ButtonPlay.ActTemp = ButtonPlay.Act;
				ButtonPlay.Act = Gogogogo;
			}
			if (Pack == 2 && temp >= DifficultyConfig.instance.BudgesARENA2)
			{
				ButtonPlay.ActTemp = ButtonPlay.Act;
				ButtonPlay.Act = Gogogogo;
			}
			if (Pack == 3 && temp >= DifficultyConfig.instance.BudgesARENA3)
			{
				ButtonPlay.ActTemp = ButtonPlay.Act;
				ButtonPlay.Act = Gogogogo;
			}
		}
	}

	public void Gogogogo()
	{
		if (Progress.shop.EsterLevelPlay)
		{
			Progress.shop.endlessLevel = false;
			Progress.shop.ArenaNew = false;
			Progress.shop.bossLevel = false;
		}
		else
		{
			Progress.shop.endlessLevel = SpecialLevel;
			Progress.shop.bossLevel = BossLevel;
			Progress.shop.ArenaNew = ArenaNew;
			if (SpecialLevel && GameEnergyLogic.isEnoughForRace)
			{
				Progress.shop.SpecialMissionsOpenTime = DateTime.MinValue;
				Progress.shop.ActivCellNum = -1;
			}
		}
		OnCellClick(this);
	}

	public void SetState(State state, int boxesCollected = 0, bool isActive = false)
	{
		if (Active != null)
		{
			Active.enabled = true;
		}
		this.state = state;
		if (boxesCollected > 3)
		{
			boxesCollected = 3;
		}
		boxesCollecteds = boxesCollected;
		if (boxes != null && boxes.Length > 0)
		{
			for (int i = 0; i < boxes.Length; i++)
			{
				if (boxes[i] != null)
				{
					boxes[i].SetActive(value: false);
				}
			}
		}
		switch (state)
		{
		case State.Closed:
			if (base.gameObject.activeSelf && Active != null)
			{
				Active.SetBool(_isLocked, value: true);
			}
			if (base.gameObject.activeSelf && Active != null)
			{
				Active.SetBool(_isActive, value: false);
			}
			break;
		case State.Available:
			if (base.gameObject.activeSelf && Active != null)
			{
				Active.SetBool(_isLocked, value: false);
			}
			if (base.gameObject.activeSelf && Active != null)
			{
				Active.SetBool(_isActive, value: false);
			}
			for (int l = 0; l < boxesCollected; l++)
			{
				if (boxes.Length > 0)
				{
					boxes[l].SetActive(value: true);
				}
			}
			break;
		case State.Active:
			if (base.gameObject.activeSelf && Active != null)
			{
				Active.SetBool(_isLocked, value: false);
			}
			if (base.gameObject.activeSelf && Active != null)
			{
				Active.SetBool(_isActive, value: true);
			}
			if (Progress.shop.activeCar < 0 || Progress.shop.activeCar > LGCV.CarsForLevel.Count - 1)
			{
				Progress.shop.activeCar = 0;
			}
			if (LGCV.CarsForLevel.Count >= Progress.shop.activeCar)
			{
				LGCV.CarsForLevel[Progress.shop.activeCar].SetActive(value: true);
				LGCV.CarsForLevel[Progress.shop.activeCar].transform.SetParent(PositionForCar.transform.parent);
				LGCV.CarsForLevel[Progress.shop.activeCar].transform.localPosition = Vector3.zero;
				LGCV.CarsForLevel[Progress.shop.activeCar].transform.localScale = new Vector3(0.65f, 0.65f, 0.65f);
			}
			for (int j = 0; j < boxesCollected; j++)
			{
				if (boxes.Length > 0)
				{
					boxes[j].SetActive(value: true);
				}
			}
			if (boxesCollected == 0)
			{
				GameObject[] array = boxes;
				foreach (GameObject gameObject in array)
				{
					gameObject.SetActive(value: false);
				}
			}
			break;
		}
		if (isActive)
		{
			this.state = State.Active;
		}
		for (int m = 0; m < boxesCollected; m++)
		{
			if (boxes.Length > 0)
			{
				boxes[m].SetActive(value: true);
			}
		}
	}
}
