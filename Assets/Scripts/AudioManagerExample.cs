using SMKK;
using System.Collections.Generic;
using System.IO;
using System.Xml;
using System.Xml.Serialization;
using UnityEngine;

public class AudioManagerExample : MonoBehaviour
{
	private List<string> fileNames = new List<string>();

	private string id;

	public static float musicVolume
	{
		get
		{
			return AudioManager.instance.musicVolume;
		}
		set
		{
			AudioManager.instance.musicVolume = value;
		}
	}

	public static float soundVolume
	{
		get
		{
			return AudioManager.instance.soundVolume;
		}
		set
		{
			AudioManager.instance.soundVolume = value;
		}
	}

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

	private void Start()
	{
		AudioManager.instance.musicVolume = 1f;
		AudioManager.instance.soundVolume = 1f;
		getAllFiles();
	}

	private void getAllFiles()
	{
		fileNames = new List<string>();
		TextAsset textAsset = Resources.Load("audio") as TextAsset;
		if (!(textAsset == null))
		{
			AudioSerialization audioSerialization = Deserialize<AudioSerialization>(textAsset.text);
			for (int i = 0; i < audioSerialization.files.Count; i++)
			{
				fileNames.Add(audioSerialization.files[i].fileName);
			}
		}
	}

	private T Deserialize<T>(string details)
	{
		XmlSerializer xmlSerializer = new XmlSerializer(typeof(T));
		XmlReader xmlReader = XmlReader.Create(new StringReader(details));
		return (T)xmlSerializer.Deserialize(xmlReader);
	}

	private void OnGUI()
	{
		if (fileNames.Count == 0)
		{
			GUI.Label(new Rect(10f, 10f, 250f, 250f), "No audio files found. Please add some \"*.wav\" or \"*.mp3\" files to the project.");
			return;
		}
		string fileName = fileNames[Random.Range(0, fileNames.Count)];
		if (id == null)
		{
			if (GUI.Button(new Rect(10f, 10f, 150f, 75f), "Play random sound"))
			{
				Play(fileName);
			}
		}
		else if (GUI.Button(new Rect(10f, 10f, 150f, 75f), "Stop " + id))
		{
			Stop(id);
			id = null;
		}
		if (GUI.Button(new Rect(180f, 10f, 150f, 75f), "PlayAsync random sound"))
		{
			PlayAsync(fileName);
		}
		if (GUI.Button(new Rect(350f, 10f, 150f, 75f), "PlayBackgroundMusic"))
		{
			PlayBackgroundMusic(fileName);
		}
		if (GUI.Button(new Rect(350f, 100f, 150f, 75f), "StopBackgroundMusic"))
		{
			StopBackgroundMusic();
		}
		if (GUI.Button(new Rect(350f, 200f, 150f, 75f), (!AudioManager.instance.pausedBackground) ? "PauseBackgoundMusic" : "ResumeBackground"))
		{
			if (AudioManager.instance.pausedBackground)
			{
				AudioManager.instance.ResumeBackgroundMusic();
			}
			else
			{
				AudioManager.instance.PauseBackgroundMusic();
			}
		}
		if (GUI.Button(new Rect(10f, 100f, 150f, 75f), "Stop all sounds"))
		{
			Stop();
			id = null;
		}
		if (GUI.Button(new Rect(10f, 200f, 150f, 75f), (!AudioManager.instance.paused) ? "Pause all sounds" : "Resume all sounds"))
		{
			if (AudioManager.instance.paused)
			{
				AudioManager.instance.Resume();
			}
			else
			{
				AudioManager.instance.Pause();
			}
		}
	}

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
		return AudioManager.instance.Play(fileName, AudioManager.instance.soundVolume, loop: false, async: true);
	}

	public static string PlayAsync(string fileName, float volume)
	{
		return AudioManager.instance.Play(fileName, volume, loop: false, async: true);
	}

	public static string PlayAsync(string fileName, float volume, bool loop)
	{
		return AudioManager.instance.Play(fileName, volume, loop, async: true);
	}

	public static string PlayAsyncRandom(params string[] names)
	{
		return PlayAsync(names[Random.Range(0, names.Length)]);
	}

	public static void PlayBackgroundMusic(string fileName)
	{
		AudioManager.instance.PlayBackgroundMusic(fileName, AudioManager.instance.musicVolume);
	}

	public static void PlayBackgroundMusic(string fileName, float volume)
	{
		AudioManager.instance.PlayBackgroundMusic(fileName, volume);
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
