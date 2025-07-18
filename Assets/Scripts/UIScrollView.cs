using System;
using UnityEngine;

[ExecuteInEditMode]
[RequireComponent(typeof(UIPanel))]
[AddComponentMenu("NGUI/Interaction/Scroll View")]
public class UIScrollView : MonoBehaviour
{
	public enum Movement
	{
		Horizontal,
		Vertical,
		Unrestricted,
		Custom
	}

	public enum DragEffect
	{
		None,
		Momentum,
		MomentumAndSpring
	}

	public enum ShowCondition
	{
		Always,
		OnlyIfNeeded,
		WhenDragging
	}

	public delegate void OnDragNotification();

	public static BetterList<UIScrollView> list = new BetterList<UIScrollView>();

	public Movement movement;

	public DragEffect dragEffect = DragEffect.MomentumAndSpring;

	public bool restrictWithinPanel = true;

	public bool disableDragIfFits;

	public bool smoothDragStart = true;

	public bool iOSDragEmulation = true;

	public float scrollWheelFactor = 0.25f;

	public float momentumAmount = 35f;

	public UIProgressBar horizontalScrollBar;

	public UIProgressBar verticalScrollBar;

	public ShowCondition showScrollBars = ShowCondition.OnlyIfNeeded;

	public Vector2 customMovement = new Vector2(1f, 0f);

	public UIWidget.Pivot contentPivot;

	public OnDragNotification onDragStarted;

	public OnDragNotification onDragFinished;

	public OnDragNotification onMomentumMove;

	public OnDragNotification onStoppedMoving;

	[HideInInspector]
	[SerializeField]
	private Vector3 scale = new Vector3(1f, 0f, 0f);

	[SerializeField]
	[HideInInspector]
	private Vector2 relativePositionOnReset = Vector2.zero;

	protected Transform mTrans;

	protected UIPanel mPanel;

	protected Plane mPlane;

	protected Vector3 mLastPos;

	protected bool mPressed;

	protected Vector3 mMomentum = Vector3.zero;

	protected float mScroll;

	protected Bounds mBounds;

	protected bool mCalculatedBounds;

	protected bool mShouldMove;

	protected bool mIgnoreCallbacks;

	protected int mDragID = -10;

	protected Vector2 mDragStartOffset = Vector2.zero;

	protected bool mDragStarted;

	[NonSerialized]
	private bool mStarted;

	[HideInInspector]
	public UICenterOnChild centerOnChild;

	public UIPanel panel => mPanel;

	public bool isDragging => mPressed && mDragStarted;

	public virtual Bounds bounds
	{
		get
		{
			if (!mCalculatedBounds)
			{
				mCalculatedBounds = true;
				mTrans = base.transform;
				mBounds = NGUIMath.CalculateRelativeWidgetBounds(mTrans, mTrans);
			}
			return mBounds;
		}
	}

	public bool canMoveHorizontally => movement == Movement.Horizontal || movement == Movement.Unrestricted || (movement == Movement.Custom && customMovement.x != 0f);

	public bool canMoveVertically => movement == Movement.Vertical || movement == Movement.Unrestricted || (movement == Movement.Custom && customMovement.y != 0f);

	public virtual bool shouldMoveHorizontally
	{
		get
		{
			Vector3 size = bounds.size;
			float num = size.x;
			if (mPanel.clipping == UIDrawCall.Clipping.SoftClip)
			{
				float num2 = num;
				Vector2 clipSoftness = mPanel.clipSoftness;
				num = num2 + clipSoftness.x * 2f;
			}
			return Mathf.RoundToInt(num - mPanel.width) > 0;
		}
	}

	public virtual bool shouldMoveVertically
	{
		get
		{
			Vector3 size = bounds.size;
			float num = size.y;
			if (mPanel.clipping == UIDrawCall.Clipping.SoftClip)
			{
				float num2 = num;
				Vector2 clipSoftness = mPanel.clipSoftness;
				num = num2 + clipSoftness.y * 2f;
			}
			return Mathf.RoundToInt(num - mPanel.height) > 0;
		}
	}

