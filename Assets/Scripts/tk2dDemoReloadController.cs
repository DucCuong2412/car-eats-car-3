using UnityEngine;

[AddComponentMenu("2D Toolkit/Demo/tk2dDemoReloadController")]
public class tk2dDemoReloadController : MonoBehaviour
{
	private void Reload()
	{
		UnityEngine.SceneManagement.SceneManager.LoadScene(UnityEngine.SceneManagement.SceneManager.GetActiveScene().buildIndex);
	}
}
