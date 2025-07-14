using UnityEngine;

[RequireComponent(typeof(BoxCollider2D))]
public class DestroyIfCar : MonoBehaviour
{
	private string cageSoundID;

	private void OnTriggerEnter2D(Collider2D other)
	{
		if (other.tag == "CarMain" || other.tag == "CarMainChild")
		{
			base.gameObject.SetActive(value: false);
		}
	}

	private void OnEnable()
	{
		if (base.gameObject.transform.parent != null && base.gameObject.transform.parent.gameObject.name.Contains("Cage"))
		{
			cageSoundID = Audio.Play((!(UnityEngine.Random.Range(0f, 1f) < 0.5f)) ? "gfx_cage_02_sn" : "gfx_cage_01_sn", Audio.soundVolume, loop: true);
		}
	}

	private void OnDisable()
	{
		if (cageSoundID != null)
		{
			Audio.Stop(cageSoundID);
		}
	}
}
