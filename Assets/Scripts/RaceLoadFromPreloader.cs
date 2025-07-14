using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class RaceLoadFromPreloader : MonoBehaviour
{
	public IEnumerator Start()
	{
		int t = 7;
		while (t > 0)
		{
			t--;
			yield return null;
		}
		SceneManager.LoadScene("Race", LoadSceneMode.Additive);
	}
}
