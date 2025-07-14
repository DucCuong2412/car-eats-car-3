using System;
using UnityEngine;

[AddComponentMenu("2D Toolkit/UI/tk2dUIProgressBar")]
public class tk2dUIProgressBar : MonoBehaviour
{
	public Transform scalableBar;

	public tk2dClippedSprite clippedSpriteBar;

	public tk2dSlicedSprite slicedSpriteBar;

	private bool initializedSlicedSpriteDimensions;

	private Vector2 emptySlicedSpriteDimensions = Vector2.zero;

	private Vector2 fullSlicedSpriteDimensions = Vector2.zero;

	private Vector2 currentDimensions = Vector2.zero;

	[SerializeField]
	private float percent;

	private bool isProgressComplete;

	public GameObject sendMessageTarget;

	public string SendMessageOnProgressCompleteMethodName = string.Empty;

	public float Value
	{
		get
		{
			return percent;
		}
		set
		{
			percent = Mathf.Clamp(value, 0f, 1f);
			if (!Application.isPlaying)
			{
				return;
			}
			if (clippedSpriteBar != null)
			{
				clippedSpriteBar.clipTopRight = new Vector2(Value, 1f);
			}
			else if (scalableBar != null)
			{
				Transform transform = scalableBar;
				float value2 = Value;
				Vector3 localScale = scalableBar.localScale;
				float y = localScale.y;
				Vector3 localScale2 = scalableBar.localScale;
				transform.localScale = new Vector3(value2, y, localScale2.z);
			}
			else if (slicedSpriteBar != null)
			{
				InitializeSlicedSpriteDimensions();
				float newX = Mathf.Lerp(emptySlicedSpriteDimensions.x, fullSlicedSpriteDimensions.x, Value);
				currentDimensions.Set(newX, fullSlicedSpriteDimensions.y);
				slicedSpriteBar.dimensions = currentDimensions;
			}
			if (!isProgressComplete && Value == 1f)
			{
				isProgressComplete = true;
				if (this.OnProgressComplete != null)
				{
					this.OnProgressComplete();
				}
				if (sendMessageTarget != null && SendMessageOnProgressCompleteMethodName.Length > 0)
				{
					sendMessageTarget.SendMessage(SendMessageOnProgressCompleteMethodName, this, SendMessageOptions.RequireReceiver);
				}
			}
			else if (isProgressComplete && Value < 1f)
			{
				isProgressComplete = false;
			}
		}
	}

	public event Action OnProgressComplete;

	private void Start()
	{
		InitializeSlicedSpriteDimensions();
		Value = percent;
	}

	private void InitializeSlicedSpriteDimensions()
	{
		if (!initializedSlicedSpriteDimensions)
		{
			if (slicedSpriteBar != null)
			{
				tk2dSpriteDefinition currentSprite = slicedSpriteBar.CurrentSprite;
				Vector3 vector = currentSprite.boundsData[1];
				fullSlicedSpriteDimensions = slicedSpriteBar.dimensions;
				emptySlicedSpriteDimensions.Set((slicedSpriteBar.borderLeft + slicedSpriteBar.borderRight) * vector.x / currentSprite.texelSize.x, fullSlicedSpriteDimensions.y);
			}
			initializedSlicedSpriteDimensions = true;
		}
	}
}
