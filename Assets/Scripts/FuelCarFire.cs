using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FuelCarFire : MonoBehaviour
{
	[SerializeField]
	private class fireMove
	{
		public Vector3 prevPos = default(Vector3);

		public int CountStay;

		public GameObject Fire;

		public GameObject MoveObj;
	}

	public float TimeBetvinGnr = 0.1f;

	public int coll = 10;

	public Vector2 direction;

	public float forces;

	private List<fireMove> list = new List<fireMove>();

	private Vector3 prev = Vector3.zero;

	private float iteratorGnr = 1f;

	private void Start()
	{
		list.Clear();
	}

	private void setFragments()
	{
		Pool.Animate(Pool.Explosion.exp7, base.transform.position, "GUI");
	}

	private IEnumerator upd()
	{
		for (int i = 0; i < coll; i++)
		{
			if (Vector3.Distance(prev, base.transform.position) < 2f)
			{
				break;
			}
			prev = base.transform.position;
			GameObject _obj2 = Pool.GetFuelBomb(base.transform.position);
			fireMove cl = new fireMove
			{
				prevPos = Vector3.zero,
				CountStay = 0,
				MoveObj = _obj2
			};
			list.Add(cl);
			yield return new WaitForSeconds(0.2f);
		}
	}

	private void Update()
	{
		if (iteratorGnr > 0f)
		{
			iteratorGnr -= Time.deltaTime;
			return;
		}
		iteratorGnr = TimeBetvinGnr;
		StartCoroutine(upd());
	}
}
