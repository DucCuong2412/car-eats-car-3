using System.Collections;
using UnityEngine;

namespace Smokoko.DebugModule
{
	public class HUDFPS : MonoBehaviour
	{
		public Rect startRect = new Rect(10f, 40f, 75f, 50f);

		public bool updateColor = true;

		public bool allowDrag = true;

		public float frequency = 0.5f;

		public int nbDecimal = 1;

		private float accum;

		private int iFPStotal;

		private int frames;

		private Color color = Color.white;

		private string sFPS = string.Empty;

		private string sFPSavr = string.Empty;

		private GUIStyle style;

		private void Start()
		{
			StartCoroutine(FPS());
		}

		private void Update()
		{
			accum += Time.timeScale / Time.deltaTime;
			frames++;
		}

		private IEnumerator FPS()
		{
			int count = 0;
			while (true)
			{
				float fps = accum / (float)frames;
				iFPStotal += (int)fps;
				count++;
				sFPS = fps.ToString("f" + Mathf.Clamp(nbDecimal, 0, 10));
				sFPSavr = (iFPStotal / count).ToString("d");
				color = ((fps >= 50f) ? Color.green : ((!(fps > 30f)) ? Color.red : Color.yellow));
				accum = 0f;
				frames = 0;
				yield return new WaitForSeconds(frequency);
			}
		}

		private void OnGUI()
		{
			Rect clientRect = new Rect(startRect.x, startRect.y, (float)Screen.height / 5f, (float)Screen.height / 10f);
			GUI.color = ((!updateColor) ? Color.white : color);
			startRect = GUI.Window(0, clientRect, DoMyWindow, string.Empty);
		}

		private void DoMyWindow(int windowID)
		{
			GUIStyle label = GUI.skin.label;
			label.fixedHeight = (float)Screen.height / 10f;
			label.fontSize = (int)((float)Screen.height / 30f);
			label.alignment = TextAnchor.MiddleCenter;
			GUI.Label(new Rect(0f, 0f, startRect.width, startRect.height), sFPS + " FPS\n" + sFPSavr + " AVR", label);
			if (allowDrag)
			{
				GUI.DragWindow(new Rect(0f, 0f, Screen.width, Screen.height));
			}
		}
	}
}
