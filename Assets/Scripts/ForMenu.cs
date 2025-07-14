using System.Collections;
using UnityEngine;

public class ForMenu : MonoBehaviour
{
	public Animation anim;

	private void OnEnable()
	{
		StartCoroutine(start());
	}

	private void OnDisable()
	{
	}

	private IEnumerator start()
	{
		while (anim.isPlaying)
		{
			yield return null;
		}
		if (Progress.settings.showComics)
		{
			UnityEngine.SceneManagement.SceneManager.LoadScene("Comics");
		}
		else
		{
			Game.LoadLevel("map_new");
		}
	}
}
