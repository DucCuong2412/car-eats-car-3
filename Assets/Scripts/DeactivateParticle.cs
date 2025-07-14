using UnityEngine;

[RequireComponent(typeof(ParticleSystem))]
public class DeactivateParticle : MonoBehaviour
{
	private ParticleSystem[] allParticles;

	private void OnEnable()
	{
		allParticles = base.gameObject.GetComponentsInChildren<ParticleSystem>();
	}

	private void Update()
	{
		for (int i = 0; i < allParticles.Length; i++)
		{
			if (allParticles[i].isPlaying)
			{
				return;
			}
		}
		base.gameObject.SetActive(value: false);
	}

	private void OnBecameInvisible()
	{
		for (int i = 0; i < allParticles.Length; i++)
		{
			allParticles[i].Stop();
		}
		base.gameObject.SetActive(value: false);
	}
}
