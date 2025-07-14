using UnityEngine;

public class PitTriger : MonoBehaviour
{
	public bool StartTriger;

	public bool ToNormalCarLayers;

	public bool Presser;

	public PitController controller;

	private void OnTriggerEnter2D(Collider2D other)
	{
		if (other.tag == "CarMain" || other.tag == "CarMainChild")
		{
			if (StartTriger)
			{
				GetComponent<BoxCollider2D>().enabled = false;
				controller.OnStartEnter();
			}
			else if (ToNormalCarLayers)
			{
				controller.OnNormalLayers();
				base.gameObject.SetActive(value: false);
			}
			else if (Presser)
			{
				controller.OnPresser();
			}
		}
	}
}