	protected virtual bool shouldMove
	{
		get
		{
			if (!disableDragIfFits)
			{
				return true;
			}
			if (mPanel == null)
			{
				mPanel = GetComponent<UIPanel>();
			}
			Vector4 finalClipRegion = mPanel.finalClipRegion;
			Bounds bounds = this.bounds;
			float num = (finalClipRegion.z != 0f) ? (finalClipRegion.z * 0.5f) : ((float)Screen.width);
			float num2 = (finalClipRegion.w != 0f) ? (finalClipRegion.w * 0.5f) : ((float)Screen.height);
			if (canMoveHorizontally)
			{
				Vector3 min = bounds.min;
				if (min.x < finalClipRegion.x - num)
				{
					return true;
				}
				Vector3 max = bounds.max;
				if (max.x > finalClipRegion.x + num)
				{
					return true;
				}
			}
			if (canMoveVertically)
			{
				Vector3 min2 = bounds.min;
				if (min2.y < finalClipRegion.y - num2)
				{
					return true;
				}
				Vector3 max2 = bounds.max;
				if (max2.y > finalClipRegion.y + num2)
				{
					return true;
				}
			}
			return false;
		}
	}

	public Vector3 currentMomentum
	{
		get
		{
			return mMomentum;
		}
		set
		{
			mMomentum = value;
			mShouldMove = true;
		}
	}

	private void Awake()
	{
		mTrans = base.transform;
		mPanel = GetComponent<UIPanel>();
		if (mPanel.clipping == UIDrawCall.Clipping.None)
		{
			mPanel.clipping = UIDrawCall.Clipping.ConstrainButDontClip;
		}
		if (movement != Movement.Custom && scale.sqrMagnitude > 0.001f)
		{
			if (scale.x == 1f && scale.y == 0f)
			{
				movement = Movement.Horizontal;
			}
			else if (scale.x == 0f && scale.y == 1f)
			{
				movement = Movement.Vertical;
			}
			else if (scale.x == 1f && scale.y == 1f)
			{
				movement = Movement.Unrestricted;
			}
			else
			{
				movement = Movement.Custom;
				customMovement.x = scale.x;
				customMovement.y = scale.y;
			}
			scale = Vector3.zero;
		}
		if (contentPivot == UIWidget.Pivot.TopLeft && relativePositionOnReset != Vector2.zero)
		{
			contentPivot = NGUIMath.GetPivot(new Vector2(relativePositionOnReset.x, 1f - relativePositionOnReset.y));
			relativePositionOnReset = Vector2.zero;
		}
	}

	private void OnEnable()
	{
		list.Add(this);
		if (mStarted && Application.isPlaying)
		{
			CheckScrollbars();
		}
	}

	private void Start()
	{
		mStarted = true;
		if (Application.isPlaying)
		{
			CheckScrollbars();
		}
	}

	private void CheckScrollbars()
	{
		if (horizontalScrollBar != null)
		{
			EventDelegate.Add(horizontalScrollBar.onChange, OnScrollBar);
			horizontalScrollBar.alpha = ((showScrollBars != 0 && !shouldMoveHorizontally) ? 0f : 1f);
		}
		if (verticalScrollBar != null)
		{
			EventDelegate.Add(verticalScrollBar.onChange, OnScrollBar);
			verticalScrollBar.alpha = ((showScrollBars != 0 && !shouldMoveVertically) ? 0f : 1f);
		}
	}

	private void OnDisable()
	{
		list.Remove(this);
	}

	public bool RestrictWithinBounds(bool instant)
	{
		return RestrictWithinBounds(instant, horizontal: true, vertical: true);
	}

	public bool RestrictWithinBounds(bool instant, bool horizontal, bool vertical)
	{
		Bounds bounds = this.bounds;
		Vector3 vector = mPanel.CalculateConstrainOffset(bounds.min, bounds.max);
		if (!horizontal)
		{
			vector.x = 0f;
		}
		if (!vertical)
		{
			vector.y = 0f;
		}
		if (vector.sqrMagnitude > 0.1f)
		{
			if (!instant && dragEffect == DragEffect.MomentumAndSpring)
			{
				Vector3 pos = mTrans.localPosition + vector;
				pos.x = Mathf.Round(pos.x);
				pos.y = Mathf.Round(pos.y);
				SpringPanel.Begin(mPanel.gameObject, pos, 13f).strength = 8f;
			}
			else
			{
				MoveRelative(vector);
				if (Mathf.Abs(vector.x) > 0.01f)
				{
					mMomentum.x = 0f;
				}
				if (Mathf.Abs(vector.y) > 0.01f)
				{
					mMomentum.y = 0f;
				}
				if (Mathf.Abs(vector.z) > 0.01f)
				{
					mMomentum.z = 0f;
				}
				mScroll = 0f;
			}
			return true;
		}
		return false;
	}

