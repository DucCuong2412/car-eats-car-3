using UnityEngine;

public class test : MonoBehaviour
{
	private void Awake()
	{
	}

	private void Start()
	{
		EasyHill2DManager.Instance().InitCulling();
	}

	private void Update()
	{
		EasyHill2DManager.Instance().CullSegments();
		if (UnityEngine.Input.GetKey(KeyCode.Space) || UnityEngine.Input.touchCount > 0)
		{
			GetComponent<Rigidbody2D>().AddForce(Vector2.up * -100f);
		}
		if (UnityEngine.Input.GetKeyUp(KeyCode.Escape))
		{
			Application.Quit();
		}
		if (UnityEngine.Input.touchCount > 1)
		{
			UnityEngine.SceneManagement.SceneManager.LoadScene(0);
		}
	}

	private void OnGUI()
	{
		GUI.Label(new Rect(25f, 25f, 400f, 200f), " Touch screen or press Space to accelerate the ball downwards.");
	}
}
