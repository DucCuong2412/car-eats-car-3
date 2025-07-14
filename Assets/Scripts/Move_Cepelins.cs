using System.Collections;
using UnityEngine;

public class Move_Cepelins : MonoBehaviour
{
	public GameObject GO;

	public float timeLife;

	public GameObject spawvn;

	[Header("BOMB")]
	public float delay;

	public float koll;

	public Animator animators;

	[Header("airStrike")]
	public bool ifCepelin = true;

	public float TreteZnachenyaLerpa = 0.003f;

	private float timer;

	private int i;

	private bool is_first = true;

	private Rigidbody2D rb2d;

	private static string CarMain = "CarMain";

	private static string CarMainChild = "CarMainChild";

	private int hash_isBombDroped = Animator.StringToHash("isBombDroped");

	public float speed = 2f;

	private void OnTriggerEnter2D(Collider2D coll)
	{
		if ((coll.tag == CarMain || coll.tag == CarMainChild) && is_first)
		{
			StartCoroutine(Booms());
		}
	}

	private void OnEnable()
	{
		if (RaceLogic.instance.AllInitedForPool)
		{
			rb2d = GO.GetComponent<Rigidbody2D>();
			timer = 0f;
			is_first = true;
			if (!ifCepelin)
			{
				StartCoroutine(Booms());
			}
		}
	}

	private IEnumerator Booms()
	{
		is_first = false;
		int time = 0;
		while ((float)time < koll)
		{
			if (ifCepelin)
			{
				Pool.GameOBJECT(Pool.Bombs.bombCepel, spawvn.transform.position);
				if (animators != null)
				{
					animators.SetBool(hash_isBombDroped, value: true);
				}
				time++;
				yield return 0;
				if (animators != null)
				{
					animators.SetBool(hash_isBombDroped, value: false);
				}
			}
			else
			{
				time++;
				Pool.GameOBJECT(Pool.Bombs.bombAirStike, spawvn.transform.position);
			}
			yield return new WaitForSeconds(delay);
		}
	}

	private void Update()
	{
		if (Time.timeScale == 0f)
		{
			return;
		}
		if (ifCepelin)
		{
			if (timeLife > timer)
			{
				timer += Time.deltaTime;
				GO.transform.localPosition += Vector3.right * speed * Time.deltaTime;
				Transform transform = GO.transform;
				Vector3 localPosition = GO.transform.localPosition;
				float x = localPosition.x;
				Vector3 localPosition2 = RaceLogic.instance.car.transform.localPosition;
				transform.localPosition = new Vector3(x, localPosition2.y + 18f);
			}
			else
			{
				StartCoroutine(death());
			}
		}
		else if (timeLife > timer)
		{
			timer += Time.deltaTime;
			Transform transform2 = GO.transform;
			Vector3 localPosition3 = GO.transform.localPosition;
			Vector3 localPosition4 = RaceLogic.instance.car.transform.localPosition;
			float x2 = localPosition4.x + 150f;
			Vector3 localPosition5 = RaceLogic.instance.car.transform.localPosition;
			transform2.localPosition = Vector3.Lerp(localPosition3, new Vector3(x2, localPosition5.y + 18f), TreteZnachenyaLerpa);
		}
		else
		{
			StartCoroutine(death());
		}
	}

	private IEnumerator death()
	{
		float fraction = 0f;
		while (fraction < 1f)
		{
			fraction += Time.deltaTime;
			Transform transform = base.transform;
			Vector3 position = base.transform.position;
			Vector3 position2 = base.transform.position;
			float x = position2.x;
			Vector3 position3 = base.transform.position;
			transform.position = Vector3.Lerp(position, new Vector3(x, position3.y + 0.1f), fraction);
			yield return null;
		}
		Pool.Animate(Pool.Explosion.exp25, base.transform.position);
		base.gameObject.SetActive(value: false);
	}
}
