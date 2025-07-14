using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PitController : MonoBehaviour
{
	public float HPGate = 200f;

	public float DmgDelay = 0.5f;

	public float PresserStart;

	public float PresserToCar;

	public GameObject Presser;

	public Transform PresserPoint;

	public List<GameObject> MasksObj = new List<GameObject>();

	public List<GameObject> objToON = new List<GameObject>();

	private Dictionary<Transform, int> startLayers = new Dictionary<Transform, int>();

	private int layerCast;

	private bool EndMove;

	private void OnEnable()
	{
		layerCast = LayerMask.GetMask("EnemyCar", "Objects", "ObjectsBox");
		if (RaceLogic.instance.AllInitedForPool)
		{
			for (int i = 0; i < objToON.Count; i++)
			{
				objToON[i].SetActive(value: true);
			}
			Presser.gameObject.SetActive(value: false);
		}
	}

	public void OnStartEnter()
	{
		Presser.gameObject.SetActive(value: true);
		Audio.Play("fallingrocks");
		Rigidbody2D rigidbody2D = null;
		for (int i = 0; i < MasksObj.Count; i++)
		{
			rigidbody2D = MasksObj[i].GetComponent<Rigidbody2D>();
			if (rigidbody2D != null)
			{
				rigidbody2D.isKinematic = false;
				rigidbody2D.AddForce(new Vector2(UnityEngine.Random.Range(-5, 5) * 150, UnityEngine.Random.Range(-3, 3) * 150));
			}
			else
			{
				MasksObj[i].SetActive(value: false);
			}
			rigidbody2D = null;
		}
	}

	public void OnNormalLayers()
	{
		StartCoroutine(PresserMove());
	}

	public void GateDeath()
	{
	}

	public void OnPresser()
	{
		EndMove = true;
	}

	private IEnumerator PresserMove()
	{
		EndMove = false;
		while (true)
		{
			Vector3 position = Presser.transform.position;
			float y = position.y;
			Vector3 position2 = PresserPoint.position;
			if (y > position2.y)
			{
				Transform transform = Presser.transform;
				Vector3 position3 = Presser.transform.position;
				float x = position3.x;
				Vector3 position4 = Presser.transform.position;
				transform.position = new Vector3(x, position4.y - Time.deltaTime * PresserStart);
				yield return null;
				continue;
			}
			break;
		}
		while (!EndMove)
		{
			Transform transform2 = Presser.transform;
			Vector3 position5 = Presser.transform.position;
			float x2 = position5.x;
			Vector3 position6 = Presser.transform.position;
			transform2.position = new Vector3(x2, position6.y - Time.deltaTime * PresserToCar);
			yield return null;
		}
		RaceLogic.instance.car.DeathWithOnDie();
	}
}
