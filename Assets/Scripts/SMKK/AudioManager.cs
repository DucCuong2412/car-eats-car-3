using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Xml;
using System.Xml.Serialization;
using UnityEngine;

namespace SMKK
{
	public class AudioManager : MonoBehaviour
	{
		private static string[] p_fileTypes = new string[2]
		{
			"*.mp3",
			"*.wav"
		};

		private GameObject p_go_music;

		private GameObject p_go_sound;

		private Dictionary<string, string> fileNames;

		private List<string> playList;

		private List<AudioSource> allClips = new List<AudioSource>();

		private AudioSource backgroundMusic;

		private bool p_paused;

		private bool p_pausedBackground;

		private float p_soundVolume = 1f;

		private float p_musicVolume = 1f;

		private bool _enabledSound = true;

		private bool _enabledMusic = true;

		private static AudioManager _instance;

		public bool paused
		{
			get
			{
				return p_paused;
			}
			set
			{
				p_paused = value;
				if (value)
				{
					Pause();
				}
				else
				{
					Resume();
				}
			}
		}

		public bool pausedBackground
		{
			get
			{
				return p_pausedBackground;
			}
			set
			{
				p_pausedBackground = value;
				if (value)
				{
					PauseBackgroundMusic();
				}
				else
				{
					ResumeBackgroundMusic();
				}
			}
		}

		public float soundVolume
		{
			get
			{
				return p_soundVolume;
			}
			set
			{
				p_soundVolume = value;
				List<AudioSource> list = new List<AudioSource>(allClips);
				foreach (AudioSource item in list)
				{
					item.volume = p_soundVolume;
				}
			}
		}

		public float musicVolume
		{
			get
			{
				return p_musicVolume;
			}
			set
			{
				p_musicVolume = value;
				if (backgroundMusic != null)
				{
					backgroundMusic.volume = p_musicVolume;
				}
			}
		}

		public bool soundEnabled
		{
			get
			{
				return _enabledSound;
			}
			set
			{
				_enabledSound = value;
				if (!_enabledSound)
				{
					StopAll();
				}
			}
		}

		public bool musicEnabled
		{
			get
			{
				return _enabledMusic;
			}
			set
			{
				_enabledMusic = value;
				if (!_enabledMusic)
				{
					PauseBackgroundMusic();
				}
				else
				{
					ResumeBackgroundMusic();
				}
			}
		}

		public bool isBackgroundMusicPlaying
		{
			get
			{
				if (!backgroundMusic)
				{
					return false;
				}
				if (backgroundMusic.isPlaying)
				{
					return true;
				}
				return false;
			}
		}

		public static AudioManager instance
		{
			get
			{
				if (_instance == null)
				{
					GameObject gameObject = new GameObject("_audio controller");
					_instance = gameObject.AddComponent<AudioManager>();
					_instance.Create();
				}
				return _instance;
			}
			set
			{
				_instance = value;
			}
		}

		private void Create()
		{
			Object.DontDestroyOnLoad(base.gameObject);
			base.gameObject.AddComponent<AudioListener>();
			p_go_music = new GameObject();
			p_go_music.name = "_music";
			p_go_music.transform.parent = base.gameObject.transform;
			p_go_sound = new GameObject();
			p_go_sound.name = "_sounds";
			p_go_sound.transform.parent = base.gameObject.transform;
			if (fileNames == null)
			{
				LoadAudioFiles();
			}
			if (playList == null)
			{
				playList = new List<string>();
			}
		}

		private void LoadAudioFiles()
		{
			fileNames = new Dictionary<string, string>();
			TextAsset textAsset = Resources.Load("audio") as TextAsset;
			AudioSerialization audioSerialization = Deserialize<AudioSerialization>(textAsset.text);
			for (int i = 0; i < audioSerialization.files.Count; i++)
			{
				fileNames.Add(audioSerialization.files[i].fileName, audioSerialization.files[i].fullPath);
			}
		}

		private static string Serialize<T>(object details)
		{
			XmlSerializer xmlSerializer = new XmlSerializer(typeof(T));
			MemoryStream memoryStream = new MemoryStream();
			xmlSerializer.Serialize(memoryStream, details);
			StreamReader streamReader = new StreamReader(memoryStream);
			memoryStream.Position = 0L;
			string result = streamReader.ReadToEnd();
			memoryStream.Flush();
			memoryStream = null;
			streamReader = null;
			return result;
		}

		private static T Deserialize<T>(string details)
		{
			XmlSerializer xmlSerializer = new XmlSerializer(typeof(T));
			XmlReader xmlReader = XmlReader.Create(new StringReader(details));
			return (T)xmlSerializer.Deserialize(xmlReader);
		}

		private void addToPlaylist(string clipName)
		{
			playList.Add(clipName);
		}

		private string getFullFileName(string shortName)
		{
			string value = null;
			fileNames.TryGetValue(shortName, out value);
			return value;
		}

		private void removeFromPlaylist(string clipName)
		{
			playList.Remove(clipName);
		}

		private void Update()
		{
			List<AudioSource> list = new List<AudioSource>(allClips);
			foreach (AudioSource item in list)
			{
				if (!item.isPlaying && item.time == 0f)
				{
					removeFromPlaylist(item.clip.name);
					allClips.Remove(item);
					UnityEngine.Object.Destroy(item);
				}
			}
		}

