using System.Collections;
using UnityEngine;

public class BombControllerForCopter : MonoBehaviour
{
	public Collider2D collider;

	public int CollBomb = 5;

	public float delayForDropBombs;

	private bool is_first = true;

	public GameObject dropbomb;

	private void OnEnable()
	{
		collider.enabled = true;
		is_first = true;
	}

	private void OnTriggerEnter2D(Collider2D coll)
	{
		if ((coll.tag == "CarMain" || coll.tag == "CarMainChild") && is_first)
		{
			StartCoroutine(Booms());
		}
	}

	private IEnumerator Booms()
	{
		RaceLogic.instance.bomb_lip.Clear();
		is_first = false;
		int time = 0;
		while (time < CollBomb)
		{
			GameObject q = Pool.GameOBJECT(Pool.Bombs.bomblip, dropbomb.transform.position);
			q.SetActive(value: true);
			time++;
			yield return new WaitForSeconds(delayForDropBombs);
		}
		collider.enabled = false;
		is_first = true;
	}
}
