using UnityEngine;

[AddComponentMenu("NGUI/Examples/Load Level On Click")]
public class LoadLevelOnClick : MonoBehaviour
{
	public string levelName;

	private void OnClick()
	{
		if (!string.IsNullOrEmpty(levelName))
		{
			UnityEngine.SceneManagement.SceneManager.LoadScene(levelName);
		}
	}
}