		public string Play(string fileName, float volume, bool loop, bool async)
		{
			if (!_enabledSound)
			{
				return null;
			}
			fileName = Path.GetFileNameWithoutExtension(fileName);
			string fullFileName = getFullFileName(fileName);
			if (fullFileName == null)
			{
				UnityEngine.Debug.LogWarning($"{fileName} was not found. Check the file and do  \"Tools/Audio/Generate Audio\" to generate this file.");
				return string.Empty;
			}
			string result = null;
			if (!async && playList.Contains(fileName))
			{
				return result;
			}
			AudioSource audioSource = null;
			audioSource = p_go_sound.AddComponent<AudioSource>();
			audioSource.clip = (Resources.Load(fullFileName + fileName) as AudioClip);
			audioSource.volume = volume;
			audioSource.loop = loop;
			if (!async)
			{
				addToPlaylist(fileName);
			}
			allClips.Add(audioSource);
			result = audioSource.clip.name;
			StartCoroutine(DelayToPlaySound(audioSource));
			return result;
		}

		private IEnumerator DelayToPlaySound(AudioSource source)
		{
			while (source.clip.loadState != AudioDataLoadState.Loaded)
			{
				yield return null;
			}
			source.Play();
		}

		public void ChengValue(string id, float volume)
		{
			int count = allClips.Count;
			int num = 0;
			while (true)
			{
				if (num < count)
				{
					if (allClips[num].clip.name == id)
					{
						break;
					}
					num++;
					continue;
				}
				return;
			}
			allClips[num].volume = volume;
		}

		public void Pause()
		{
			StartCoroutine(Pauseone());
		}

		private IEnumerator Pauseone()
		{
			yield return 0;
			if (_enabledSound)
			{
				p_paused = true;
				List<AudioSource> allSources = new List<AudioSource>(allClips);
				foreach (AudioSource item in allSources)
				{
					item.Pause();
				}
			}
		}

		public void Resume()
		{
			StartCoroutine(Resumeone());
		}

		private IEnumerator Resumeone()
		{
			yield return 0;
			if (_enabledSound)
			{
				List<AudioSource> allSources = new List<AudioSource>(allClips);
				foreach (AudioSource item in allSources)
				{
					item.Play();
				}
				p_paused = false;
			}
		}

		public void Stop(string id)
		{
			StartCoroutine(stopone(id));
		}

		private IEnumerator stopone(string id)
		{
			yield return 0;
			if (id != null && !(id == string.Empty))
			{
				List<AudioSource> allSources = new List<AudioSource>(allClips);
				foreach (AudioSource item in allSources)
				{
					if (item.clip.name == id)
					{
						item.Stop();
					}
				}
			}
		}

		public void StopAll()
		{
			StartCoroutine(StopAllone());
		}

		private IEnumerator StopAllone()
		{
			yield return 0;
			List<AudioSource> allSources = new List<AudioSource>(allClips);
			foreach (AudioSource item in allSources)
			{
				item.Stop();
			}
			p_paused = false;
		}

		public void PlayBackgroundMusic(string fileName, float volume, bool loop = true)
		{
			StartCoroutine(PlayBM(fileName, volume, loop));
		}

		private IEnumerator PlayBM(string fileName, float volume, bool loop = true)
		{
			yield return 0;
			p_pausedBackground = false;
			fileName = Path.GetFileNameWithoutExtension(fileName);
			string filePath = getFullFileName(fileName);
			if (filePath == null)
			{
				UnityEngine.Debug.LogWarning($"{fileName} was not found. Check the file and do  \"Tools/Audio/Generate Audio\" to generate this file.");
				yield break;
			}
			if (backgroundMusic == null)
			{
				backgroundMusic = p_go_music.AddComponent<AudioSource>();
			}
			backgroundMusic.clip = (Resources.Load(filePath + fileName) as AudioClip);
			backgroundMusic.volume = volume;
			backgroundMusic.loop = loop;
			StartCoroutine(DelayToPlay(backgroundMusic));
		}

		private IEnumerator DelayToPlay(AudioSource backgroundMusic)
		{
			while (backgroundMusic.clip.loadState != AudioDataLoadState.Loaded)
			{
				yield return null;
			}
			if (_enabledMusic)
			{
				backgroundMusic.Play();
			}
		}

		public void PauseBackgroundMusic()
		{
			StartCoroutine(PauseBM());
		}

		private IEnumerator PauseBM()
		{
			yield return 0;
			if (!(backgroundMusic == null))
			{
				if (backgroundMusic.isPlaying)
				{
					backgroundMusic.Pause();
				}
				p_pausedBackground = true;
			}
		}

		public void ResumeBackgroundMusic()
		{
			StartCoroutine(ResumeBM());
		}

		private IEnumerator ResumeBM()
		{
			yield return 0;
			if (_enabledMusic && !(backgroundMusic == null))
			{
				if (!backgroundMusic.isPlaying)
				{
					backgroundMusic.Play();
				}
				p_pausedBackground = false;
			}
		}

		public void StopBackgroundMusic()
		{
			StartCoroutine(StopBM());
		}

		private IEnumerator StopBM()
		{
			yield return 0;
			if (!(backgroundMusic == null) && !(backgroundMusic.clip == null))
			{
				backgroundMusic.Stop();
				p_pausedBackground = false;
			}
		}

		public bool IsSoundPlaying(string id)
		{
			if (id == null || id.Equals(string.Empty))
			{
				return false;
			}
			List<AudioSource> list = new List<AudioSource>(allClips);
			foreach (AudioSource item in list)
			{
				if (item.clip.name == id)
				{
					return true;
				}
			}
			return false;
		}
	}
}
