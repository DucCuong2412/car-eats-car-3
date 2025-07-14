using UnityEngine;

public class Canva : MonoBehaviour
{
	public Canvas canvaNeedForCamera;

	private void OnEnable()
	{
		Camera[] array = UnityEngine.Object.FindObjectsOfType<Camera>();
		Camera[] array2 = array;
		foreach (Camera camera in array2)
		{
			if (camera.name == "Camera_GUI" || camera.name == "CameraUI")
			{
				canvaNeedForCamera.worldCamera = camera;
			}
		}
	}

	private void Update()
	{
	}
}
