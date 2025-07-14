using System.Collections;
using UnityEngine;

public class CopterLogic : MonoBehaviour
{
	public Car2DAIController aiController;

	public float lifeTime = 100f;

	public float heightAboveSurfaceBase = 3f;

	public LayerMask groundCheckLayerMask;

	public float moveSpeed = 3f;

	public float distanceToSkipRotation = 1.5f;

	private float oldHeight;

	private float lifeTimeCounter;

	[Header("Lurch")]
	public bool isLurching = true;

	public float lurchAngleOffset = 0.5f;

	[Range(0f, 90f)]
	public float minLurchAngle;

	[Range(0f, 90f)]
	public float maxLurchAngle = 30f;

	[Range(0f, 90f)]
	public float minLongLurchAngle = 20f;

	private float lurchAngle;

	private bool lurchWave;

	[Header("Dynamic movement Y")]
	public float offsetDifference = 0.05f;

	public float maxOffset = 1f;

	private float offsetY;

	private float offsetDirection = 1f;

	private Transform target
	{
		get
		{
			if (RaceLogic.instance.car != null)
			{
				return RaceLogic.instance.car.gameObject.transform;
			}
			return null;
		}
	}

	private bool isAhead
	{
		get
		{
			return aiController.constructor.isAhead;
		}
		set
		{
			aiController.constructor.isAhead = value;
		}
	}

	private new Transform transform => aiController.transform;

	private void Update()
	{
		lifeTime -= Time.deltaTime;
		if (lifeTime <= 0f)
		{
			StartCoroutine(death());
		}
		else
		{
			FlyingUnitLogic();
		}
	}

	private IEnumerator death()
	{
		float fraction = 0f;
		while (fraction < 1f)
		{
			fraction += Time.deltaTime;
			Transform transform = this.transform;
			Vector3 position = this.transform.position;
			Vector3 position2 = this.transform.position;
			float x = position2.x;
			Vector3 position3 = this.transform.position;
			transform.position = Vector3.Lerp(position, new Vector3(x, position3.y + 0.3f), fraction);
			yield return null;
		}
		Audio.Stop("helicopter");
		Pool.Animate(Pool.Explosion.exp25, this.transform.position);
		base.gameObject.SetActive(value: false);
	}

	private IEnumerator Wait()
	{
		while (RaceLogic.instance.car == null)
		{
			yield return 0;
		}
	}

	private void OnEnable()
	{
		oldHeight = 0f;
		lifeTime = 10f;
		lifeTimeCounter = lifeTime;
		StartCoroutine(Lurch());
		StartCoroutine(Wait());
		if (target != null)
		{
			Audio.PlayAsync("helicopter", 1f, loop: true);
		}
	}

	private IEnumerator Lurch()
	{
		while (true)
		{
			if (!isLurching)
			{
				yield return 0;
				continue;
			}
			float angleMultiplier = (!isAhead) ? (-1f) : 1f;
			lurchAngle += lurchAngleOffset;
			if (lurchWave)
			{
				lurchAngle -= lurchAngleOffset * 2f;
			}
			lurchAngle = Mathf.Clamp(lurchAngle, minLurchAngle, maxLurchAngle);
			if (lurchWave && lurchAngle <= minLongLurchAngle)
			{
				lurchWave = false;
			}
			if (lurchAngle >= maxLurchAngle)
			{
				lurchWave = true;
			}
			transform.rotation = Quaternion.Euler(0f, 0f, lurchAngle * angleMultiplier);
			yield return 0;
		}
	}

	private void FlyingUnitLogic()
	{
		if (!(target == null))
		{
			LookAtTargetFlying();
			this.transform.position += (isAhead ? Vector3.right : (-Vector3.right)) * moveSpeed * Time.deltaTime;
			float num = 0f;
			float b = 0f;
			float num2 = 0f;
			Vector3 position = target.transform.position;
			num = position.y;
			RaycastHit2D raycastHit2D = Physics2D.Raycast(this.transform.position + Vector3.up * 5f, Vector2.down, 100f, 1 << LayerMask.NameToLayer("Ground"));
			if (raycastHit2D.collider != null)
			{
				Vector2 point = raycastHit2D.point;
				b = point.y;
			}
			num = Mathf.Max(num, b);
			float num3 = num - oldHeight;
			num = oldHeight + num3 * 0.25f;
			offsetY += offsetDifference * offsetDirection;
			if (Mathf.Abs(offsetY) >= maxOffset)
			{
				offsetDirection *= -1f;
			}
			num2 = heightAboveSurfaceBase + offsetY;
			Transform transform = this.transform;
			Vector3 position2 = this.transform.position;
			float x = position2.x;
			Vector3 position3 = target.position;
			transform.position = new Vector3(x, position3.y + heightAboveSurfaceBase, 0f);
			oldHeight = num;
		}
	}

	private void LookAtTargetFlying()
	{
		Vector3 position = target.position;
		float x = position.x;
		Vector3 position2 = transform.position;
		bool flag = x - position2.x < 0f;
		if (flag == isAhead)
		{
			Vector3 position3 = target.position;
			float x2 = position3.x;
			Vector3 position4 = transform.position;
			if (Mathf.Abs(x2 - position4.x) >= distanceToSkipRotation)
			{
				isAhead = flag;
				lurchAngle = 0f;
				lurchWave = false;
				aiController.constructor.Turn(!isAhead);
			}
		}
	}
}
