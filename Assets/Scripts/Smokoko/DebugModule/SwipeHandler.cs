using System.Collections.Generic;
using UnityEngine;

namespace Smokoko.DebugModule
{
	public class SwipeHandler : MonoBehaviour
	{
		public delegate void myDelegate();

		private int minTouchCount = 3;

		private List<KeyValuePair<int, Vector2>> allTouches = new List<KeyValuePair<int, Vector2>>();

		public event myDelegate onShow;

		public event myDelegate onHide;

		public void SetTouchCount(int touchCount)
		{
			minTouchCount = touchCount;
		}

		private void Update()
		{
			Touch[] touches = Input.touches;
			for (int i = 0; i < touches.Length; i++)
			{
				Touch t = touches[i];
				if (t.phase == TouchPhase.Began)
				{
					allTouches.Add(new KeyValuePair<int, Vector2>(t.fingerId, t.position));
				}
				else if (t.phase == TouchPhase.Canceled || t.phase == TouchPhase.Ended)
				{
					allTouches.RemoveAll((KeyValuePair<int, Vector2> key) => key.Key == t.fingerId);
				}
			}
			if (allTouches.Count < minTouchCount)
			{
				return;
			}
			if (checkSwipe(down: true))
			{
				if (this.onShow != null)
				{
					this.onShow();
				}
			}
			else if (checkSwipe(down: false) && this.onHide != null)
			{
				this.onHide();
			}
		}

		private bool checkSwipe(bool down)
		{
			if (UnityEngine.Input.touchCount != allTouches.Count)
			{
				return false;
			}
			Touch[] touches = Input.touches;
			for (int i = 0; i < touches.Length; i++)
			{
				Touch touch = touches[i];
				KeyValuePair<int, Vector2> keyValuePair = allTouches.Find((KeyValuePair<int, Vector2> keypair) => keypair.Key == touch.fingerId);
				if (down)
				{
					Vector2 value = keyValuePair.Value;
					float y = value.y;
					Vector2 position = touch.position;
					if (y - position.y < (float)Screen.height * 0.3f)
					{
						return false;
					}
				}
				else
				{
					Vector2 position2 = touch.position;
					float y2 = position2.y;
					Vector2 value2 = keyValuePair.Value;
					if (y2 - value2.y < (float)Screen.height * 0.3f)
					{
						return false;
					}
				}
			}
			return true;
		}
	}
}
