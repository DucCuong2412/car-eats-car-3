using System.Collections;
using UnityEngine;

public class CellContainerVisibility : MonoBehaviour
{
	private Camera camera;

	private GameObject go;

	private RectTransform rectTransform;

	private Rect rect;

	private bool goIsActiveSelf = true;

	private Vector2 pos;

	private bool contains;

	private CellContainer cell;

	private CellContainer.State state;

	public static CellContainerVisibility Create(GameObject toAdd, CellContainer cell)
	{
		CellContainerVisibility cellContainerVisibility = toAdd.AddComponent<CellContainerVisibility>();
		cellContainerVisibility.cell = cell;
		return cellContainerVisibility;
	}

	private void Awake()
	{
		go = base.transform.GetChild(0).gameObject;
		rectTransform = go.GetComponent<RectTransform>();
		camera = GetComponentInParent<Canvas>().worldCamera;
		ResizeRect();
	}

	private IEnumerator Start()
	{
		int waitFrames = 2;
		base.enabled = false;
		while (true)
		{
			int num;
			waitFrames = (num = waitFrames) - 1;
			if (num <= 0)
			{
				break;
			}
			yield return null;
		}
		while (true)
		{
			Check();
			yield return null;
		}
	}

	private void ResizeRect()
	{
		rect = camera.pixelRect;
		float num = rect.width * 0.1f;
		float num2 = rect.height * 0.1f;
		rect.position -= new Vector2(num, num2);
		rect.width += num * 2f;
		rect.height += num2 * 2f;
	}

	private void Check()
	{
		pos = camera.WorldToScreenPoint(rectTransform.position);
		contains = rect.Contains(pos);
		if (contains)
		{
			if (!goIsActiveSelf)
			{
				goIsActiveSelf = true;
				go.SetActive(value: true);
				cell.state = state;
			}
		}
		else if (goIsActiveSelf)
		{
			state = cell.state;
			goIsActiveSelf = false;
			go.SetActive(value: false);
		}
	}
}