	public void DisableSpring()
	{
		SpringPanel component = GetComponent<SpringPanel>();
		if (component != null)
		{
			component.enabled = false;
		}
	}

	public void UpdateScrollbars()
	{
		UpdateScrollbars(recalculateBounds: true);
	}

	public virtual void UpdateScrollbars(bool recalculateBounds)
	{
		if (mPanel == null)
		{
			return;
		}
		if (horizontalScrollBar != null || verticalScrollBar != null)
		{
			if (recalculateBounds)
			{
				mCalculatedBounds = false;
				mShouldMove = shouldMove;
			}
			Bounds bounds = this.bounds;
			Vector2 vector = bounds.min;
			Vector2 vector2 = bounds.max;
			if (horizontalScrollBar != null && vector2.x > vector.x)
			{
				Vector4 finalClipRegion = mPanel.finalClipRegion;
				int num = Mathf.RoundToInt(finalClipRegion.z);
				if ((num & 1) != 0)
				{
					num--;
				}
				float f = (float)num * 0.5f;
				f = Mathf.Round(f);
				if (mPanel.clipping == UIDrawCall.Clipping.SoftClip)
				{
					float num2 = f;
					Vector2 clipSoftness = mPanel.clipSoftness;
					f = num2 - clipSoftness.x;
				}
				float contentSize = vector2.x - vector.x;
				float viewSize = f * 2f;
				float x = vector.x;
				float x2 = vector2.x;
				float num3 = finalClipRegion.x - f;
				float num4 = finalClipRegion.x + f;
				x = num3 - x;
				x2 -= num4;
				UpdateScrollbars(horizontalScrollBar, x, x2, contentSize, viewSize, inverted: false);
			}
			if (verticalScrollBar != null && vector2.y > vector.y)
			{
				Vector4 finalClipRegion2 = mPanel.finalClipRegion;
				int num5 = Mathf.RoundToInt(finalClipRegion2.w);
				if ((num5 & 1) != 0)
				{
					num5--;
				}
				float f2 = (float)num5 * 0.5f;
				f2 = Mathf.Round(f2);
				if (mPanel.clipping == UIDrawCall.Clipping.SoftClip)
				{
					float num6 = f2;
					Vector2 clipSoftness2 = mPanel.clipSoftness;
					f2 = num6 - clipSoftness2.y;
				}
				float contentSize2 = vector2.y - vector.y;
				float viewSize2 = f2 * 2f;
				float y = vector.y;
				float y2 = vector2.y;
				float num7 = finalClipRegion2.y - f2;
				float num8 = finalClipRegion2.y + f2;
				y = num7 - y;
				y2 -= num8;
				UpdateScrollbars(verticalScrollBar, y, y2, contentSize2, viewSize2, inverted: true);
			}
		}
		else if (recalculateBounds)
		{
			mCalculatedBounds = false;
		}
	}

	protected void UpdateScrollbars(UIProgressBar slider, float contentMin, float contentMax, float contentSize, float viewSize, bool inverted)
	{
		if (slider == null)
		{
			return;
		}
		mIgnoreCallbacks = true;
		float num;
		if (viewSize < contentSize)
		{
			contentMin = Mathf.Clamp01(contentMin / contentSize);
			contentMax = Mathf.Clamp01(contentMax / contentSize);
			num = contentMin + contentMax;
			slider.value = (inverted ? ((!(num > 0.001f)) ? 0f : (1f - contentMin / num)) : ((!(num > 0.001f)) ? 1f : (contentMin / num)));
		}
		else
		{
			contentMin = Mathf.Clamp01((0f - contentMin) / contentSize);
			contentMax = Mathf.Clamp01((0f - contentMax) / contentSize);
			num = contentMin + contentMax;
			slider.value = (inverted ? ((!(num > 0.001f)) ? 0f : (1f - contentMin / num)) : ((!(num > 0.001f)) ? 1f : (contentMin / num)));
			if (contentSize > 0f)
			{
				contentMin = Mathf.Clamp01(contentMin / contentSize);
				contentMax = Mathf.Clamp01(contentMax / contentSize);
				num = contentMin + contentMax;
			}
		}
		UIScrollBar uIScrollBar = slider as UIScrollBar;
		if (uIScrollBar != null)
		{
			uIScrollBar.barSize = 1f - num;
		}
		mIgnoreCallbacks = false;
	}

