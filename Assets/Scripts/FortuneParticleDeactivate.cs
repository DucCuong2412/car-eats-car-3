using UnityEngine;

[RequireComponent(typeof(ParticleSystem))]
public class FortuneParticleDeactivate : MonoBehaviour
{
	private float dt;

	private void OnEnable()
	{
		dt = 0f;
	}

	private void Update()
	{
		dt += Time.unscaledDeltaTime;
		if (dt < GetComponent<ParticleSystem>().duration * 2f)
		{
			GetComponent<ParticleSystem>().Simulate(Time.unscaledDeltaTime, withChildren: true, restart: false);
		}
		else
		{
			base.gameObject.SetActive(value: false);
		}
	}
}
