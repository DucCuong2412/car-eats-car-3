using AnimationOrTween;
using System.Collections;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class RateUsWindow : MonoBehaviour
{
	[SerializeField]
	private GameObject _root;

	[SerializeField]
	private GameObject _rootRate;

	[SerializeField]
	private Button _buttonClose;

	[SerializeField]
	private Button _buttonRate;

	[SerializeField]
	private Toggle[] stars;

	public static UnityAction onYesAction;

	public static UnityAction onNoAction;

	public GameObject noObject;

	public GameObject yesObject;

	public Transform leftMarker;

	public Transform rightMarker;

	public Transform topMarker;

	public Transform bottomMarker;

	[HideInInspector]
	public float ForPercent = 0.1f;

	public Animation _animationPanel;

	public static bool IsOpened;

	private bool _isRateActive;

	private int countOfStars;

	private void OnEnable()
	{
		Show(null, null);
		Audio.Play("gui_screen_on");
	}

	public void Init()
	{
		if (Progress.shop.ForRateUs)
		{
			return;
		}
		if (!GameObject.Find("RateUs"))
		{
			SceneManager.LoadScene("RateUs", LoadSceneMode.Additive);
		}
		_isRateActive = false;
		AddListeners();
		if (_root != null && _rootRate != null && _buttonRate != null)
		{
			_root.SetActive(value: true);
			_rootRate.SetActive(value: true);
			_buttonRate.gameObject.SetActive(value: false);
			for (int i = 0; i < stars.Length; i++)
			{
				stars[i].isOn = false;
			}
		}
		if (_animationPanel != null)
		{
			_animationPanel.Play("rateUs_show");
			ActiveAnimation.Play(_animationPanel, "rateUs_show", Direction.Forward);
		}
	}

	private void Update()
	{
		if (UnityEngine.Input.GetKeyDown(KeyCode.Escape))
		{
			Close();
		}
		else
		{
			if (!Input.GetMouseButton(0) && Input.touches.Length == 0)
			{
				return;
			}
			Vector3 mousePosition = UnityEngine.Input.mousePosition;
			float x = mousePosition.x;
			Vector3 position = leftMarker.position;
			if (!(x > position.x))
			{
				return;
			}
			Vector3 mousePosition2 = UnityEngine.Input.mousePosition;
			float x2 = mousePosition2.x;
			Vector3 position2 = rightMarker.position;
			if (!(x2 < position2.x))
			{
				return;
			}
			Vector3 mousePosition3 = UnityEngine.Input.mousePosition;
			float y = mousePosition3.y;
			Vector3 position3 = bottomMarker.position;
			if (!(y > position3.y))
			{
				return;
			}
			Vector3 mousePosition4 = UnityEngine.Input.mousePosition;
			float y2 = mousePosition4.y;
			Vector3 position4 = topMarker.position;
			if (y2 < position4.y)
			{
				if (!_isRateActive)
				{
					_buttonRate.gameObject.SetActive(value: true);
					_isRateActive = true;
				}
				Vector3 position5 = rightMarker.position;
				float x3 = position5.x;
				Vector3 position6 = leftMarker.position;
				float num = x3 - position6.x;
				Vector3 mousePosition5 = UnityEngine.Input.mousePosition;
				float x4 = mousePosition5.x;
				float num2 = x4;
				Vector3 position7 = leftMarker.position;
				float num3 = (num2 - position7.x) / num;
				StartCoroutine(SetStars(countOfStars = (int)(num3 / 0.2f)));
			}
		}
	}

	private IEnumerator SetStars(int index)
	{
		for (int i = 0; i <= index; i++)
		{
			stars[i].isOn = true;
		}
		for (int j = index + 1; j < stars.Length; j++)
		{
			stars[j].isOn = false;
		}
		yield return new WaitForSeconds(ForPercent);
		stars[index].isOn = true;
	}

	private void AddListeners()
	{
		if ((bool)_buttonClose)
		{
			_buttonClose.onClick.AddListener(OnButtonCloseClick);
		}
		if ((bool)_buttonRate)
		{
			_buttonRate.onClick.AddListener(OnButtonRateClick);
		}
	}

	private void RemoveListeners()
	{
		if ((bool)_buttonClose)
		{
			_buttonClose.onClick.RemoveAllListeners();
		}
		if ((bool)_buttonRate)
		{
			_buttonRate.onClick.RemoveAllListeners();
		}
	}

	private void OnButtonRateClick()
	{
		if (onYesAction != null)
		{
			onYesAction();
		}
		if (countOfStars >= 3)
		{
			OpenStore();
		}
		else
		{
			SendEmail();
		}
		Close();
	}

	private void OnButtonCloseClick()
	{
		if (onNoAction != null)
		{
			onNoAction();
		}
		Open();
	}

	private void OnButtonFeedbackClick()
	{
		SendEmail();
	}

	public void SendEmail()
	{
		SendMail.EmailUs("Car Eats Car 2/Suggestion", string.Empty);
	}

	public void OpenStore()
	{
		string url_android = RateUsLinks.Instance.url_android;
		Application.OpenURL(url_android);
	}

	public void Open()
	{
		_buttonClose.gameObject.SetActive(value: false);
		yesObject.SetActive(value: false);
		noObject.SetActive(value: true);
	}

	public void closeNo()
	{
		Progress.shop.ForRateUs = true;
		Close();
	}

	public void Close()
	{
		ActiveAnimation activeAnimation = ActiveAnimation.Play(_animationPanel, "rateUs_hide", Direction.Forward);
		activeAnimation.onFinished.Add(new EventDelegate(delegate
		{
			IsOpened = false;
			_root.SetActive(value: false);
			_rootRate.SetActive(value: false);
			onYesAction = null;
			onNoAction = null;
			RemoveListeners();
			Progress.shop.needRateUs2 = false;
		}));
	}

	public static void Show(UnityAction onYes, UnityAction onNo)
	{
		IsOpened = true;
		ShowCoroutine(onYes, onNo);
	}

	private static void ShowCoroutine(UnityAction onYes, UnityAction onNo)
	{
		RateUsWindow rateUsWindow = UnityEngine.Object.FindObjectOfType<RateUsWindow>();
		rateUsWindow.Init();
		onYesAction = onYes;
		onNoAction = onNo;
	}
}
