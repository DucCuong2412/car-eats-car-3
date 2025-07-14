using UnityEngine;

public class IncubatorMG_TriggerLisenner : MonoBehaviour
{
	public IncubatorMG_InterectObj ControllerObjs;

	private IncubatorMG_Controller _controller;

	private void Start()
	{
		_controller = UnityEngine.Object.FindObjectOfType<IncubatorMG_Controller>();
	}

	private void OnTriggerEnter2D(Collider2D other)
	{
		if (other.tag == "CarMain")
		{
			_controller.OnTrigerEnter(ControllerObjs);
		}
	}
}
