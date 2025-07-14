using UnityEngine;

public class ParticlesLayer : MonoBehaviour
{
	public void Start()
	{
		Pool.SetParticleSystemSortingLayer(base.gameObject, "Particles");
	}
}
