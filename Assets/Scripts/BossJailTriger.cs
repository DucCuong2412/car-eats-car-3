using UnityEngine;

public class BossJailTriger : MonoBehaviour
{
	private void OnTriggerEnter2D(Collider2D other)
	{
		if (other.tag == "CarMain" || other.tag == "CarMainChild")
		{
			base.gameObject.SetActive(value: false);
		}
	}
}
