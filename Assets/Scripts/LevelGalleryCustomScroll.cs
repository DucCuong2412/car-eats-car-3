using System.Collections.Generic;
using UnityEngine;

public class LevelGalleryCustomScroll : MonoBehaviour
{
	public LevelGalleryModel model;

	public List<GameObject> igroreTouchIfActive = new List<GameObject>();

	private ShopModel pShopModel;

	private bool isTouching;

	private Vector3 startTouchPosition = Vector3.zero;

	private ShopModel shopModel
	{
		get
		{
			if (pShopModel == null)
			{
				pShopModel = UnityEngine.Object.FindObjectOfType<ShopModel>();
			}
			return pShopModel;
		}
	}

	private void Update()
	{
		if (Application.platform == RuntimePlatform.WindowsEditor || Application.platform == RuntimePlatform.OSXEditor)
		{
			checkMouse();
		}
		else
		{
			checkTouch();
		}
	}

	private bool canTouch()
	{
		if (model.isTeleporting)
		{
			return false;
		}
		for (int i = 0; i < igroreTouchIfActive.Count; i++)
		{
			if (igroreTouchIfActive[i].activeSelf)
			{
				return false;
			}
		}
		if (shopModel != null && shopModel.isShopOpen)
		{
			return false;
		}
		if (GameObject.Find("ShopCanvasWindows") != null)
		{
			return false;
		}
		return true;
	}

	private void checkMouse()
	{
		if (Input.GetMouseButtonUp(0))
		{
			isTouching = false;
		}
		if (Input.GetMouseButtonDown(0))
		{
			isTouching = true;
			startTouchPosition = UnityEngine.Input.mousePosition;
		}
		else if (isTouching)
		{
			float x = startTouchPosition.x;
			Vector3 mousePosition = UnityEngine.Input.mousePosition;
			float f = x - mousePosition.x;
			if (Mathf.Abs(f) > (float)Screen.width / 20f && canTouch())
			{
				moveScroll(Mathf.Sign(f));
			}
		}
	}

	private void checkTouch()
	{
		if (UnityEngine.Input.touchCount != 1)
		{
			return;
		}
		if (UnityEngine.Input.GetTouch(0).phase == TouchPhase.Ended || UnityEngine.Input.GetTouch(0).phase == TouchPhase.Canceled)
		{
			isTouching = false;
		}
		if (UnityEngine.Input.GetTouch(0).phase == TouchPhase.Began)
		{
			isTouching = true;
			startTouchPosition = UnityEngine.Input.GetTouch(0).position;
		}
		else if (isTouching)
		{
			float x = startTouchPosition.x;
			Vector2 position = UnityEngine.Input.GetTouch(0).position;
			float f = x - position.x;
			if (Mathf.Abs(f) > (float)Screen.width / 20f && canTouch())
			{
				moveScroll(Mathf.Sign(f));
			}
		}
	}

	private void moveScroll(float right)
	{
		isTouching = false;
		if (right > 0f && model.activePack < 4)
		{
			model.ToRight();
		}
		else if (right < 0f && model.activePack > 1)
		{
			model.ToLeft();
		}
	}
}
