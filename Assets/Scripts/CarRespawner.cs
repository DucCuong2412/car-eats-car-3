using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Car2DController))]
public class CarRespawner : MonoBehaviour
{
	public float timeToCanAppear = 3f;

	public Animator anim;

	private float dt;

	private Collider2D[] colliders = new Collider2D[10];

	private Dictionary<Transform, int> startLayers = new Dictionary<Transform, int>();

	private SpriteRenderer[] Sprites;

	private int layerCast;

	private bool toAlpha = true;

	public void Respawn()
	{
		layerCast = LayerMask.GetMask("EnemyCar", "Objects", "ObjectsBox", "NoShadows");
		dt = timeToCanAppear;
		Vector3 localScale = base.gameObject.transform.localScale;
		base.gameObject.transform.localScale = new Vector3(0.1f, 0.1f, 0.1f);
		RaceLogic.instance.car.EngineModule.Speed = 0f;
		RaceLogic.instance.car.EngineModule.Break(onoff: true);
		RaceLogic.instance.car.EngineModule.Break(onoff: false);
		StartCoroutine(testScaler(localScale));
		foreach (Transform key in startLayers.Keys)
		{
			key.gameObject.layer = LayerMask.NameToLayer("CarRespawn");
		}
		if (!RaceLogic.instance.deathInMarker)
		{
			RaceLogic.instance.deathInMarker = false;
		}
		base.gameObject.SetActive(value: true);
		base.enabled = true;
		StartCoroutine(Effect());
	}

	private IEnumerator testScaler(Vector3 scales)
	{
		while (true)
		{
			float x = scales.x;
			Vector3 localScale = base.gameObject.transform.localScale;
			if (!(x > localScale.x))
			{
				break;
			}
			base.gameObject.transform.localScale = Vector3.Lerp(base.gameObject.transform.localScale, base.gameObject.transform.localScale + new Vector3(0.1f, 0.1f, 0.1f), 1f);
			yield return null;
		}
		base.gameObject.transform.localScale = scales;
	}

	private IEnumerator Effect()
	{
		if (dt > 0f || AnyCollisionWithCar())
		{
			Color alpha = new Color(1f, 1f, 1f, 0f);
			Color white = Color.white;
			float t = 0f;
			if (toAlpha)
			{
				while (t < 1f)
				{
					t += Time.deltaTime * 2.5f;
					for (int i = 0; i < Sprites.Length; i++)
					{
						Sprites[i].color = Color.Lerp(white, alpha, t);
					}
					yield return null;
				}
				toAlpha = false;
			}
			else
			{
				while (t < 1f)
				{
					t += Time.deltaTime * 2.5f;
					for (int j = 0; j < Sprites.Length; j++)
					{
						Sprites[j].color = Color.Lerp(alpha, white, t);
					}
					yield return null;
				}
				toAlpha = true;
			}
			StartCoroutine(Effect());
		}
		else
		{
			for (int k = 0; k < Sprites.Length; k++)
			{
				Sprites[k].color = Color.white;
			}
			yield return null;
		}
	}

	private void ReturnToNormalState()
	{
		foreach (Transform key in startLayers.Keys)
		{
			key.gameObject.layer = startLayers[key];
		}
	}

	private void ToGround()
	{
		Vector2 origin = base.transform.position + 10f * Vector3.up - Vector3.right;
		RaycastHit2D raycastHit2D = Physics2D.Raycast(origin, -Vector2.up, float.MaxValue, 1 << LayerMask.NameToLayer("Ground"));
		Vector2 origin2 = base.transform.position + 10f * Vector3.up + Vector3.right;
		RaycastHit2D raycastHit2D2 = Physics2D.Raycast(origin2, -Vector2.up, float.MaxValue, 1 << LayerMask.NameToLayer("Ground"));
		Vector2 point = raycastHit2D2.point;
		float x = point.x;
		Vector2 point2 = raycastHit2D.point;
		float x2 = x - point2.x;
		Vector2 point3 = raycastHit2D2.point;
		float y = point3.y;
		Vector2 point4 = raycastHit2D.point;
		Vector2 from = new Vector2(x2, y - point4.y);
		float num = Vector2.Angle(from, Vector2.right);
		if (from.y < 0f)
		{
			num = 360f - num;
		}
		Vector2 point5 = raycastHit2D.point;
		float y2 = point5.y;
		Vector2 point6 = raycastHit2D2.point;
		float num2 = Mathf.Max(y2, point6.y) + 4f;
		Transform transform = base.transform;
		Vector3 position = base.transform.position;
		float x3 = position.x;
		float y3 = num2;
		Vector3 position2 = base.transform.position;
		transform.position = new Vector3(x3, y3, position2.z);
		base.transform.eulerAngles = Vector3.forward * num;
	}

	public void CheckLayers()
	{
		Transform[] componentsInChildren = base.gameObject.GetComponentsInChildren<Transform>(includeInactive: true);
		Transform[] array = componentsInChildren;
		foreach (Transform transform in array)
		{
			if (!startLayers.ContainsKey(transform))
			{
				startLayers.Add(transform, transform.gameObject.layer);
			}
		}
		Sprites = base.gameObject.GetComponentsInChildren<SpriteRenderer>(includeInactive: true);
		base.enabled = false;
	}

	private void FixedUpdate()
	{
		dt -= Time.fixedDeltaTime;
		if (dt < 0f && !AnyCollisionWithCar())
		{
			ReturnToNormalState();
			base.enabled = false;
		}
	}

	private bool AnyCollisionWithCar()
	{
		if (Physics2D.OverlapCircleNonAlloc(base.transform.position, 4f, colliders, layerCast, -10f, 10f) > 0)
		{
			return true;
		}
		return false;
	}
}
