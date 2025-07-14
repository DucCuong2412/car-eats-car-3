using System.Collections;
using UnityEngine;

public class bomb_lip : MonoBehaviour
{
	public GameObject go;

	public Rigidbody2D telo;

	public CircleCollider2D colls;

	private bool ground;

	[Header("BOOMS")]
	public float timer;

	public float damage;

	public float impulse = 100f;

	private int layerCastobj;

	private int layerCastGr;

	private Pool asdf;

	private Transform _parent;

	private static string CarMain = "CarMain";

	private static string CarMainChild = "CarMainChild";

	private static string Wheels = "Wheels";

	private void OnEnable()
	{
		if (RaceLogic.instance.AllInitedForPool)
		{
			StartCoroutine(rb());
		}
	}

	private IEnumerator rb()
	{
		int t = 7;
		while (t > 0)
		{
			t--;
			yield return null;
		}
		telo.velocity = Vector3.right * 30f;
		_parent = Pool.instance.gameObject.transform;
		layerCastobj = LayerMask.GetMask("Objects");
		layerCastGr = LayerMask.GetMask("Ground");
	}

	private void OnCollisionEnter2D(Collision2D coll)
	{
		if (!RaceManager.instance.isStarted)
		{
			return;
		}
		if (coll.gameObject.tag == CarMain || coll.gameObject.tag == CarMainChild)
		{
			go.transform.SetParent(RaceLogic.instance.car.gameObject.transform);
			telo.bodyType = RigidbodyType2D.Static;
			telo.isKinematic = true;
			colls.enabled = false;
			if (!ground)
			{
				StartCoroutine(Booms());
			}
			else
			{
				StartCoroutine(afterGround());
			}
		}
		if (coll.gameObject.layer == layerCastobj || coll.gameObject.layer == layerCastGr || coll.gameObject.tag == "Ground")
		{
			StartCoroutine(grounds());
		}
		if (coll.gameObject.tag == Wheels)
		{
			StartCoroutine(afterGround());
		}
	}

	private IEnumerator grounds()
	{
		ground = true;
		telo.isKinematic = true;
		telo.bodyType = RigidbodyType2D.Static;
		float time = 0f;
		while (time < timer)
		{
			time += Time.deltaTime;
			yield return 0;
		}
		Pool.Animate(Pool.Explosion.exp25, base.transform.position);
		ground = false;
		telo.isKinematic = false;
		telo.bodyType = RigidbodyType2D.Dynamic;
		colls.enabled = true;
		RaceLogic.instance.bomb_lip.Add(go);
		go.transform.SetParent(_parent);
		go.SetActive(value: false);
	}

	private IEnumerator Booms()
	{
		float time = 0f;
		Audio.PlayAsync("detonator sound");
		while (time < timer)
		{
			time += Time.deltaTime;
			yield return 0;
		}
		Audio.Stop("detonator sound");
		Audio.PlayAsync("exp_tnt_05_sn");
		Pool.Animate(Pool.Explosion.exp25, base.transform.position);
		if (Game.currentState != Game.gameState.Finish)
		{
			RaceLogic.instance.HitMainCar(damage);
		}
		ground = false;
		telo.isKinematic = false;
		telo.bodyType = RigidbodyType2D.Dynamic;
		colls.enabled = true;
		RaceLogic.instance.bomb_lip.Add(go);
		go.transform.SetParent(_parent);
		go.SetActive(value: false);
	}

	private IEnumerator afterGround()
	{
		yield return 0;
		Audio.PlayAsync("exp_tnt_05_sn");
		Pool.Animate(Pool.Explosion.exp25, base.transform.position);
		RaceLogic.instance.HitMainCar(damage);
		ground = false;
		telo.isKinematic = false;
		telo.bodyType = RigidbodyType2D.Dynamic;
		colls.enabled = true;
		RaceLogic.instance.bomb_lip.Add(go);
		go.transform.SetParent(_parent);
		go.SetActive(value: false);
	}
}
