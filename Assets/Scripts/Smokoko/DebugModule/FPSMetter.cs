using System.Collections;
using UnityEngine;

namespace Smokoko.DebugModule
{
	public class FPSMetter : MonoBehaviour
	{
		private static FPSMetter _instance;

		private int FramesPerSec;

		private int FramesPerLev;

		private IEnumerator fpsEnumerator;

		public static FPSMetter instance
		{
			get
			{
				if (_instance == null)
				{
					GameObject gameObject = new GameObject("_fps metter instance");
					_instance = gameObject.AddComponent<FPSMetter>();
					Object.DontDestroyOnLoad(gameObject);
				}
				return _instance;
			}
		}

		public void Show(bool active)
		{
			_instance.gameObject.SetActive(active);
			if (active)
			{
				FPS();
			}
		}

		public void Destroy()
		{
			UnityEngine.Object.Destroy(_instance.gameObject);
		}

		private void OnGUI()
		{
			if (GUI.Button(new Rect(0f, 30f, 100f, 100f), $"{FramesPerSec} / {FramesPerLev}"))
			{
				FPS();
			}
		}

		private void FPS()
		{
			if (fpsEnumerator != null)
			{
				StopCoroutine(fpsEnumerator);
			}
			fpsEnumerator = FPSDisplay();
			FramesPerSec = (FramesPerLev = 0);
			StartCoroutine(fpsEnumerator);
		}

		private IEnumerator FPSDisplay()
		{
			float frequency = 0.5f;
			float dt = 0f;
			int count = 0;
			int sum = 0;
			while (true)
			{
				int lastFrameCount = Time.frameCount;
				float lastTime = Time.realtimeSinceStartup;
				while (dt < frequency)
				{
					dt += Time.deltaTime;
					yield return null;
				}
				float timeSpan = Time.realtimeSinceStartup - lastTime;
				int frameCount = Time.frameCount - lastFrameCount;
				FramesPerSec = Mathf.RoundToInt((float)frameCount / timeSpan);
				sum += FramesPerSec;
				count++;
				FramesPerLev = sum / count;
				dt = 0f;
			}
		}
	}
}
