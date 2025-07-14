using UnityEngine;

public class StopFolow : MonoBehaviour
{
	private void OnTriggerEnter2D(Collider2D other)
	{
		if (other.tag == "CarMain" || other.tag == "CarMainChild")
		{
			RaceLogic.instance.follow.Stop();
			ParallaxSystem parallaxSystem = UnityEngine.Object.FindObjectOfType<ParallaxSystem>();
			parallaxSystem.enabled = false;
		}
	}
}