	public virtual void SetDragAmount(float x, float y, bool updateScrollbars)
	{
		if (mPanel == null)
		{
			mPanel = GetComponent<UIPanel>();
		}
		DisableSpring();
		Bounds bounds = this.bounds;
		Vector3 min = bounds.min;
		float x2 = min.x;
		Vector3 max = bounds.max;
		if (x2 == max.x)
		{
			return;
		}
		Vector3 min2 = bounds.min;
		float y2 = min2.y;
		Vector3 max2 = bounds.max;
		if (y2 == max2.y)
		{
			return;
		}
		Vector4 finalClipRegion = mPanel.finalClipRegion;
		float num = finalClipRegion.z * 0.5f;
		float num2 = finalClipRegion.w * 0.5f;
		Vector3 min3 = bounds.min;
		float num3 = min3.x + num;
		Vector3 max3 = bounds.max;
		float num4 = max3.x - num;
		Vector3 min4 = bounds.min;
		float num5 = min4.y + num2;
		Vector3 max4 = bounds.max;
		float num6 = max4.y - num2;
		if (mPanel.clipping == UIDrawCall.Clipping.SoftClip)
		{
			float num7 = num3;
			Vector2 clipSoftness = mPanel.clipSoftness;
			num3 = num7 - clipSoftness.x;
			float num8 = num4;
			Vector2 clipSoftness2 = mPanel.clipSoftness;
			num4 = num8 + clipSoftness2.x;
			float num9 = num5;
			Vector2 clipSoftness3 = mPanel.clipSoftness;
			num5 = num9 - clipSoftness3.y;
			float num10 = num6;
			Vector2 clipSoftness4 = mPanel.clipSoftness;
			num6 = num10 + clipSoftness4.y;
		}
		float num11 = Mathf.Lerp(num3, num4, x);
		float num12 = Mathf.Lerp(num6, num5, y);
		if (!updateScrollbars)
		{
			Vector3 localPosition = mTrans.localPosition;
			if (canMoveHorizontally)
			{
				localPosition.x += finalClipRegion.x - num11;
			}
			if (canMoveVertically)
			{
				localPosition.y += finalClipRegion.y - num12;
			}
			mTrans.localPosition = localPosition;
		}
		if (canMoveHorizontally)
		{
			finalClipRegion.x = num11;
		}
		if (canMoveVertically)
		{
			finalClipRegion.y = num12;
		}
		Vector4 baseClipRegion = mPanel.baseClipRegion;
		mPanel.clipOffset = new Vector2(finalClipRegion.x - baseClipRegion.x, finalClipRegion.y - baseClipRegion.y);
		if (updateScrollbars)
		{
			UpdateScrollbars(mDragID == -10);
		}
	}

	public void InvalidateBounds()
	{
		mCalculatedBounds = false;
	}

	[ContextMenu("Reset Clipping Position")]
	public void ResetPosition()
	{
		if (NGUITools.GetActive(this))
		{
			mCalculatedBounds = false;
			Vector2 pivotOffset = NGUIMath.GetPivotOffset(contentPivot);
			SetDragAmount(pivotOffset.x, 1f - pivotOffset.y, updateScrollbars: false);
			SetDragAmount(pivotOffset.x, 1f - pivotOffset.y, updateScrollbars: true);
		}
	}

	public void UpdatePosition()
	{
		if (!mIgnoreCallbacks && (horizontalScrollBar != null || verticalScrollBar != null))
		{
			mIgnoreCallbacks = true;
			mCalculatedBounds = false;
			Vector2 pivotOffset = NGUIMath.GetPivotOffset(contentPivot);
			float x = (!(horizontalScrollBar != null)) ? pivotOffset.x : horizontalScrollBar.value;
			float y = (!(verticalScrollBar != null)) ? (1f - pivotOffset.y) : verticalScrollBar.value;
			SetDragAmount(x, y, updateScrollbars: false);
			UpdateScrollbars(recalculateBounds: true);
			mIgnoreCallbacks = false;
		}
	}

	public void OnScrollBar()
	{
		if (!mIgnoreCallbacks)
		{
			mIgnoreCallbacks = true;
			float x = (!(horizontalScrollBar != null)) ? 0f : horizontalScrollBar.value;
			float y = (!(verticalScrollBar != null)) ? 0f : verticalScrollBar.value;
			SetDragAmount(x, y, updateScrollbars: false);
			mIgnoreCallbacks = false;
		}
	}

	public virtual void MoveRelative(Vector3 relative)
	{
		mTrans.localPosition += relative;
		Vector2 clipOffset = mPanel.clipOffset;
		clipOffset.x -= relative.x;
		clipOffset.y -= relative.y;
		mPanel.clipOffset = clipOffset;
		UpdateScrollbars(recalculateBounds: false);
	}

