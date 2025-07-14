using UnityEngine;

public class ForCageTutorial : MonoBehaviour
{
	public GameObject shade;

	public Animator animClicProgress;

	public void showShade()
	{
		shade.SetActive(value: true);
		Time.timeScale = 0f;
	}

	public void hideShade()
	{
		shade.SetActive(value: false);
		Time.timeScale = 1f;
	}
}
