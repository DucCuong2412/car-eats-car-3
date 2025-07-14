using System;
using System.Collections;
using UnityEngine;

public class LevelIconCheck : MonoBehaviour
{
	public UISprite spriteLamp;

	public UISprite spriteLight;

	public GameObject chainObject;

	public UISprite chainSprite;

	public UILabel levelLabel;

	[SerializeField]
	private byte _number;

	[SerializeField]
	private byte _pack;

	private LevelGalleryView _galleryView;

	private LevelGalleryModel _galleryModel;

	private GameObject ticketsObject;

	private IEnumerator hidingTickets;

	private bool rotLeft;

	private float rotAngle = 5f;

	private IEnumerator rotation;

	public byte Number => _number;

	public byte Pack => _pack;

	public LevelGalleryView galleryView
	{
		get
		{
			if (_galleryView == null)
			{
				_galleryView = (LevelGalleryView)UnityEngine.Object.FindObjectOfType(typeof(LevelGalleryView));
			}
			return _galleryView;
		}
	}

	public LevelGalleryModel galleryModel
	{
		get
		{
			if (_galleryModel == null)
			{
				_galleryModel = (LevelGalleryModel)UnityEngine.Object.FindObjectOfType(typeof(LevelGalleryModel));
			}
			return _galleryModel;
		}
	}

	private void Awake()
	{
		rotation = Rotation();
		_pack = byte.Parse(base.transform.parent.gameObject.name);
		chainSprite = chainObject.GetComponent<UISprite>();
	}

	public void SetState(bool isAvailable, bool animated, int ticketsFound)
	{
		if (isAvailable)
		{
			OpenRoad(animated, ticketsFound);
		}
		else
		{
			spriteLight.color = new Color(1f, 1f, 1f, 0.01f);
		}
	}

	private void OpenRoad(bool animate, int ticketsFound)
	{
		if (ticketsFound > 0)
		{
			ticketsObject = UnityEngine.Object.Instantiate(galleryView.tickets[ticketsFound - 1]);
			ticketsObject.transform.parent = chainObject.transform.parent;
			ticketsObject.transform.localScale = Vector3.one;
			ticketsObject.transform.localPosition = Vector3.up * 40f;
			AnimatedAlpha[] componentsInChildren = ticketsObject.GetComponentsInChildren<AnimatedAlpha>();
			AnimatedAlpha[] array = componentsInChildren;
			foreach (AnimatedAlpha animatedAlpha in array)
			{
				animatedAlpha.alpha = ((!animate) ? 1f : 0.01f);
			}
			ticketsObject.SetActive(value: true);
		}
		if (animate)
		{
			StartCoroutine(alfaHide(ticketsFound > 0));
		}
		else
		{
			chainObject.GetComponent<UISprite>().color = new Color(1f, 1f, 1f, 0f);
		}
	}

	public void Tickets(bool hide)
	{
		if (!(ticketsObject == null))
		{
			if (hidingTickets != null)
			{
				StopCoroutine(hidingTickets);
			}
			hidingTickets = HideTickets(hide);
			StartCoroutine(hidingTickets);
		}
	}

	private IEnumerator HideTickets(bool hide)
	{
		float dt = 0f;
		AnimatedAlpha[] allAnims = ticketsObject.GetComponentsInChildren<AnimatedAlpha>();
		while (dt < 1f)
		{
			AnimatedAlpha[] array = allAnims;
			foreach (AnimatedAlpha animatedAlpha in array)
			{
				animatedAlpha.alpha += (float)((!hide) ? 1 : (-1)) * 0.07f;
			}
			dt += Time.fixedDeltaTime;
			yield return null;
		}
		hidingTickets = null;
	}

	private IEnumerator alfaHide(bool showTickets)
	{
		UISprite sprite = chainObject.GetComponent<UISprite>();
		while (true)
		{
			Color color = sprite.color;
			if (!(color.a > 0f))
			{
				break;
			}
			UISprite uISprite = sprite;
			Color color2 = sprite.color;
			uISprite.color = new Color(1f, 1f, 1f, color2.a - 0.1f);
			yield return new WaitForFixedUpdate();
		}
		if (showTickets)
		{
			if (hidingTickets != null)
			{
				StopCoroutine(hidingTickets);
			}
			hidingTickets = HideTickets(hide: false);
			yield return StartCoroutine(hidingTickets);
		}
	}

	private void OnClick()
	{
		if (!Progress.levels.Pack(Pack).Level(Number).isOpen)
		{
			Audio.PlayAsync("gui_denied");
			galleryModel.ShowBuyLevelsPanel();
		}
		else if (GameEnergyLogic.isEnoughForRace)
		{
			galleryModel.TeleportCar(Pack, Number, delegate
			{
				Progress.levels.active_pack = Pack;
				Progress.levels.active_level = Number;
				Progress.Save(Progress.SaveType.Levels);
				GameEnergyLogic.GetFuelForRace();
				galleryView.PlayFuelLabelAnim(delegate
				{
					Game.LoadLevel("Race");
				});
			});
		}
		else
		{
			galleryModel.ShowBuyCanvasWindow();
		}
	}

