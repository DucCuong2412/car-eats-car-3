using UnityEngine;

public class TurboEffect : MonoBehaviour
{
	private MeshRenderer fire;

	private float prevstate = 1f;

	private void Start()
	{
		fire = GetComponent<MeshRenderer>();
	}

	private void OnEnable()
	{
		GetComponentInParent<Car2DAIController>().OnTurboChanged += Use;
	}

	private void Use(float value)
	{
		if (value > 0f && prevstate > value && fire != null)
		{
			fire.enabled = true;
		}
		prevstate = value;
	}

	private void Update()
	{
		fire.enabled = false;
	}
}
