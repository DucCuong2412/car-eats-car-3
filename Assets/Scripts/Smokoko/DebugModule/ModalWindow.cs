using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

namespace Smokoko.DebugModule
{
	public class ModalWindow : MonoBehaviour
	{
		public enum DialogType
		{
			DialogBox,
			InfoPopUp
		}

		private class DialogObject
		{
			public DialogType Type;

			public GameObject Object;

			public Dictionary<int, Button> ButtonList = new Dictionary<int, Button>();

			public List<UnityAction> BackgroundClickActionList = new List<UnityAction>();
		}

		private GameObject ButtonPrefab;

		private GameObject QuestionDialogPrefab;

		private GameObject panel;

		private int resID;

		private InputHandler inputHandler;

		private Dictionary<int, DialogObject> dialogMap = new Dictionary<int, DialogObject>();

		[SerializeField]
		private List<int> queueWindows = new List<int>();

		private int activeWindow = -1;

		private static ModalWindow _instance;

		public static ModalWindow instance
		{
			get
			{
				if (!_instance)
				{
					GameObject gameObject = new GameObject("Canvas Debug");
					Canvas canvas = gameObject.AddComponent<Canvas>();
					canvas.renderMode = RenderMode.ScreenSpaceOverlay;
					canvas.sortingOrder = 1000;
					canvas.overrideSorting = true;
					CanvasScaler canvasScaler = gameObject.AddComponent<CanvasScaler>();
					canvasScaler.uiScaleMode = CanvasScaler.ScaleMode.ScaleWithScreenSize;
					canvasScaler.matchWidthOrHeight = 1f;
					gameObject.AddComponent<GraphicRaycaster>();
					_instance = gameObject.AddComponent<ModalWindow>();
					_instance.ButtonPrefab = (GameObject)Resources.Load("DialogBoxButtonPrefab");
					_instance.QuestionDialogPrefab = (GameObject)Resources.Load("DialogBoxPrefab");
					_instance.panel = new GameObject("Panel");
					_instance.panel.transform.SetParent(gameObject.transform);
					_instance.panel.SetActive(value: false);
					Image image = _instance.panel.AddComponent<Image>();
					RectTransform rectTransform = image.rectTransform;
					Vector2 zero = Vector2.zero;
					image.rectTransform.anchorMin = zero;
					rectTransform.anchoredPosition = zero;
					image.rectTransform.anchorMax = Vector2.one;
					image.color = new Color(0f, 0f, 0f, 0.5f);
					_instance.inputHandler = _instance.panel.AddComponent<InputHandler>();
					if (!_instance)
					{
						UnityEngine.Debug.LogError("The instance of canvas is null.");
					}
				}
				return _instance;
			}
		}

		[Obsolete]
		public void SetBackgroundClick(int resID, params UnityAction[] onBackgroundClick)
		{
			DialogObject dialogObject = dialogMap[resID];
			dialogObject.BackgroundClickActionList.Clear();
			if (onBackgroundClick != null)
			{
				for (int i = 0; i < onBackgroundClick.Length; i++)
				{
					dialogObject.BackgroundClickActionList.Add(onBackgroundClick[i]);
				}
			}
			dialogMap[resID] = dialogObject;
		}

