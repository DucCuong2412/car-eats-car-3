using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DronController : MonoBehaviour
{
	public GameObject PointGnrBomb;

	public Animator AnimTorn;

	public Animator AnimDropFront;

	public Animator AnimDropBack;

	public int TypeDron;

	public float speed = 2f;

	public float width = 5f;

	public float height = 2f;

	public float offsetOfCarY = 7f;

	public float timeToNextAttack = 2f;

	public float timeStayInTarget = 1f;

	public float speedToTarget = 0.6f;

	public Vector3 offsetForTarget = default(Vector3);

	public float dmg = 1f;

	public float distanceToTargetAttack = 20f;

	private float tMinDist = 1000000f;

	private float tCurDist = -1f;

	private bool canAttack = true;

	private Car2DAIController target;

	private int pointsNum = 1;

	private float counter;

	private float counterTime;

	private List<Vector3> pointsList = new List<Vector3>();

	private int _moveForward = Animator.StringToHash("moveForward");

	private int _isAttack = Animator.StringToHash("isAttack");

	private int _isBombDropped = Animator.StringToHash("isBombDropped");

	private float tCos = -1f;

	private void OnEnable()
	{
		pointsList.Clear();
		for (int i = 0; i < pointsNum; i++)
		{
			pointsList.Add(Vector3.zero);
		}
		target = null;
		canAttack = true;
	}

	private void Update()
	{
		if (RaceLogic.instance.car == null)
		{
			return;
		}
		ChengCordsMove();
		if (counterTime >= 0f && canAttack)
		{
			counterTime -= Time.deltaTime;
		}
		if (!(counterTime <= 0f) || !canAttack)
		{
			return;
		}
		tMinDist = 1000000f;
		for (int i = 0; i < RaceLogic.instance.race.activeAIs.Count; i++)
		{
			if (RaceLogic.instance.race.activeAIs[i].gameObject.activeSelf)
			{
				tCurDist = Vector3.Distance(base.transform.position, RaceLogic.instance.race.activeAIs[i].transform.position);
				if (tCurDist < distanceToTargetAttack && tMinDist > tCurDist)
				{
					tMinDist = tCurDist;
					target = RaceLogic.instance.race.activeAIs[i];
				}
			}
		}
		if (target != null)
		{
			counterTime = timeToNextAttack;
			canAttack = false;
			StartCoroutine(moveToTarget());
		}
	}

	private void ChengCordsMove()
	{
		for (int num = pointsNum - 1; num > 0; num--)
		{
			pointsList[num] = pointsList[num - 1];
		}
		pointsList[0] = RaceLogic.instance.car.transform.position;
		if (target == null)
		{
			counter += Time.deltaTime * speed;
			tCos = Mathf.Cos(counter);
			if (tCos < -0.97f)
			{
				AnimTorn.SetBool(_moveForward, value: true);
			}
			else if (tCos > 0.97f)
			{
				AnimTorn.SetBool(_moveForward, value: false);
			}
			Transform transform = base.transform;
			Vector3 vector = pointsList[pointsNum - 1];
			float x = vector.x + tCos * width;
			Vector3 vector2 = pointsList[pointsNum - 1];
			float y = vector2.y + offsetOfCarY + Mathf.Sin(counter) * height;
			Vector3 vector3 = pointsList[pointsNum - 1];
			transform.position = new Vector3(x, y, vector3.z);
		}
	}

	private IEnumerator moveToTarget()
	{
		float tCouterMoveToTarg2 = 0f;
		Vector3 position = base.transform.position;
		float x = position.x;
		Vector3 position2 = target.transform.position;
		if (x < position2.x)
		{
			AnimTorn.SetBool(_moveForward, value: true);
		}
		else
		{
			AnimTorn.SetBool(_moveForward, value: false);
		}
		while (tCouterMoveToTarg2 < 1f)
		{
			tCouterMoveToTarg2 += Time.deltaTime * speedToTarget;
			Transform transform = base.transform;
			Vector3 vector = pointsList[pointsNum - 1];
			float x2 = vector.x + Mathf.Cos(counter) * width;
			Vector3 vector2 = pointsList[pointsNum - 1];
			float y = vector2.y + offsetOfCarY + Mathf.Sin(counter) * height;
			Vector3 vector3 = pointsList[pointsNum - 1];
			transform.position = Vector3.Lerp(new Vector3(x2, y, vector3.z), target.transform.position + offsetForTarget, tCouterMoveToTarg2);
			yield return null;
		}
		if (AnimDropFront.gameObject.activeSelf)
		{
			AnimDropFront.SetBool(_isAttack, value: true);
		}
		if (AnimDropBack.gameObject.activeSelf)
		{
			AnimDropBack.SetBool(_isAttack, value: true);
		}
		float tTimeStay = timeStayInTarget;
		while (tTimeStay > 0f && target.gameObject.activeSelf)
		{
			tTimeStay -= Time.deltaTime;
			Transform transform2 = base.transform;
			Vector3 position3 = target.transform.position;
			float x3 = position3.x;
			Vector3 position4 = target.transform.position;
			float y2 = position4.y;
			Vector3 position5 = target.transform.position;
			transform2.position = new Vector3(x3, y2, position5.z) + offsetForTarget;
			if (TypeDron == 2 || TypeDron == 3)
			{
				target.SetDamage(dmg);
			}
			yield return null;
		}
		if (TypeDron == 1)
		{
			GameObject @object = Pool.instance.GetObject("Bomb_0");
			@object.SetActive(value: true);
			@object.transform.position = PointGnrBomb.transform.position;
			BombLogic.instance.bomb(@object.transform, dmg);
			if (AnimDropFront.gameObject.activeSelf)
			{
				AnimDropFront.SetTrigger(_isBombDropped);
			}
			if (AnimDropBack.gameObject.activeSelf)
			{
				AnimDropBack.SetTrigger(_isBombDropped);
			}
		}
		else if (TypeDron == 2 || TypeDron == 3)
		{
			if (AnimDropFront.gameObject.activeSelf)
			{
				AnimDropFront.SetBool(_isAttack, value: false);
			}
			if (AnimDropBack.gameObject.activeSelf)
			{
				AnimDropBack.SetBool(_isAttack, value: false);
			}
		}
		tCouterMoveToTarg2 = 0f;
		while (tCouterMoveToTarg2 < 1f)
		{
			tCouterMoveToTarg2 += Time.deltaTime * speedToTarget;
			Transform transform3 = base.transform;
			Vector3 a = target.transform.position + offsetForTarget;
			Vector3 vector4 = pointsList[pointsNum - 1];
			float x4 = vector4.x + Mathf.Cos(counter) * width;
			Vector3 vector5 = pointsList[pointsNum - 1];
			float y3 = vector5.y + offsetOfCarY + Mathf.Sin(counter) * height;
			Vector3 vector6 = pointsList[pointsNum - 1];
			transform3.position = Vector3.Lerp(a, new Vector3(x4, y3, vector6.z), tCouterMoveToTarg2);
			yield return null;
		}
		canAttack = true;
		target = null;
	}
}
