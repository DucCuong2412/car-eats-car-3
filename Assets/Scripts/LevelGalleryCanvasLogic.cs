//using CompleteProject;
using Smokoko.DebugModule;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelGalleryCanvasLogic : MonoBehaviour
{
	[SerializeField]
	protected int activePack;

	[SerializeField]
	protected int activeLevel;

	protected CellContainer ActiveCell;

	protected List<CellContainer> allCells = new List<CellContainer>();

	protected List<CellContainer> allCellsArena = new List<CellContainer>();

	private bool chek;

	protected bool IsEnoughFuel => GameEnergyLogic.isEnoughForRace;

	protected virtual void Awake()
	{
		Game.OnStateChange(Game.gameState.Levels);
		if (Progress.shop.Incubator_Eggs == null || Progress.shop.Incubator_Eggs.Count == 0)
		{
			for (int i = 0; i < 10; i++)
			{
				Progress.shop.Incubator_Eggs.Add(item: false);
			}
		}
		Time.timeScale = 1f;
		Progress.levels.Pack(1).Level(1).isOpen = true;
		SetRubiesCount(Progress.shop.currency);
		SetEnergy(GameEnergyLogic.GetEnergy);
		activePack = Progress.levels.active_pack;
		activeLevel = Progress.levels.active_level;
		SetActiveCarIndex(Progress.shop.activeCar);
		GetAndSetCells();
		StartCoroutine(forNextCell());
		SetAllCellsInteractable(interactable: false);
		SetActive(activePack, activeLevel);
		if (!LevelGalleryTutorial.needTutorial)
		{
			ShowCar(ActiveCell, delegate
			{
				SetAllCellsInteractable(interactable: true);
			});
		}
	}

	protected virtual void OnEnable()
	{
		if (Progress.shop.StartComixShow)
		{
		}
		GameEnergyLogic.OnFuelRestored = (Action<int>)Delegate.Combine(GameEnergyLogic.OnFuelRestored, new Action<int>(SetEnergy));
	}

	protected virtual void Update()
	{
		SetInfIco();
		SetRubiesCount(Progress.shop.currency);
	}

	protected virtual void OnDisable()
	{
		GameEnergyLogic.OnFuelRestored = (Action<int>)Delegate.Remove(GameEnergyLogic.OnFuelRestored, new Action<int>(SetEnergy));
	}

	protected virtual void SetActiveCarIndex(int index)
	{
	}

	protected virtual void SetRubiesCount(int rubies)
	{
	}

	public virtual void SetInfIco()
	{
	}

	protected virtual void SetEnergy(int energy)
	{
	}

	protected virtual void ScrollToLeft()
	{
		if (activePack > 1)
		{
			activePack--;
			SetActive(activePack, activeLevel);
		}
	}

	protected virtual void ScrollToRight()
	{
		if (activePack < 4)
		{
			activePack++;
			SetActive(activePack, activeLevel);
		}
	}

	protected virtual void SetActive(int pack, int level)
	{
		activePack = pack;
		activeLevel = level;
	}

	private IEnumerator forNextCell()
	{
		if (!Progress.levels.InUndegroundComixShowed && Progress.levels.InUndeground && Progress.levels.active_level_last_openned_under == 6)
		{
			yield break;
		}
		int packG = 0;
		int levelG = 0;
		yield return new WaitForSeconds(1f);
		if (Progress.levels.InUndeground)
		{
			if (Progress.levels.active_level_last_openned_under != 12 && Progress.levels.Pack(Progress.levels.active_pack_last_openned_under) != null && Progress.levels.Pack(Progress.levels.active_pack_last_openned_under).Level(Progress.levels.active_level_last_openned_under + 1).isOpen)
			{
				foreach (CellContainer allCell in allCells)
				{
					if (allCell.Pack == Progress.levels.active_pack_last_openned_under && allCell.Level == Progress.levels.active_level_last_openned_under && !allCell.BossLevel)
					{
						allCell.SetState(CellContainer.State.Available, Progress.levels.Pack(allCell.Pack).Level(allCell.Level).oldticket);
					}
					if (allCell.Pack == Progress.levels.active_pack_last_openned_under && allCell.Level == Progress.levels.active_level_last_openned_under + 1)
					{
						allCell.SetState(CellContainer.State.Active, Progress.levels.Pack(allCell.Pack).Level(allCell.Level).oldticket, isActive: true);
						allCell.ActiveRotate.anchoredPosition = allCell.gameObject.GetComponent<RectTransform>().anchoredPosition;
						allCell.ButtonPlay.Act = allCell.Gogogogo;
					}
				}
				packG = Progress.levels.active_pack_last_openned_under;
				levelG = Progress.levels.active_level_last_openned_under + 1;
			}
		}
		else if (Progress.levels.active_level_last_openned != 12 && Progress.levels.active_pack_last_openned != 3)
		{
			if (Progress.levels.active_level_last_openned == 12)
			{
				if (Progress.levels.Pack(Progress.levels.active_pack_last_openned + 1).Level(1).isOpen)
				{
					int num = 1;
					int num2 = Progress.levels.active_pack_last_openned + 1;
					foreach (CellContainer allCell2 in allCells)
					{
						if (allCell2.Pack == Progress.levels.active_pack_last_openned && allCell2.Level == Progress.levels.active_level_last_openned)
						{
							if (allCell2.Active != null)
							{
								allCell2.Active.enabled = true;
							}
							allCell2.SetState(CellContainer.State.Available, Progress.levels.Pack(allCell2.Pack).Level(allCell2.Level).oldticket);
						}
						if (allCell2.Pack == num2 && allCell2.Level == num && !allCell2.ArenaNew && !allCell2.BossLevel)
						{
							if (allCell2.Active != null)
							{
								allCell2.Active.enabled = true;
							}
							allCell2.SetState(CellContainer.State.Active, Progress.levels.Pack(allCell2.Pack).Level(allCell2.Level).oldticket, isActive: true);
							allCell2.ActiveRotate.anchoredPosition = allCell2.gameObject.GetComponent<RectTransform>().anchoredPosition;
							UnityEngine.Debug.Log("!#&^$@!!! zapus action for play buttons");
							allCell2.ButtonPlay.Act = allCell2.Gogogogo;
						}
					}
					packG = num2;
					levelG = num;
				}
			}
			else if (Progress.levels.Pack(Progress.levels.active_pack_last_openned) != null && Progress.levels.Pack(Progress.levels.active_pack_last_openned).Level(Progress.levels.active_level_last_openned + 1).isOpen)
			{
				foreach (CellContainer allCell3 in allCells)
				{
					if (allCell3.Pack == Progress.levels.active_pack_last_openned && allCell3.Level == Progress.levels.active_level_last_openned)
					{
						allCell3.SetState(CellContainer.State.Available, Progress.levels.Pack(allCell3.Pack).Level(allCell3.Level).oldticket);
					}
					if (allCell3.Pack == Progress.levels.active_pack_last_openned && allCell3.Level == Progress.levels.active_level_last_openned + 1)
					{
						allCell3.SetState(CellContainer.State.Active, Progress.levels.Pack(allCell3.Pack).Level(allCell3.Level).oldticket, isActive: true);
						allCell3.ActiveRotate.anchoredPosition = allCell3.gameObject.GetComponent<RectTransform>().anchoredPosition;
						allCell3.ButtonPlay.Act = allCell3.Gogogogo;
					}
				}
				packG = Progress.levels.active_pack_last_openned;
				levelG = Progress.levels.active_level_last_openned + 1;
			}
		}
		if (packG != 0)
		{
			if (Progress.levels.InUndeground)
			{
				Progress.levels.active_pack_last_openned_under = packG;
			}
			else
			{
				Progress.levels.active_pack_last_openned = packG;
			}
		}
		if (levelG != 0)
		{
			if (Progress.levels.InUndeground)
			{
				Progress.levels.active_level_last_openned_under = levelG;
			}
			else
			{
				Progress.levels.active_level_last_openned = levelG;
			}
		}
	}

	private void GetAndSetCells()
	{
		allCells.Clear();
		allCells.AddRange(UnityEngine.Object.FindObjectsOfType<CellContainer>());
		foreach (CellContainer allCell in allCells)
		{
			if (!Progress.levels.Pack(allCell.Pack).Level(allCell.Level).isOpen)
			{
				allCell.SetState(CellContainer.State.Closed);
			}
			else
			{
				bool flag = false;
				if (!allCell.BossLevel && !allCell.SpecialLevel)
				{
					allCell.SetState(CellContainer.State.Available, Progress.levels.Pack(allCell.Pack).Level(allCell.Level).oldticket);
					if (Progress.levels.InUndeground)
					{
						if (Progress.levels.active_pack_last_openned_under > Progress.levels.Max_Active_Pack_under)
						{
							flag = (allCell.Pack == Progress.levels.Max_Active_Pack_under && allCell.Level == Progress.levels.Max_Active_Level_under);
						}
						else if (Progress.levels.active_level_last_openned_under > Progress.levels.Max_Active_Level_under)
						{
							flag = (allCell.Pack == Progress.levels.Max_Active_Pack_under && allCell.Level == Progress.levels.Max_Active_Level_under);
						}
						else if (Progress.levels.active_level_last_openned_under == -1 && Progress.levels.active_pack_last_openned_under == -1)
						{
							flag = (allCell.Pack == Progress.levels.Max_Active_Pack_under && allCell.Level == Progress.levels.Max_Active_Level_under);
							Progress.levels.active_level_last_openned_under = Progress.levels.Max_Active_Level_under;
							Progress.levels.active_pack_last_openned_under = Progress.levels.Max_Active_Pack_under;
						}
						else
						{
							flag = (allCell.Pack == Progress.levels.active_pack_last_openned_under && allCell.Level == Progress.levels.active_level_last_openned_under);
						}
					}
					else if (Progress.levels.active_pack_last_openned > Progress.levels.Max_Active_Pack)
					{
						flag = (allCell.Pack == Progress.levels.Max_Active_Pack && allCell.Level == Progress.levels.Max_Active_Level);
					}
					else if (Progress.levels.active_level_last_openned > Progress.levels.Max_Active_Level)
					{
						flag = (allCell.Pack == Progress.levels.Max_Active_Pack && allCell.Level == Progress.levels.Max_Active_Level);
					}
					else if (Progress.levels.active_level_last_openned == -1 && Progress.levels.active_pack_last_openned == -1)
					{
						flag = (allCell.Pack == Progress.levels.Max_Active_Pack && allCell.Level == Progress.levels.Max_Active_Level);
						Progress.levels.active_level_last_openned = Progress.levels.Max_Active_Level;
						Progress.levels.active_pack_last_openned = Progress.levels.Max_Active_Pack;
					}
					else
					{
						flag = (allCell.Pack == Progress.levels.active_pack_last_openned && allCell.Level == Progress.levels.active_level_last_openned);
					}
					if (flag)
					{
						allCell.SetState(CellContainer.State.Active, Progress.levels.Pack(allCell.Pack).Level(allCell.Level).oldticket);
						allCell.ActiveRotate.anchoredPosition = allCell.gameObject.GetComponent<RectTransform>().anchoredPosition;
						allCell.ButtonPlay.Act = allCell.Gogogogo;
					}
				}
				if (allCell.BossLevel)
				{
					int num = 0;
					for (int i = 1; i <= 3; i++)
					{
						for (int j = 1; j <= 12; j++)
						{
							num += Progress.levels.Pack(i).Level(j).oldticket;
						}
					}
					if (allCell.Pack == 1)
					{
						if (num >= DifficultyConfig.instance.BudgesBoss1)
						{
							allCell.SetState(CellContainer.State.Available);
						}
						else
						{
							allCell.SetState(CellContainer.State.Closed);
						}
					}
					else if (allCell.Pack == 2)
					{
						if (num >= DifficultyConfig.instance.BudgesBoss2)
						{
							allCell.SetState(CellContainer.State.Available);
						}
						else
						{
							allCell.SetState(CellContainer.State.Closed);
						}
					}
					else if (allCell.Pack == 3)
					{
						if (num >= DifficultyConfig.instance.BudgesBoss3)
						{
							allCell.SetState(CellContainer.State.Available);
						}
						else
						{
							allCell.SetState(CellContainer.State.Closed);
						}
					}
				}
				if (flag)
				{
					ActiveCell = allCell;
				}
			}
			CellContainer cellContainer = allCell;
			cellContainer.OnCellClick = (Action<CellContainer>)Delegate.Combine(cellContainer.OnCellClick, new Action<CellContainer>(OnCellClicked));
		}
		StartCoroutine(animattorOff(allCells));
	}

	private IEnumerator animattorOff(List<CellContainer> cell)
	{
		yield return new WaitForSeconds(1f);
		for (int i = 0; i < cell.Count; i++)
		{
			if (cell[i].Active != null)
			{
				cell[i].Active.enabled = false;
			}
		}
	}

	public void MissionsLEX(CellContainer cell)
	{
	}

	protected void OnCellClicked(CellContainer cell)
	{
		if (cell.BossLevel)
		{
			if (cell.state == CellContainer.State.Closed)
			{
				MapCars mapCars = UnityEngine.Object.FindObjectOfType<MapCars>();
				mapCars.BossLockOpen(cell.Pack);
				return;
			}
			OnAvailableClick(cell);
			if (GameEnergyLogic.isEnoughForRace)
			{
				ActiveCell.SetState(CellContainer.State.Available);
			}
		}
		else if (cell.SpecialLevel && !IsEnoughFuel)
		{
			NotEnoughFuel();
		}
		else if (Progress.levels.Pack(cell.Pack).Level(cell.Level).isOpen)
		{
			OnAvailableClick(cell);
		}
		else
		{
			OnClosedLevelClick(cell);
		}
	}

	protected virtual void OnAvailableClick(CellContainer cell)
	{
		SetAllCellsInteractable(interactable: false);
		Progress.levels.active_pack = (byte)cell.Pack;
		Progress.levels.active_level = (byte)cell.Level;
		if (ActiveCell != cell)
		{
			TakeFuelAndGo();
		}
		else
		{
			TakeFuelAndGo();
		}
	}

	protected virtual void HideCar(CellContainer cell, Action onComplete)
	{
		int oldticket = Progress.levels.Pack(cell.Pack).Level(cell.Level).oldticket;
		cell.SetState(CellContainer.State.Available, oldticket);
	}

	protected virtual void ShowCar(CellContainer cell, Action onComplete)
	{
		if (!Progress.shop.endlessLevel)
		{
			ActiveCell = cell;
			int oldticket = Progress.levels.Pack(cell.Pack).Level(cell.Level).oldticket;
			cell.SetState(CellContainer.State.Active, oldticket, isActive: true);
		}
	}

	protected virtual void TakeFuelAndGo()
	{
	}

	protected virtual void OnClosedLevelClick(CellContainer cell)
	{
		SetInAppPrices(cell);
		Game.PushInnerState("levels_buy", ButtonBuyClose);
	}

	protected virtual void LoadRace()
	{
		UnityEngine.SceneManagement.SceneManager.LoadScene("Race_preloader");
	}

	protected int GetFuelForRace()
	{
		GameEnergyLogic.GetFuelForRace();
		return PriceConfig.instance.energy.eachStart;
	}

	protected void SetAllCellsInteractable(bool interactable)
	{
		allCells.ForEach(delegate(CellContainer c)
		{
			c.button.interactable = interactable;
		});
	}

	protected virtual void NotEnoughFuel()
	{
		SetAllCellsInteractable(interactable: true);
		GUI_shop.instance.ShowBuyCanvasWindow();
	}

	protected virtual void ButtonBuyClose()
	{
		Game.PopInnerState("levels_buy", executeClose: false);
	}

	protected virtual void ButtonBuyAll()
	{
		if (DebugFacade.isDebug)
		{
			ModalWindow.instance.Show("Debug buy all location", new ButtonInfo("Buy", OpenAllLoc));
		}
		else
		{
			//Purchaser.BuyProductID(Purchaser.UnlockNext, OpenAllLoc);
		}
	}

	protected virtual void ButtonBuyPack()
	{
		if (DebugFacade.isDebug)
		{
			if (Progress.shop.BuyPack == -1)
			{
				Progress.shop.BuyPack = activePack;
			}
			int num = 0;
			switch (Progress.shop.BuyPack)
			{
			case 1:
				num = ((!Progress.levels.InUndeground) ? 1 : 4);
				break;
			case 2:
				num = ((!Progress.levels.InUndeground) ? 2 : 5);
				break;
			case 3:
				num = 3;
				break;
			}
			ModalWindow.instance.Show("Debug buy location" + num, new ButtonInfo("Buy", OpenLocation));
			return;
		}
		//string productId = Purchaser.UnlockWorld1;
		//if (Progress.shop.BuyPack == -1)
		//{
		//	Progress.shop.BuyPack = activePack;
		//}
		//switch (Progress.shop.BuyPack)
		//{
		//case 1:
		//	productId = ((!Progress.levels.InUndeground) ? Purchaser.UnlockWorld1 : Purchaser.UnlockWorldUnder);
		//	break;
		//case 2:
		//	productId = ((!Progress.levels.InUndeground) ? Purchaser.UnlockWorld2 : Purchaser.UnlockWorldUnder2);
		//	break;
		//case 3:
		//	productId = Purchaser.UnlockWorld3;
		//	break;
		//}
		//Purchaser.BuyProductID(productId, OpenLocation);
		Progress.shop.BuyPack = -1;
	}

	protected virtual void ButtonBackClicked()
	{
		Game.LoadLevel("Menu");
	}

	protected virtual void ButtonShopClicked()
	{
		if (Game.currentState != Game.gameState.Shop)
		{
			Game.OnStateChange(Game.gameState.Shop);
		}
		UnityEngine.SceneManagement.SceneManager.LoadScene("garage_new");
	}

	protected virtual void ButtonMonstroClicked()
	{
		if (Game.currentState != Game.gameState.Monstropedia)
		{
			Game.OnStateChange(Game.gameState.Monstropedia);
		}
		UnityEngine.SceneManagement.SceneManager.LoadScene("monstropedia_new");
	}

	protected virtual void ButtonPoliceClicked()
	{
		if (Game.currentState != Game.gameState.Monstropedia)
		{
			Game.OnStateChange(Game.gameState.Monstropedia);
		}
		UnityEngine.SceneManagement.SceneManager.LoadScene("policepedia_new");
	}

	protected virtual void ButtonPremiumShopClicked()
	{
		Progress.shop.premiumShop = true;
		Progress.shop.premiumShopforFirst = false;
		GUI_shop.instance.ShowPremiumShop();
	}

	protected virtual void SetInAppPrices(CellContainer cell)
	{
	}

	private void OpenNextLevel()
	{
		int i = GetLastAvailableLevelInPack(activePack) + 1;
		if (Progress.levels.Pack(activePack).Level(1).isOpen)
		{
			Progress.levels.Pack(activePack).Level(i).isOpen = true;
		}
		else
		{
			Progress.levels.Pack(activePack).Level(1).isOpen = true;
			Progress.levels.Pack(activePack).Level(1).lgChainDropped = true;
		}
		Audio.PlayAsync("shop_purchase");
		UpdateCells();
		OnPurchaseSuccess();
		Progress.review.atLeastOnePurchase = true;
	}

	protected virtual void OnPurchaseSuccess()
	{
	}

	private int GetLastAvailableLevelInPack(int pack)
	{
		for (byte b = 1; b <= 9; b = (byte)(b + 1))
		{
			if (!Progress.levels.Pack(pack).Level(b).isOpen)
			{
				return b - 1;
			}
		}
		return 9;
	}

	private void OpenAllLoc()
	{
		bool inUndeground = Progress.levels.InUndeground;
		bool undeground = Progress.shop.Undeground2;
		Progress.levels.InUndeground = false;
		Progress.shop.Undeground2 = false;
		Progress.levels.Max_Active_Level = 12;
		Progress.levels.Max_Active_Pack = 3;
		for (int i = 1; i <= 3; i++)
		{
			for (int j = 1; j <= 12; j++)
			{
				if (!Progress.levels.Pack(i).Level(j).isOpen)
				{
					Progress.levels.Pack(i).Level(j).isOpen = true;
				}
			}
		}
		Progress.levels.InUndeground = true;
		Progress.shop.Undeground2 = true;
		for (int k = 1; k <= 3; k++)
		{
			for (int l = 1; l <= 12; l++)
			{
				if (!Progress.levels.Pack(k).Level(l).isOpen)
				{
					Progress.levels.Pack(k).Level(l).isOpen = true;
				}
				if (!Progress.levels._packUnderground[k]._level[l].isOpen)
				{
					Progress.levels._packUnderground[k]._level[l].isOpen = true;
				}
			}
		}
		Audio.PlayAsync("shop_purchase");
		Progress.levels.InUndeground = false;
		Progress.shop.Undeground2 = false;
		Progress.levels.InUndeground = inUndeground;
		Progress.shop.Undeground2 = undeground;
		AnalyticsManager.LogEvent(EventCategoty.premium_shop, "unlock_all_levels", Progress.levels.active_pack.ToString() + "_" + Progress.levels.active_level.ToString());
		UpdateCells();
		OnPurchaseSuccess();
		Progress.review.atLeastOnePurchase = true;
	}

	private void OpenLocation()
	{
		if (Progress.shop.BuyPack == -1)
		{
			Progress.shop.BuyPack = activePack;
		}
		while (!chek)
		{
			if (!Progress.levels.Pack(Progress.shop.BuyPack).isOpen)
			{
				Progress.levels.Max_Active_Level = 12;
				Progress.levels.Pack(Progress.shop.BuyPack).isOpen = true;
				for (int i = 1; i <= 12; i++)
				{
					if (!Progress.levels.Pack(Progress.shop.BuyPack).Level(i).isOpen)
					{
						Progress.levels.Pack(Progress.shop.BuyPack).Level(i).isOpen = true;
						chek = true;
					}
				}
			}
			else
			{
				Progress.shop.BuyPack++;
			}
		}
		chek = false;
		Progress.levels.Max_Active_Pack = (byte)Progress.shop.BuyPack;
		Audio.PlayAsync("shop_purchase");
		switch (Progress.shop.BuyPack)
		{
		case 1:
			if (Progress.levels.InUndeground)
			{
				AnalyticsManager.LogEvent(EventCategoty.premium_shop, "unlock_world4", Progress.levels.active_pack.ToString() + "_" + Progress.levels.active_level.ToString());
			}
			else
			{
				AnalyticsManager.LogEvent(EventCategoty.premium_shop, "unlock_world1", Progress.levels.active_pack.ToString() + "_" + Progress.levels.active_level.ToString());
			}
			break;
		case 2:
			if (Progress.levels.InUndeground)
			{
				AnalyticsManager.LogEvent(EventCategoty.premium_shop, "unlock_world5", Progress.levels.active_pack.ToString() + "_" + Progress.levels.active_level.ToString());
			}
			else
			{
				AnalyticsManager.LogEvent(EventCategoty.premium_shop, "unlock_world2", Progress.levels.active_pack.ToString() + "_" + Progress.levels.active_level.ToString());
			}
			break;
		case 3:
			AnalyticsManager.LogEvent(EventCategoty.premium_shop, "unlock_world3", Progress.levels.active_pack.ToString() + "_" + Progress.levels.active_level.ToString());
			break;
		}
		UpdateCells();
		OnPurchaseSuccess();
		Progress.review.atLeastOnePurchase = true;
	}

	private void UpdateCells()
	{
		foreach (CellContainer allCell in allCells)
		{
			if (allCell.state == CellContainer.State.Closed && !allCell.BossLevel && Progress.levels.Pack(allCell.Pack).Level(allCell.Level).isOpen)
			{
				int oldticket = Progress.levels.Pack(allCell.Pack).Level(allCell.Level).oldticket;
				allCell.SetState(CellContainer.State.Available, oldticket);
			}
		}
	}
}
