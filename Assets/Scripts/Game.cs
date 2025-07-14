using Smokoko.Social;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Game : GameBase
{
	public enum gameState
	{
		None,
		Preloader,
		Menu,
		Comics,
		Levels,
		Shop,
		ToDoList,
		PausedRace,
		PreRace,
		Race,
		Finish,
		FinishLose,
		PrevState,
		Tutorial,
		Fortune,
		OpenWindow,
		Monstropedia,
		Revive
	}

	[Serializable]
	private class InnerState
	{
		public string inner_id;

		public Action onClose;

		public InnerState(string inner_id, Action onClose)
		{
			this.inner_id = inner_id;
			this.onClose = onClose;
		}
	}

	private static Game _game;

	public static bool BlockSpavnRubi;

	private static gameState p_currentState;

	private static gameState p_previousState = gameState.Menu;

	[HideInInspector]
	public string nextLev = string.Empty;

	private bool raceAtleastOne;

	private Stack<InnerState> innerStates = new Stack<InnerState>();

	private static Game instance
	{
		get
		{
			if (_game == null)
			{
				GameObject gameObject = new GameObject("_gameManager");
				_game = gameObject.AddComponent<Game>();
				UnityEngine.Object.DontDestroyOnLoad(gameObject);
				AnalyticsManager.StartSession();
				FBLeaderboard.Init();
			}
			return _game;
		}
	}

	public static gameState currentState
	{
		get
		{
			return p_currentState;
		}
		set
		{
			p_currentState = value;
		}
	}

	public static gameState previousState
	{
		get
		{
			return p_previousState;
		}
		private set
		{
			p_previousState = value;
		}
	}

	public override void Awake()
	{
		base.Awake();
		ScoreManager.OnConnection = (Action<bool>)Delegate.Combine(ScoreManager.OnConnection, new Action<bool>(OnGPConnectionChanged));
	}

	private void OnGPConnectionChanged(bool isConnected)
	{
		if (isConnected)
		{
			PlayerPrefs.SetInt("gp_connect", 1);
		}
		else
		{
			PlayerPrefs.SetInt("gp_connect", 0);
		}
	}

	public static int GPWasConnected()
	{
		return PlayerPrefs.GetInt("gp_connect", -1);
	}

	public static void InitGameCenter()
	{
		UnityEngine.Debug.Log("Initing Game Center Wrapper");
		GameCenterWrapper.Init();
		instance.StartCoroutine(WaitForGameCenter());
	}

	private static IEnumerator WaitForGameCenter()
	{
		UnityEngine.Debug.Log("WAITING FOR GAME_CENTER");
		while (!Progress.settings.LoginToGP)
		{
			yield return 0;
		}
		UnityEngine.Debug.Log("WAITING FOR GAME_CENTER_COMPLETED");
		GameCenterWrapper.LoadGameSaves();
	}

	public static void OnSavesLoaded()
	{
		UnityEngine.Debug.Log("Going to Show Cloud Progress Change on SCENE ->" + currentState);
		instance.StartCoroutine(WaitForSaves());
	}

	private static IEnumerator WaitForSaves()
	{
		while (!SceneManager.GetActiveScene().name.Contains("map_new"))
		{
			yield return 0;
		}
		UnityEngine.Debug.Log("CLOUD_SAVE_SHOW");
		yield return new WaitForSeconds(1f);
		GameCenterWrapper.ShowCloudProgress();
	}

	public static void OnStateChange(gameState gs, bool blockSound = false)
	{
		if (gs == gameState.PrevState)
		{
			gs = p_currentState;
			p_currentState = p_previousState;
			p_previousState = gs;
		}
		else
		{
			p_previousState = p_currentState;
			p_currentState = gs;
		}
		switch (p_currentState)
		{
		case gameState.Preloader:
			GameBase.TogglePause(pause: false);
			break;
		case gameState.Levels:
			GameBase.TogglePause(pause: false);
			break;
		case gameState.PausedRace:
			GameBase.TogglePause(pause: true);
			break;
		case gameState.PreRace:
			instance.raceAtleastOne = true;
			GameBase.TogglePause(pause: false);
			break;
		case gameState.Race:
			GameBase.TogglePause(pause: false);
			break;
		case gameState.Finish:
			RaceLogic.instance.AllInitedForPool = false;
			break;
		case gameState.FinishLose:
			RaceLogic.instance.AllInitedForPool = false;
			break;
		}
		instance.StartCoroutine(instance.delayToMusicPlay(blockSound));
		instance.innerStates.Clear();
	}

	private IEnumerator delayToMusicPlay(bool blockSound = false)
	{
		Action playBgMusic = delegate
		{
			if (!Audio.isBackgroundMusicPlaying)
			{
				if (!instance.raceAtleastOne)
				{
					Audio.PlayBackgroundMusic("music_interface");
				}
				else
				{
					Audio.PlayBackgroundMusic("music_interface");
				}
			}
		};
		switch (p_currentState)
		{
		case gameState.Menu:
			playBgMusic();
			break;
		case gameState.Comics:
			playBgMusic();
			break;
		case gameState.Levels:
			if (p_previousState != gameState.Shop && Progress.shop.StartComixShow)
			{
				playBgMusic();
			}
			break;
		case gameState.Shop:
			if (p_previousState != gameState.Levels)
			{
				Audio.PlayBackgroundMusic("music_interface");
			}
			break;
		case gameState.ToDoList:
			Audio.StopBackgroundMusic();
			break;
		case gameState.PausedRace:
			Audio.PauseBackgroundMusic();
			Audio.Pause();
			break;
		case gameState.Race:
			if (!blockSound && (p_previousState == gameState.PausedRace || p_previousState == gameState.Race))
			{
				Audio.ResumeBackgroundMusic();
				Audio.Resume();
			}
			break;
		case gameState.Finish:
			if (previousState == gameState.Race)
			{
				Audio.PlayBackgroundMusic("music_level_end", 1f, loop: false);
				instance.StartCoroutine(instance.playInterfaceAfterFinish());
			}
			break;
		case gameState.FinishLose:
			Audio.StopBackgroundMusic();
			if (previousState == gameState.Race)
			{
				instance.StartCoroutine(instance.playInterfaceAfterFinish());
			}
			break;
		}
		yield return 0;
	}

	public static void PauseAudio()
	{
		Audio.Pause();
		Audio.PauseBackgroundMusic();
	}

	public static void ResumeAudio()
	{
		Audio.Resume();
		Audio.ResumeBackgroundMusic();
	}

	private IEnumerator playInterfaceAfterFinish()
	{
		while (true)
		{
			if (currentState == gameState.PreRace || currentState == gameState.Levels)
			{
				yield break;
			}
			if (!Audio.isBackgroundMusicPlaying)
			{
				break;
			}
			yield return null;
		}
		Audio.PlayBackgroundMusic("music_interface");
	}

	public void MoreGames()
	{
	}

	public int RandomMusic()
	{
		return UnityEngine.Random.Range(1, 6);
	}

	public static void LoadLevel(string nextScene)
	{
		instance.p_LoadLevel(nextScene);
		instance.nextLev = nextScene;
		if (!instance.nextLev.Contains("_"))
		{
			instance.OnStateChange(instance.nextLev);
		}
	}

	private void LoadLevel(int pack, int level)
	{
		LoadLevel("Race");
	}

	private void Start()
	{
		preloaderSceneName = "Preloader";
		GameObject gameObject = new GameObject("_quitChecker");
		gameObject.AddComponent<QuitGame>();
		UnityEngine.Object.DontDestroyOnLoad(gameObject);
	}

	private void OnApplicationPause(bool pauseStatus)
	{
		if (!pauseStatus)
		{
			NotificationsWrapper.HideAllNotifications();
		}
	}

	public static void PushInnerState(string inner_id, Action onClose)
	{
		instance.innerStates.Push(new InnerState(inner_id, onClose));
	}

	public static bool PopInnerState(string inner_id = null, bool executeClose = true)
	{
		if (instance.innerStates.Count > 0)
		{
			if (string.IsNullOrEmpty(inner_id))
			{
				if (instance.innerStates.Peek().inner_id == "shop_load")
				{
					return true;
				}
				InnerState innerState = instance.innerStates.Pop();
				if (executeClose)
				{
					innerState.onClose();
				}
			}
			else if (instance.innerStates.Peek().inner_id == inner_id)
			{
				InnerState innerState2 = instance.innerStates.Pop();
				if (executeClose)
				{
					innerState2.onClose();
				}
			}
			return true;
		}
		return false;
	}
}
