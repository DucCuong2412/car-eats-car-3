using System;
using UnityEngine;
using UnityEngine.UI;

public class NewControllerForButtonPlayOnMap : MonoBehaviour
{
	public Button EsterPlay;

	public Button EsterPlayVideo;

	public Button PLAY;

	//public Button PLAYARENA;

	//public Button PLAYARENAVIDEO;

	public GameObject AD;

	public Animation anim;

	public MapCars MC;

	public LevelGalleryCanvasView LevelGalleryCanvasView;

	[HideInInspector]
	public Action Act;

	public Action ActForEster;

	[HideInInspector]
	public Action ActTemp;

	private bool IsEnoughFuel => GameEnergyLogic.isEnoughForRace;

	private void OnEnable()
	{
		PLAY.onClick.AddListener(ClicBtn);
		//PLAYARENA.onClick.AddListener(ClicBtnARENA);
		//PLAYARENAVIDEO.onClick.AddListener(ClicBtnVideo);
		if (EsterPlay != null)
		{
			EsterPlay.onClick.AddListener(EsterClicBtn);
		}
		if (EsterPlayVideo != null)
		{
			EsterPlayVideo.onClick.AddListener(EsterClicBtnVideo);
		}
		if (ActForEster == null)
		{
			ActForEster = Act;
		}
	}

	private void OnDisable()
	{
		PLAY.onClick.RemoveAllListeners();
		//PLAYARENA.onClick.RemoveAllListeners();
		//PLAYARENAVIDEO.onClick.RemoveAllListeners();
		if (EsterPlay != null)
		{
			EsterPlay.onClick.RemoveAllListeners();
		}
		if (EsterPlayVideo != null)
		{
			EsterPlayVideo.onClick.RemoveAllListeners();
		}
	}

	public void ReinitListenersValentine()
	{
		if (EsterPlay != null)
		{
			EsterPlay.onClick.RemoveAllListeners();
		}
		if (EsterPlayVideo != null)
		{
			EsterPlayVideo.onClick.RemoveAllListeners();
		}
		if (EsterPlay != null)
		{
			EsterPlay.onClick.AddListener(EsterClicBtn);
		}
		if (EsterPlayVideo != null)
		{
			EsterPlayVideo.onClick.AddListener(EsterClicBtnVideo);
		}
	}

	private void Update()
	{
		//if (AD.activeSelf)
		//{
		//	PLAY.gameObject.SetActive(value: false);
		//}
		//else
		//{
		//	PLAY.gameObject.SetActive(value: true);
		//}
	}

	private void EsterClicBtnVideo()
	{
		NewWrapperForAppodeal.instance.ShowVideo(delegate(bool sucsess)
		{
			if (sucsess)
			{
				EsterClicBtn();
			}
			else
			{
				EsterPlayVideo.interactable = true;
			}
		}, delegate
		{
			GameObject gameObject = UnityEngine.Object.Instantiate(Resources.Load("noVideoBox", typeof(GameObject))) as GameObject;
			EsterPlayVideo.interactable = true;
		}, delegate
		{
			EsterPlayVideo.interactable = true;
		});
	}

	private void EsterClicBtn()
	{
		Progress.shop.endlessLevel = false;
		Progress.shop.ArenaNew = false;
		Progress.shop.bossLevel = false;
		Progress.shop.EsterLevelPlay = true;
		ActForEster();
	}

	private void ClicBtnVideo()
	{
		//PLAYARENAVIDEO.interactable = false;
		//PLAYARENA.interactable = false;
		NewWrapperForAppodeal.instance.ShowVideo(delegate(bool sucsess)
		{
			if (sucsess)
			{
				Act();
				//PLAYARENAVIDEO.interactable = true;
				//PLAYARENA.interactable = true;
			}
			else
			{
				//PLAYARENAVIDEO.interactable = true;
				//PLAYARENA.interactable = true;
			}
		}, delegate
		{
			GameObject gameObject = UnityEngine.Object.Instantiate(Resources.Load("noVideoBox", typeof(GameObject))) as GameObject;
			//PLAYARENAVIDEO.interactable = true;
			//PLAYARENA.interactable = true;
		}, delegate
		{
			//PLAYARENAVIDEO.interactable = true;
			//PLAYARENA.interactable = true;
		});
	}

	private void ClicBtn()
	{
		if (Act != null)
		{
			if (IsEnoughFuel)
			{
				PLAY.interactable = false;
			}
			Act();
			if (Progress.levels.InUndeground)
			{
				Progress.levels.InUndegroundPreloader = true;
			}
		}
	}

	private void ClicBtnARENA()
	{
		if (Act != null)
		{
			if (Progress.levels.active_pack_last_openned == 1 && Progress.shop.currency > DifficultyConfig.instance.RubinivForStartARENA1)
			{
				//PLAYARENA.interactable = false;
				Progress.shop.currency -= DifficultyConfig.instance.RubinivForStartARENA1;
				Act();
			}
			else if (Progress.levels.active_pack_last_openned == 2 && Progress.shop.currency > DifficultyConfig.instance.RubinivForStartARENA2)
			{
				//PLAYARENA.interactable = false;
				Progress.shop.currency -= DifficultyConfig.instance.RubinivForStartARENA2;
				Act();
			}
			else if (Progress.levels.active_pack_last_openned == 3 && Progress.shop.currency > DifficultyConfig.instance.RubinivForStartARENA3)
			{
				//PLAYARENA.interactable = false;
				Progress.shop.currency -= DifficultyConfig.instance.RubinivForStartARENA3;
				Act();
			}
			else
			{
				LevelGalleryCanvasView.ButtonBuyRealMoney();
			}
		}
	}
}
