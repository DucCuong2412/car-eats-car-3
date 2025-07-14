using UnityEngine;
using UnityEngine.UI;

public class CameraPrerender : MonoBehaviour
{
	public RawImage image;

	public bool disable;

	private void Update()
	{
		if (UnityEngine.Input.GetKeyDown(KeyCode.T))
		{
			disable = !disable;
		}
	}

	public void TakeSnapshot(int width, int height)
	{
		Texture2D texture2D = new Texture2D(width, height, TextureFormat.RGB24, mipChain: true);
		texture2D.ReadPixels(new Rect(0f, 0f, width, height), 0, 0);
		texture2D.Apply();
		image.texture = texture2D;
	}

	private void OnPostRender()
	{
		if (disable)
		{
			TakeSnapshot(Screen.width, Screen.height);
			disable = false;
		}
	}
}
