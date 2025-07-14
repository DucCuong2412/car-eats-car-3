using UnityEngine;

[AddComponentMenu("2D Toolkit/UI/tk2dUIDragItem")]
public class tk2dUIDragItem : tk2dUIBaseItemControl
{
	public tk2dUIManager uiManager;

	private Vector3 offset = Vector3.zero;

	private bool isBtnActive;

	private void OnEnable()
	{
		if ((bool)uiItem)
		{
			uiItem.OnDown += ButtonDown;
			uiItem.OnRelease += ButtonRelease;
		}
	}

	private void OnDisable()
	{
		if ((bool)uiItem)
		{
			uiItem.OnDown -= ButtonDown;
			uiItem.OnRelease -= ButtonRelease;
		}
		if (isBtnActive)
		{
			if (tk2dUIManager.Instance__NoCreate != null)
			{
				tk2dUIManager.Instance.OnInputUpdate -= UpdateBtnPosition;
			}
			isBtnActive = false;
		}
	}

	private void UpdateBtnPosition()
	{
		base.transform.position = CalculateNewPos();
	}

	private Vector3 CalculateNewPos()
	{
		Vector2 position = uiItem.Touch.position;
		Camera uICameraForControl = tk2dUIManager.Instance.GetUICameraForControl(base.gameObject);
		Camera camera = uICameraForControl;
		float x = position.x;
		float y = position.y;
		Vector3 position2 = base.transform.position;
		float z = position2.z;
		Vector3 position3 = uICameraForControl.transform.position;
		Vector3 vector = camera.ScreenToWorldPoint(new Vector3(x, y, z - position3.z));
		Vector3 position4 = base.transform.position;
		vector.z = position4.z;
		vector += offset;
		return vector;
	}

	public void ButtonDown()
	{
		if (!isBtnActive)
		{
			tk2dUIManager.Instance.OnInputUpdate += UpdateBtnPosition;
		}
		isBtnActive = true;
		offset = Vector3.zero;
		Vector3 b = CalculateNewPos();
		offset = base.transform.position - b;
	}

	public void ButtonRelease()
	{
		if (isBtnActive)
		{
			tk2dUIManager.Instance.OnInputUpdate -= UpdateBtnPosition;
		}
		isBtnActive = false;
	}
}
