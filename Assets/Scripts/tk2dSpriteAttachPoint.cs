using System.Collections.Generic;
using UnityEngine;

[ExecuteInEditMode]
[AddComponentMenu("2D Toolkit/Sprite/tk2dSpriteAttachPoint")]
public class tk2dSpriteAttachPoint : MonoBehaviour
{
	private tk2dBaseSprite sprite;

	public List<Transform> attachPoints = new List<Transform>();

	private static bool[] attachPointUpdated = new bool[32];

	public bool deactivateUnusedAttachPoints;

	private void Awake()
	{
		if (sprite == null)
		{
			sprite = GetComponent<tk2dBaseSprite>();
			if (sprite != null)
			{
				HandleSpriteChanged(sprite);
			}
		}
	}

	private void OnEnable()
	{
		if (sprite != null)
		{
			sprite.SpriteChanged += HandleSpriteChanged;
		}
	}

	private void OnDisable()
	{
		if (sprite != null)
		{
			sprite.SpriteChanged -= HandleSpriteChanged;
		}
	}

	private void UpdateAttachPointTransform(tk2dSpriteDefinition.AttachPoint attachPoint, Transform t)
	{
		t.localPosition = Vector3.Scale(attachPoint.position, sprite.scale);
		t.localScale = sprite.scale;
		Vector3 scale = sprite.scale;
		float num = Mathf.Sign(scale.x);
		Vector3 scale2 = sprite.scale;
		float num2 = num * Mathf.Sign(scale2.y);
		t.localEulerAngles = new Vector3(0f, 0f, attachPoint.angle * num2);
	}

	private void HandleSpriteChanged(tk2dBaseSprite spr)
	{
		tk2dSpriteDefinition currentSprite = spr.CurrentSprite;
		int num = Mathf.Max(currentSprite.attachPoints.Length, attachPoints.Count);
		if (num > attachPointUpdated.Length)
		{
			attachPointUpdated = new bool[num];
		}
		tk2dSpriteDefinition.AttachPoint[] array = currentSprite.attachPoints;
		foreach (tk2dSpriteDefinition.AttachPoint attachPoint in array)
		{
			bool flag = false;
			int num2 = 0;
			foreach (Transform attachPoint2 in attachPoints)
			{
				if (attachPoint2 != null && attachPoint2.name == attachPoint.name)
				{
					attachPointUpdated[num2] = true;
					UpdateAttachPointTransform(attachPoint, attachPoint2);
					flag = true;
				}
				num2++;
			}
			if (!flag)
			{
				GameObject gameObject = new GameObject(attachPoint.name);
				Transform transform = gameObject.transform;
				transform.parent = base.transform;
				UpdateAttachPointTransform(attachPoint, transform);
				attachPointUpdated[attachPoints.Count] = true;
				attachPoints.Add(transform);
			}
		}
		if (!deactivateUnusedAttachPoints)
		{
			return;
		}
		for (int j = 0; j < attachPoints.Count; j++)
		{
			if (attachPoints[j] != null)
			{
				GameObject gameObject2 = attachPoints[j].gameObject;
				if (attachPointUpdated[j] && !gameObject2.activeSelf)
				{
					gameObject2.SetActive(value: true);
				}
				else if (!attachPointUpdated[j] && gameObject2.activeSelf)
				{
					gameObject2.SetActive(value: false);
				}
			}
			attachPointUpdated[j] = false;
		}
	}
}
