using UnityEngine;

public class ParticleIgnoreTimeScale : MonoBehaviour
{
	private double lastTime;

	private ParticleSystem particle;

	private void OnEnable()
	{
		if (RaceLogic.instance.AllInitedForPool)
		{
			lastTime = Time.realtimeSinceStartup;
			particle = GetComponent<ParticleSystem>();
		}
	}

	private void Update()
	{
		float t = Time.realtimeSinceStartup - (float)lastTime;
		particle.Simulate(t, withChildren: true, restart: false);
		lastTime = Time.realtimeSinceStartup;
	}
}
