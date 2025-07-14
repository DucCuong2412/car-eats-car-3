using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace Smokoko.DebugModule
{
	public class PopUpWindow : MonoBehaviour
	{
		public enum DialogType
		{
			DialogBox,
			InfoPopUp
		}

		private class DialogObject
		{
			public DialogType Type;

			public float DuringTime;

			public GameObject Object;
		}

		private GameObject QuestionDialogPrefab;

		private GameObject panel;

		private int resID;

		private InputHandler inputHandler;

		private Dictionary<int, DialogObject> dialogMap = new Dictionary<int, DialogObject>();

		[SerializeField]
		private List<int> queueWindows = new List<int>();

		private int activeWindow = -1;

		private static PopUpWindow _instance;

		public static PopUpWindow instance
		{
			get
			{
				if (!_instance)
				{
					GameObject gameObject = new GameObject("Canvas Debug 2");
					Canvas canvas = gameObject.AddComponent<Canvas>();
					canvas.renderMode = RenderMode.ScreenSpaceOverlay;
					canvas.sortingOrder = 1100;
					canvas.overrideSorting = true;
					CanvasScaler canvasScaler = gameObject.AddComponent<CanvasScaler>();
					canvasScaler.uiScaleMode = CanvasScaler.ScaleMode.ScaleWithScreenSize;
					canvasScaler.matchWidthOrHeight = 1f;
					gameObject.AddComponent<GraphicRaycaster>();
					_instance = gameObject.AddComponent<PopUpWindow>();
					_instance.QuestionDialogPrefab = (GameObject)Resources.Load("PopUpPrefab");
					_instance.panel = new GameObject("Panel");
					_instance.panel.transform.SetParent(gameObject.transform);
					_instance.panel.SetActive(value: false);
					RectTransform rectTransform = _instance.panel.AddComponent<RectTransform>();
					Vector2 vector2 = rectTransform.anchoredPosition = (rectTransform.anchorMax = Vector2.one);
					vector2 = (rectTransform.anchorMin = (rectTransform.sizeDelta = Vector2.zero));
					_instance.inputHandler = _instance.panel.AddComponent<InputHandler>();
					if (!_instance)
					{
						UnityEngine.Debug.LogError("The instance of canvas is null.");
					}
				}
				return _instance;
			}
		}

		public int CreateDialogBox(string question, float during)
		{
			resID++;
			GameObject gameObject = UnityEngine.Object.Instantiate(QuestionDialogPrefab);
			Text component = gameObject.transform.Find("QuestionText").gameObject.GetComponent<Text>();
			component.text = question;
			gameObject.name = "QuestionDialog";
			gameObject.transform.SetParent(panel.transform, worldPositionStays: false);
			gameObject.transform.localScale = new Vector3(1f, 1f, 1f);
			gameObject.SetActive(value: false);
			DialogObject dialogObject = new DialogObject();
			dialogObject.Type = DialogType.DialogBox;
			dialogObject.DuringTime = during;
			dialogObject.Object = gameObject;
			dialogMap.Add(resID, dialogObject);
			return resID;
		}

		[Obsolete]
		public bool DestroyDialog(int dialogID)
		{
			if (!dialogMap.ContainsKey(dialogID))
			{
				UnityEngine.Debug.LogError("Can't find dialog ID: " + dialogID);
				return false;
			}
			GameObject @object = dialogMap[dialogID].Object;
			dialogMap.Remove(dialogID);
			UnityEngine.Object.Destroy(@object);
			return true;
		}

		public void DestroyAll()
		{
			foreach (KeyValuePair<int, DialogObject> item in dialogMap)
			{
				UnityEngine.Object.Destroy(item.Value.Object);
			}
			dialogMap.Clear();
			resID = 0;
		}

		public int Show(string question, float delay = 1.5f)
		{
			int num = CreateDialogBox(question, delay);
			Show(num);
			return num;
		}

		private void Show(int dialogID, bool isShow = true)
		{
			if (isShow)
			{
				if (panel.activeSelf)
				{
					queueWindows.Add(dialogID);
					return;
				}
				StartCoroutine(hideGOAfter(dialogID));
				ForceShow(dialogID, isShow);
			}
			else if (dialogID == activeWindow)
			{
				ForceShow(dialogID, isShow);
				queueWindows.Remove(dialogID);
				ExecuteLastInQueue(dialogID);
			}
			else
			{
				queueWindows.Remove(dialogID);
			}
		}

		private void ForceShow(int dialogID, bool flag = true)
		{
			activeWindow = ((!flag) ? (-1) : dialogID);
			UpdateInputHandler(dialogID);
			panel.SetActive(flag);
			HideAllDialogs();
			GameObject @object = dialogMap[dialogID].Object;
			if (@object == null)
			{
				UnityEngine.Debug.LogError("Can't find dialog with id: " + dialogID);
			}
			else
			{
				@object.SetActive(flag);
			}
		}

		private void ExecuteLastInQueue(int dialogID, bool flag = true)
		{
			if (queueWindows.Count > 0)
			{
				Show(queueWindows[0]);
			}
		}

		private IEnumerator hideGOAfter(int _dialogID)
		{
			float sec = dialogMap[_dialogID].DuringTime;
			yield return new WaitForSeconds(sec);
			Show(_dialogID, isShow: false);
		}

		private void UpdateInputHandler(int resID)
		{
			DialogObject dialogObject = dialogMap[resID];
			inputHandler.SetPointerUpAction(delegate
			{
			});
		}

		private void HideAllDialogs()
		{
			foreach (KeyValuePair<int, DialogObject> item in dialogMap)
			{
				GameObject @object = item.Value.Object;
				if (@object.activeSelf)
				{
					@object.SetActive(value: false);
				}
			}
		}
	}
}