	public void MoveAbsolute(Vector3 absolute)
	{
		Vector3 a = mTrans.InverseTransformPoint(absolute);
		Vector3 b = mTrans.InverseTransformPoint(Vector3.zero);
		MoveRelative(a - b);
	}

	public void Press(bool pressed)
	{
		if (UICamera.currentScheme == UICamera.ControlScheme.Controller)
		{
			return;
		}
		if (smoothDragStart && pressed)
		{
			mDragStarted = false;
			mDragStartOffset = Vector2.zero;
		}
		if (!base.enabled || !NGUITools.GetActive(base.gameObject))
		{
			return;
		}
		if (!pressed && mDragID == UICamera.currentTouchID)
		{
			mDragID = -10;
		}
		mCalculatedBounds = false;
		mShouldMove = shouldMove;
		if (!mShouldMove)
		{
			return;
		}
		mPressed = pressed;
		if (pressed)
		{
			mMomentum = Vector3.zero;
			mScroll = 0f;
			DisableSpring();
			mLastPos = UICamera.lastWorldPosition;
			mPlane = new Plane(mTrans.rotation * Vector3.back, mLastPos);
			Vector2 clipOffset = mPanel.clipOffset;
			clipOffset.x = Mathf.Round(clipOffset.x);
			clipOffset.y = Mathf.Round(clipOffset.y);
			mPanel.clipOffset = clipOffset;
			Vector3 localPosition = mTrans.localPosition;
			localPosition.x = Mathf.Round(localPosition.x);
			localPosition.y = Mathf.Round(localPosition.y);
			mTrans.localPosition = localPosition;
			if (!smoothDragStart)
			{
				mDragStarted = true;
				mDragStartOffset = Vector2.zero;
				if (onDragStarted != null)
				{
					onDragStarted();
				}
			}
		}
		else if (centerOnChild != null)
		{
			centerOnChild.Recenter();
		}
		else
		{
			if (restrictWithinPanel && mPanel.clipping != 0)
			{
				RestrictWithinBounds(dragEffect == DragEffect.None, canMoveHorizontally, canMoveVertically);
			}
			if (mDragStarted && onDragFinished != null)
			{
				onDragFinished();
			}
			if (!mShouldMove && onStoppedMoving != null)
			{
				onStoppedMoving();
			}
		}
	}

	public void Drag()
	{
		if (UICamera.currentScheme == UICamera.ControlScheme.Controller || !base.enabled || !NGUITools.GetActive(base.gameObject) || !mShouldMove)
		{
			return;
		}
		if (mDragID == -10)
		{
			mDragID = UICamera.currentTouchID;
		}
		UICamera.currentTouch.clickNotification = UICamera.ClickNotification.BasedOnDelta;
		if (smoothDragStart && !mDragStarted)
		{
			mDragStarted = true;
			mDragStartOffset = UICamera.currentTouch.totalDelta;
			if (onDragStarted != null)
			{
				onDragStarted();
			}
		}
		Ray ray = (!smoothDragStart) ? UICamera.currentCamera.ScreenPointToRay(UICamera.currentTouch.pos) : UICamera.currentCamera.ScreenPointToRay(UICamera.currentTouch.pos - mDragStartOffset);
		float enter = 0f;
		if (!mPlane.Raycast(ray, out enter))
		{
			return;
		}
		Vector3 point = ray.GetPoint(enter);
		Vector3 vector = point - mLastPos;
		mLastPos = point;
		if (vector.x != 0f || vector.y != 0f || vector.z != 0f)
		{
			vector = mTrans.InverseTransformDirection(vector);
			if (movement == Movement.Horizontal)
			{
				vector.y = 0f;
				vector.z = 0f;
			}
			else if (movement == Movement.Vertical)
			{
				vector.x = 0f;
				vector.z = 0f;
			}
			else if (movement == Movement.Unrestricted)
			{
				vector.z = 0f;
			}
			else
			{
				vector.Scale(customMovement);
			}
			vector = mTrans.TransformDirection(vector);
		}
		if (dragEffect == DragEffect.None)
		{
			mMomentum = Vector3.zero;
		}
		else
		{
			mMomentum = Vector3.Lerp(mMomentum, mMomentum + vector * (0.01f * momentumAmount), 0.67f);
		}
		if (!iOSDragEmulation || dragEffect != DragEffect.MomentumAndSpring)
		{
			MoveAbsolute(vector);
		}
		else if (mPanel.CalculateConstrainOffset(bounds.min, bounds.max).magnitude > 1f)
		{
			MoveAbsolute(vector * 0.5f);
			mMomentum *= 0.5f;
		}
		else
		{
			MoveAbsolute(vector);
		}
		if (restrictWithinPanel && mPanel.clipping != 0 && dragEffect != DragEffect.MomentumAndSpring)
		{
			RestrictWithinBounds(instant: true, canMoveHorizontally, canMoveVertically);
		}
	}

