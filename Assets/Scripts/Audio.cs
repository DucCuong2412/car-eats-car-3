using SMKK;
using UnityEngine;

public static class Audio
{
	public static float soundVolume => AudioManager.instance.soundVolume;

	public static bool pausedBackground => AudioManager.instance.pausedBackground;

	public static bool paused => AudioManager.instance.paused;

	public static bool soundEnabled
	{
		get
		{
			return AudioManager.instance.soundEnabled;
		}
		set
		{
			AudioManager.instance.soundEnabled = value;
		}
	}

	public static bool musicEnabled
	{
		get
		{
			return AudioManager.instance.musicEnabled;
		}
		set
		{
			AudioManager.instance.musicEnabled = value;
		}
	}

	public static bool isBackgroundMusicPlaying => AudioManager.instance.isBackgroundMusicPlaying;

	public static string Play(string fileName)
	{
		return AudioManager.instance.Play(fileName, AudioManager.instance.soundVolume, loop: false, async: false);
	}

	public static string Play(string fileName, float volume)
	{
		return AudioManager.instance.Play(fileName, volume, loop: false, async: false);
	}

	public static string Play(string fileName, float volume, bool loop, bool async = false)
	{
		return AudioManager.instance.Play(fileName, volume, loop, async);
	}

	public static string PlayRandom(params string[] names)
	{
		return Play(names[Random.Range(0, names.Length)]);
	}

	public static string PlayAsync(string fileName)
	{
		return AudioManager.instance.Play(fileName, AudioManager.instance.soundVolume, loop: false, async: false);
	}

	public static string PlayAsync(string fileName, float volume)
	{
		return AudioManager.instance.Play(fileName, volume, loop: false, async: false);
	}

	public static string PlayAsync(string fileName, float volume, bool loop)
	{
		return AudioManager.instance.Play(fileName, volume, loop, async: false);
	}

	public static string PlayAsyncRandom(params string[] names)
	{
		return PlayAsync(names[Random.Range(0, names.Length)]);
	}

	public static void PlayBackgroundMusic(string fileName)
	{
		AudioManager.instance.PlayBackgroundMusic(fileName, AudioManager.instance.musicVolume);
	}

	public static void PlayBackgroundMusic(string fileName, float volume, bool loop = true)
	{
		AudioManager.instance.PlayBackgroundMusic(fileName, volume, loop);
	}

	public static void PlayBackgroundMusicRandom(params string[] names)
	{
		PlayBackgroundMusic(names[Random.Range(0, names.Length)]);
	}

	public static void Pause()
	{
		AudioManager.instance.Pause();
	}

	public static void Resume()
	{
		AudioManager.instance.Resume();
	}

	public static void Stop(string id = null)
	{
		if (id == null)
		{
			AudioManager.instance.StopAll();
		}
		else
		{
			AudioManager.instance.Stop(id);
		}
	}

	public static void PauseBackgroundMusic()
	{
		AudioManager.instance.PauseBackgroundMusic();
	}

	public static void ResumeBackgroundMusic()
	{
		AudioManager.instance.ResumeBackgroundMusic();
	}

	public static void StopBackgroundMusic()
	{
		AudioManager.instance.StopBackgroundMusic();
	}

	public static bool IsSoundPlaying(string id)
	{
		return AudioManager.instance.IsSoundPlaying(id);
	}
}