		public int CreateDialogBox(string question, params ButtonInfo[] buttons)
		{
			resID++;
			GameObject gameObject = UnityEngine.Object.Instantiate(QuestionDialogPrefab);
			Text component = gameObject.transform.Find("QuestionText").gameObject.GetComponent<Text>();
			component.text = question;
			gameObject.name = "QuestionDialog";
			gameObject.transform.SetParent(panel.transform, worldPositionStays: false);
			gameObject.transform.localScale = new Vector3(1f, 1f, 1f);
			gameObject.SetActive(value: false);
			GameObject gameObject2 = gameObject.transform.Find("ButtonPanel").gameObject;
			DialogObject dialogObject = new DialogObject();
			dialogObject.Type = DialogType.DialogBox;
			int idBuff = resID;
			dialogObject.BackgroundClickActionList.Add(delegate
			{
				Show(idBuff, flag: false);
			});
			if (buttons != null)
			{
				for (int i = 0; i < buttons.Length; i++)
				{
					GameObject gameObject3 = UnityEngine.Object.Instantiate(ButtonPrefab);
					Button component2 = gameObject3.GetComponent<Button>();
					Text component3 = component2.transform.Find("Text").gameObject.GetComponent<Text>();
					component3.text = buttons[i].Name;
					component2.onClick.AddListener(buttons[i].OnClickEvent);
					component2.onClick.AddListener(delegate
					{
						Show(idBuff, flag: false);
					});
					component2.transform.SetParent(gameObject2.transform);
					component2.transform.localScale = new Vector3(1f, 1f, 1f);
					dialogObject.ButtonList[buttons[i].ID] = component2;
				}
			}
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

		[Obsolete]
		public void MakePartOfDialog(int dialogID, GameObject newbieGO)
		{
			GameObject @object = dialogMap[dialogID].Object;
			Vector3 localScale = newbieGO.transform.localScale;
			newbieGO.transform.SetParent(@object.transform);
			newbieGO.transform.localScale = localScale;
		}

		public void SetDialogLinesCount(int dialogID, int count)
		{
			GameObject @object = dialogMap[dialogID].Object;
			if (@object == null)
			{
				UnityEngine.Debug.Log("can't find dialog with id:" + dialogID);
				return;
			}
			UnityEngine.Debug.Log("find dialog with id:" + dialogID);
			GridLayoutGroup componentInChildren = @object.GetComponentInChildren<GridLayoutGroup>();
			if (componentInChildren != null)
			{
				componentInChildren.constraintCount = Mathf.Clamp(count, 1, 9);
			}
		}

		public int Show(string question, params ButtonInfo[] buttons)
		{
			int num = CreateDialogBox(question, buttons);
			Show(num);
			return num;
		}

		public bool HasWindow(int key)
		{
			return dialogMap.ContainsKey(key);
		}

		public void Show(int dialogID, bool flag = true)
		{
			if (flag)
			{
				if (panel.activeSelf)
				{
					queueWindows.Add(dialogID);
				}
				else
				{
					ForceShow(dialogID, flag);
				}
			}
			else if (dialogID == activeWindow)
			{
				ForceShow(dialogID, flag);
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

		private void UpdateInputHandler(int resID)
		{
			DialogObject dialog = dialogMap[resID];
			inputHandler.SetPointerUpAction(delegate
			{
				for (int i = 0; i < dialog.BackgroundClickActionList.Count; i++)
				{
					dialog.BackgroundClickActionList[i]();
				}
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

		[Obsolete]
		public void EnableButtons(int dialogID, bool flag, params int[] buttons)
		{
			List<int> list = new List<int>();
			for (int i = 0; i < buttons.Length; i++)
			{
				list.Add(buttons[i]);
			}
			EnableButtons(dialogID, flag, list);
		}

		[Obsolete]
		public void EnableButtons(int dialogID, bool flag, List<int> buttons)
		{
			DialogObject dialogObject = dialogMap[dialogID];
			for (int i = 0; i < buttons.Count; i++)
			{
				dialogObject.ButtonList[buttons[i]].interactable = flag;
			}
		}

		private void EnableDialogBoxButtons(GameObject QuestionDialog, bool flag, List<string> buttonList)
		{
			Transform transform = QuestionDialog.transform.Find("ButtonPanel");
			IEnumerator enumerator = transform.GetEnumerator();
			try
			{
				while (enumerator.MoveNext())
				{
					Transform transform2 = (Transform)enumerator.Current;
					Button component = transform2.gameObject.GetComponent<Button>();
					if (buttonList.Contains(component.name))
					{
						component.interactable = flag;
					}
				}
			}
			finally
			{
				IDisposable disposable;
				if ((disposable = (enumerator as IDisposable)) != null)
				{
					disposable.Dispose();
				}
			}
		}
	}
}
