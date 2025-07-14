using System.Collections;
using UnityEngine;

public class GameBase : MonoBehaviour
{
	public virtual string nextScene
	{
		get;
		set;
	}

	public virtual string preloaderSceneName
	{
		get;
		set;
	}

	public virtual void Awake()
	{
	}

	public virtual void InitAll()
	{
	}

	public virtual void p_LoadLevel(string _nextLevel)
	{
		if (!Application.CanStreamedLevelBeLoaded(_nextLevel))
		{
			UnityEngine.Debug.LogError(_nextLevel + " scene doesn't exists!");
			return;
		}
		nextScene = _nextLevel;
		if (preloaderSceneName != null)
		{
			if (Application.CanStreamedLevelBeLoaded(preloaderSceneName))
			{
				UnityEngine.SceneManagement.SceneManager.LoadScene(preloaderSceneName);
				return;
			}
			UnityEngine.Debug.LogError(preloaderSceneName + " scene doesn't exists!");
			UnityEngine.SceneManagement.SceneManager.LoadScene(_nextLevel);
		}
		else
		{
			UnityEngine.SceneManagement.SceneManager.LoadScene(_nextLevel);
		}
	}

	public virtual void OnStateChange(string state)
	{
	}

	public virtual void OnLevelWasLoaded(int level)
	{
		if (preloaderSceneName != null && UnityEngine.SceneManagement.SceneManager.GetActiveScene().name.Equals(preloaderSceneName))
		{
			StartCoroutine("LoadNextLevelAfterFrame");
		}
	}

	private IEnumerator LoadNextLevelAfterFrame()
	{
		yield return new WaitForEndOfFrame();
		yield return new WaitForEndOfFrame();
		LoadNextLevel();
	}

	private void LoadNextLevel()
	{
		UnityEngine.SceneManagement.SceneManager.LoadScene(nextScene);
	}

	public static bool TogglePause(bool pause)
	{
		if (pause)
		{
			Time.timeScale = 0f;
			return false;
		}
		Time.timeScale = 1f;
		return true;
	}

	public static bool TogglePause()
	{
		if (Time.timeScale == 0f)
		{
			Time.timeScale = 1f;
			return false;
		}
		Time.timeScale = 0f;
		return true;
	}
}