	private IEnumerator AnimateClosedChains(Action callback = null)
	{
		UISprite[] sprites = chainObject.GetComponentsInChildren<UISprite>();
		GameObject leftChain = null;
		GameObject rightChain = null;
		GameObject signStop = null;
		UISprite[] array = sprites;
		foreach (UISprite uISprite in array)
		{
			if (uISprite.gameObject.name == "Chain_Left")
			{
				leftChain = uISprite.gameObject;
			}
			if (uISprite.gameObject.name == "Chain_Right")
			{
				rightChain = uISprite.gameObject;
			}
			if (uISprite.gameObject.name == "Sign_Stop")
			{
				signStop = uISprite.gameObject;
			}
		}
		if (leftChain == null || rightChain == null || signStop == null)
		{
			UnityEngine.Debug.Log("LevelIcon " + Pack + " " + Number + " error chains");
			callback?.Invoke();
			yield break;
		}
		for (int i = 0; i < 1; i++)
		{
			float ang = 0f;
			float step = 0.25f;
			while (ang < 2f)
			{
				leftChain.transform.localEulerAngles -= Vector3.forward * step;
				rightChain.transform.localEulerAngles += Vector3.forward * step;
				signStop.transform.localPosition += Vector3.down * step * 0.75f;
				ang += 0.12f;
				yield return new WaitForFixedUpdate();
			}
			while (ang > 0f)
			{
				leftChain.transform.localEulerAngles += Vector3.forward * step;
				rightChain.transform.localEulerAngles -= Vector3.forward * step;
				signStop.transform.localPosition -= Vector3.down * step * 0.75f;
				ang -= 0.12f;
				yield return new WaitForFixedUpdate();
			}
		}
		if (Pack == galleryModel.activePack)
		{
			callback?.Invoke();
		}
	}

	private void DropChains(bool animated, Action callback = null)
	{
		if (animated)
		{
			StartCoroutine(AnimateDropChains(callback));
		}
		else
		{
			chainObject.SetActive(value: false);
		}
		spriteLight.color = new Color(1f, 1f, 1f, 0.01f);
	}

	private IEnumerator AnimateDropChains(Action callback = null)
	{
		UISprite[] sprites = chainObject.GetComponentsInChildren<UISprite>();
		GameObject leftChain = null;
		GameObject rightChain = null;
		GameObject signStop = null;
		GameObject leftLock = null;
		GameObject rightLock = null;
		UISprite[] array = sprites;
		foreach (UISprite uISprite in array)
		{
			if (uISprite.gameObject.name == "Chain_Left")
			{
				leftChain = uISprite.gameObject;
			}
			if (uISprite.gameObject.name == "Chain_Right")
			{
				rightChain = uISprite.gameObject;
			}
			if (uISprite.gameObject.name == "Sign_Stop")
			{
				signStop = uISprite.gameObject;
			}
			if (uISprite.gameObject.name == "Lock_Right")
			{
				rightLock = uISprite.gameObject;
			}
			if (uISprite.gameObject.name == "Lock_Left")
			{
				leftLock = uISprite.gameObject;
			}
		}
		if (leftChain == null || rightChain == null || signStop == null || rightLock == null || leftLock == null)
		{
			UnityEngine.Debug.Log("LevelIcon " + Pack + " " + Number + " error chains");
			callback?.Invoke();
			yield break;
		}
		float ang = 0f;
		float step = 0.15f;
		while (ang < 2f)
		{
			leftChain.transform.localEulerAngles -= Vector3.forward * step;
			rightChain.transform.localEulerAngles += Vector3.forward * step;
			signStop.transform.localPosition += Vector3.down * step * 0.75f;
			ang += 0.12f;
			yield return new WaitForFixedUpdate();
		}
		while (ang > 0f)
		{
			leftChain.transform.localEulerAngles += Vector3.forward * step;
			rightChain.transform.localEulerAngles -= Vector3.forward * step;
			signStop.transform.localPosition -= Vector3.down * step * 0.75f;
			ang -= 0.12f;
			yield return new WaitForFixedUpdate();
		}
		leftLock.SetActive(value: false);
		rightLock.SetActive(value: false);
		Rigidbody2D rb = signStop.gameObject.AddComponent<Rigidbody2D>();
		rb.gravityScale = 0.3f;
		rb.AddForce(1.8f * Vector2.up, ForceMode2D.Impulse);
		yield return new WaitForSeconds(1f);
		chainObject.SetActive(value: false);
	}

	public void StartRotation()
	{
		if (Pack <= 2 && rotation != null)
		{
			StartCoroutine(rotation);
		}
	}

	public void StopRotation()
	{
		if (rotation != null)
		{
			StopCoroutine(rotation);
			ResetRotation();
		}
	}

	private void ResetRotation()
	{
		if (spriteLamp != null)
		{
			spriteLamp.transform.eulerAngles = Vector3.zero;
		}
	}

	private IEnumerator Rotation()
	{
		while (true)
		{
			spriteLamp.transform.eulerAngles += Vector3.forward * 0.1f * (rotLeft ? 1 : (-1));
			Vector3 eulerAngles = spriteLamp.transform.eulerAngles;
			float angle = eulerAngles.z;
			if (angle < 180f)
			{
				rotLeft = ((!(angle > rotAngle)) ? rotLeft : (!rotLeft));
			}
			else
			{
				rotLeft = ((!(angle < 360f - rotAngle)) ? rotLeft : (!rotLeft));
			}
			yield return new WaitForFixedUpdate();
		}
	}
}