	public void Scroll(float delta)
	{
		if (base.enabled && NGUITools.GetActive(base.gameObject) && scrollWheelFactor != 0f)
		{
			DisableSpring();
			mShouldMove |= shouldMove;
			if (Mathf.Sign(mScroll) != Mathf.Sign(delta))
			{
				mScroll = 0f;
			}
			mScroll += delta * scrollWheelFactor;
		}
	}

	private void LateUpdate()
	{
		if (!Application.isPlaying)
		{
			return;
		}
		float deltaTime = RealTime.deltaTime;
		if (showScrollBars != 0 && ((bool)verticalScrollBar || (bool)horizontalScrollBar))
		{
			bool flag = false;
			bool flag2 = false;
			if (showScrollBars != ShowCondition.WhenDragging || mDragID != -10 || mMomentum.magnitude > 0.01f)
			{
				flag = shouldMoveVertically;
				flag2 = shouldMoveHorizontally;
			}
			if ((bool)verticalScrollBar)
			{
				float alpha = verticalScrollBar.alpha;
				alpha += ((!flag) ? ((0f - deltaTime) * 3f) : (deltaTime * 6f));
				alpha = Mathf.Clamp01(alpha);
				if (verticalScrollBar.alpha != alpha)
				{
					verticalScrollBar.alpha = alpha;
				}
			}
			if ((bool)horizontalScrollBar)
			{
				float alpha2 = horizontalScrollBar.alpha;
				alpha2 += ((!flag2) ? ((0f - deltaTime) * 3f) : (deltaTime * 6f));
				alpha2 = Mathf.Clamp01(alpha2);
				if (horizontalScrollBar.alpha != alpha2)
				{
					horizontalScrollBar.alpha = alpha2;
				}
			}
		}
		if (!mShouldMove)
		{
			return;
		}
		if (!mPressed)
		{
			if (mMomentum.magnitude > 0.0001f || mScroll != 0f)
			{
				if (movement == Movement.Horizontal)
				{
					mMomentum -= mTrans.TransformDirection(new Vector3(mScroll * 0.05f, 0f, 0f));
				}
				else if (movement == Movement.Vertical)
				{
					mMomentum -= mTrans.TransformDirection(new Vector3(0f, mScroll * 0.05f, 0f));
				}
				else if (movement == Movement.Unrestricted)
				{
					mMomentum -= mTrans.TransformDirection(new Vector3(mScroll * 0.05f, mScroll * 0.05f, 0f));
				}
				else
				{
					mMomentum -= mTrans.TransformDirection(new Vector3(mScroll * customMovement.x * 0.05f, mScroll * customMovement.y * 0.05f, 0f));
				}
				mScroll = NGUIMath.SpringLerp(mScroll, 0f, 20f, deltaTime);
				Vector3 absolute = NGUIMath.SpringDampen(ref mMomentum, 9f, deltaTime);
				MoveAbsolute(absolute);
				if (restrictWithinPanel && mPanel.clipping != 0)
				{
					if (NGUITools.GetActive(centerOnChild))
					{
						if (centerOnChild.nextPageThreshold != 0f)
						{
							mMomentum = Vector3.zero;
							mScroll = 0f;
						}
						else
						{
							centerOnChild.Recenter();
						}
					}
					else
					{
						RestrictWithinBounds(instant: false, canMoveHorizontally, canMoveVertically);
					}
				}
				if (onMomentumMove != null)
				{
					onMomentumMove();
				}
				return;
			}
			mScroll = 0f;
			mMomentum = Vector3.zero;
			SpringPanel component = GetComponent<SpringPanel>();
			if (!(component != null) || !component.enabled)
			{
				mShouldMove = false;
				if (onStoppedMoving != null)
				{
					onStoppedMoving();
				}
			}
		}
		else
		{
			mScroll = 0f;
			NGUIMath.SpringDampen(ref mMomentum, 9f, deltaTime);
		}
	}
}
