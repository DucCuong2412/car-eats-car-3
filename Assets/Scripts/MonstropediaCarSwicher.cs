using UnityEngine;

public class MonstropediaCarSwicher : MonoBehaviour
{
	public ControllerMonstropedia controller;

	public bool ForCar;

	public bool ForName;

	private void OnEnable()
	{
		if (ForCar)
		{
			controller.CarSwich();
		}
		else if (ForName)
		{
			controller.NameSwich();
		}
	}
}
