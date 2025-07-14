using AnimationOrTween;
using System.Collections;
using UnityEngine;

public class UnTimedAnimation : MonoBehaviour
{
	public void OnEnable()
	{
		StartCoroutine(Play(base.gameObject.GetComponent<Animation>()));
	}

	public IEnumerator Play(Animation animation)
	{
		while (true)
		{
			ActiveAnimation.Play(animation, Direction.Forward);
			yield return Utilities.WaitForRealSeconds(14f);
		}
	}
}
