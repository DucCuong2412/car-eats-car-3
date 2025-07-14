using System.Collections;
using UnityEngine;

public class CloseVideo : MonoBehaviour
{
	private Game.gameState GS;

	public Animator an;

	private int has_isOn = Animator.StringToHash("isON");

	private void OnEnable()
	{
		GS = Game.currentState;
		Game.OnStateChange(Game.gameState.OpenWindow);
	}

	private void Update()
	{
		if (UnityEngine.Input.GetKeyDown(KeyCode.Escape))
		{
			Close();
		}
	}

	public void Close()
	{
		StartCoroutine(closes());
		Game.OnStateChange(GS);
	}

	private IEnumerator closes()
	{
		an.SetBool(has_isOn, value: false);
		yield return 0;
		yield return 0;
		yield return 0;
		UnityEngine.Object.Destroy(base.gameObject);
	}
}
