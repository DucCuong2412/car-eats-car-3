using UnityEngine;

public class GameExample : MonoBehaviour
{
	private void Start()
	{
		UnityEngine.Debug.Log("Start");
		GAMEexample.instance.InitAll();
		GAMEexample.instance.preloaderSceneName = "Preloader";
	}
}
public class GAMEexample : GameBase
{
	private static GAMEexample _instance;

	private string _someGameState = "Level";

	public static GAMEexample instance
	{
		get
		{
			if (_instance == null)
			{
				GameObject gameObject = new GameObject("_Game");
				_instance = gameObject.AddComponent<GAMEexample>();
				Object.DontDestroyOnLoad(gameObject);
				UnityEngine.Debug.Log("create _Game");
			}
			return _instance;
		}
		private set
		{
			_instance = value;
		}
	}

	public void InitAll()
	{
		base.InitAll();
	}

	public void LoadLevel(string nextScene)
	{
		base.p_LoadLevel(nextScene);
	}

	public void OnLevelWasLoaded(int level)
	{
		base.OnLevelWasLoaded(level);
		if (UnityEngine.SceneManagement.SceneManager.GetActiveScene().name.Equals("Level"))
		{
		}
		OnStateChange(_someGameState);
	}

	public void OnStateChange(string state)
	{
		base.OnStateChange(state);
	}

	private void OnGUI()
	{
		if (GUI.Button(new Rect(150f, 10f, 100f, 75f), "Menu"))
		{
			instance.LoadLevel("Menu");
		}
		if (GUI.Button(new Rect(10f, 10f, 100f, 75f), "Shop"))
		{
			instance.LoadLevel("Shop");
		}
		if (GUI.Button(new Rect(310f, 10f, 100f, 75f), "Level"))
		{
			instance.LoadLevel("Level");
		}
	}
}
